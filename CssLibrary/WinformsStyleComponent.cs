
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;

namespace CssLibrary
{
	/// <summary>
	/// Description of WinformsStyleComponent.
	/// </summary>
	[ProvideProperty("ClassName", typeof(Control))]
	[ProvideProperty("ApplyStyles", typeof(Control))]
	public class WinformsStyleComponent: Component, IExtenderProvider,ISupportInitialize
	{
		private Hashtable controls;
		private WinformsStyleLoader styleLoader;
		
		public WinformsStyleComponent()
		{
			this.controls = new Hashtable();			
		}
		
		public WinformsStyleComponent(IContainer parent) : this() {
			parent.Add(this);
		}
		
		public bool GetApplyStyles(Control item){
			
			return this.controls.ContainsKey(item);
		}
		
		public void SetApplyStyles(Control item, bool value)
		{
			if(!value){
				this.controls.Remove(item);
			}
			else{
			
				if(!this.controls.ContainsKey(item)){
			
					this.controls.Add(item, new WinformsControlProperties(""));
				
				}
			
			}
		}
		
		public void SetClassName(Control item, string value){
			if(GetApplyStyles(item)){
				if(this.controls.ContainsKey(item)){
					this.controls[item] = new WinformsControlProperties(value);
				}
				else{
					this.controls.Add(item, new WinformsControlProperties(value));
				}
			}
		}
		
		
		[Description("This section is used for styling the control using styles configured in styles.json")]
		public string GetClassName(Control item){
			if(this.controls.ContainsKey(item)){
				var cntrlprops = (WinformsControlProperties)this.controls[item];
				string result = string.Empty;
				
				foreach (var element in cntrlprops.Classes) {
				  result+=element+",";	
				}
				
				if(cntrlprops.Classes.Count()>0)
					result = result.Substring(0, result.Length -1);
				
				return result;
			}
			return string.Empty;
		}
		
		public bool CanExtend(object extendee)
		{
			if(extendee is Control){
				return true;
			}
			
			return false;
		}
		
		public void BeginInit()
		{
			
		}
		
		public void EndInit()
		{
			if(!DesignMode){
				styleLoader = WinformsStyleLoader.GetThemeLoader();
				WinformsStyleLoader.SetSheetName("sheet1");			
				styleLoader.LoadControls(this.controls);
				styleLoader.Load();
				styleLoader.RenderStyle();
			}
		}
	}
}
