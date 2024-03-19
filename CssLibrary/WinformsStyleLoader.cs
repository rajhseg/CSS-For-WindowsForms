
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace CssLibrary
{
	/// <summary>
	/// Description of WinformsStyleLoader.
	/// </summary>
	public class WinformsStyleLoader
	{
		private static WinformsStyleLoader instance = null;
		
		private WinFormTheme winformTheme;
		
		private static string winformsheetname;
		
		private Hashtable controls;
		
		public static WinformsStyleLoader GetThemeLoader(){
			if(instance==null)
				instance = new WinformsStyleLoader();
			return instance;
		}
		
		private WinformsStyleLoader()
		{		
						
		}
		
		public void Load(string filename = "styles.json"){						
			var assemblypath = Assembly.GetCallingAssembly().Location;
			var dirinfo = new DirectoryInfo(assemblypath).Parent.FullName;
			string styleData = File.ReadAllText(dirinfo+"\\"+filename);
		    winformTheme = JsonConvert.DeserializeObject<WinFormTheme>(styleData);		  
		}
		
		public static void SetSheetName(string sheetname){
			winformsheetname = sheetname;
		}
		
		internal void LoadControls(Hashtable controllist){
			this.controls = controllist;
		}
		
		public void RenderStyle() {
			
			var selectedSheet = winformTheme.Files.Where(x=>x.SheetName == winformsheetname).FirstOrDefault();
						 
		    foreach (var element in selectedSheet.ResourcexFiles) {
				ResourceManagerExtensions.LoadResources(element);
		    }
		    									
			if(selectedSheet!=null){
				
				foreach (DictionaryEntry element in controls) {
					
					var controlType = element.Key.GetType().Name;
					var controlname = ((Control)element.Key).Name;
					var classnames = ((WinformsControlProperties)element.Value).Classes;
					
					List<WinFormsStyles> applyingStyles = new List<WinFormsStyles>();
					
					var controlTypeStyles = selectedSheet.Styles.Where(x=>x.ControlType == controlType);
					
					var controlnameStyle = selectedSheet.Styles.Where(x=>x.ControlName.ToLower() == controlname.ToLower()).FirstOrDefault();
					
					List<WinFormsStyles> classStyles = new List<WinFormsStyles>();
					
					foreach (var ele in classnames) {
						var clsItem = selectedSheet.Styles.Where(x=>x.ClassName.ToLower() == ele.ToString().ToLower()).FirstOrDefault();
						if(clsItem!=null){
							classStyles.Add(clsItem);
						}
					}
					
					applyingStyles.AddRange(controlTypeStyles);
					applyingStyles.AddRange(classStyles);
					
					if(controlnameStyle!=null)
						applyingStyles.Add(controlnameStyle);
					
					List<WinFormsProps> propertiesStyleValues = this.GetStylesAfterMerge(applyingStyles, controlname);
					
					this.ApplyStyleForControl((Control)element.Key, propertiesStyleValues);
				}
			}
			
		}
		
		private void ApplyStyleForControl(Control cntrl, List<WinFormsProps> properties){
			
			foreach (var element in properties) {
				var propertyFullName = element.Name;
				var applyValue = element.Value;
				var propList = propertyFullName.Split('.');
				
				var propInfoObj = this.GetTargetingObjectToApplyValueForProperty(cntrl, propList, 0);
				var _propertyStyleToApply = propList[propList.Length-1];
				var actualSetProp = propInfoObj.GetType().GetProperties().Where(x=>x.Name.ToLower()==_propertyStyleToApply.ToLower()).FirstOrDefault();
				
				
				if(actualSetProp!=null) {					
				   
					string resourcename = string.Empty;
					bool isFromResourceFile =false;
					
					if(applyValue.Contains("||")){
						var splitResources = applyValue.Split(new string [] {"||"}, StringSplitOptions.None);
						isFromResourceFile = true;
						resourcename = splitResources[0];
						applyValue = splitResources[1];
					}									
					 
					Type t = Nullable.GetUnderlyingType(actualSetProp.PropertyType) ?? actualSetProp.PropertyType;
					object safeValue = null;
					
					if(isFromResourceFile){
						safeValue  = ResourceManagerExtensions.GetObject(resourcename, applyValue);										 	
					}
					else {
				 		safeValue =  RuntimeHelpers.GetObjectValue(TypeDescriptor.GetConverter(t).ConvertFromString(applyValue));
					}
					
					if(actualSetProp.CanWrite) {
				    	actualSetProp.SetValue(propInfoObj, safeValue, null);					    									    												
					}
					else{
						throw new System.Exception("Property "+actualSetProp.Name+" is not writable for style ("+propertyFullName
					                		+"), please set the value for parent property in string");
					}
			}
		 }
		}
		
		private object GetTargetingObjectToApplyValueForProperty(object obj, string[] propsList, int currentIndex){
			
			if(currentIndex == propsList.Count()-1) {
				return obj;
			}
			
			string propertyname = propsList[currentIndex];
			int index = -1;
			bool isIndexObject = false;
			
			if(propertyname.Contains("[")){
				isIndexObject = true;
				int sindex = propertyname.IndexOf("[");
				int eindex = propertyname.IndexOf("]");
				int length =  eindex-sindex -1;
				int startindex = sindex+1;
				string indexnum = propertyname.Substring(startindex,length);
				index = int.Parse(indexnum);
				propertyname = propertyname.Substring(0,sindex);
			}
			
			var result = obj.GetType().GetProperty(propertyname);
			currentIndex++;
			
			var propObj = result.GetValue(obj,null);
			
			if(this.isCollection(propObj)){
				propObj = ((IList)propObj)[index];
			}
						
			return GetTargetingObjectToApplyValueForProperty(propObj, propsList, currentIndex);
		}
	
		private bool isCollection(object o)
	    {
	        return typeof(IList).IsAssignableFrom(o.GetType())
	            || typeof(IList<>).IsAssignableFrom(o.GetType());
	    }
		 
		private List<WinFormsProps> GetStylesAfterMerge(List<WinFormsStyles> styles, string controlname){
			
			List<WinFormsProps> propsList = new List<WinFormsProps>();
			List<string> propertynames = new List<string>();
			
			foreach (WinFormsStyles element in styles) {
				
				if(element.ExcludeControlNames == null || (element.ExcludeControlNames!=null && !element.ExcludeControlNames.Contains(controlname))){
					foreach (var propEle in element.Props) {
						if(propertynames.Any(x=> x.ToLower() == propEle.Name.ToLower())){
							// Already added property need to update style
							var existData = propsList.First(x=>x.Name.ToLower()==propEle.Name.ToLower());
							existData.Value = propEle.Value;
						}
						else{
							// add the style
							propsList.Add(propEle);
						}
					}
				}
			}
			
			propertynames.Clear();
			
			return propsList;
		}
	}
}
