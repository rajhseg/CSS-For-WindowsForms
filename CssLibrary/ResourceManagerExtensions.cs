
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace CssLibrary
{
	/// <summary>
	/// Description of ResourceManagerExtensions.
	/// </summary>
	public class ResourceManagerExtensions
	{
		private static Dictionary<string, Dictionary<string, object>> rsxFiles = new Dictionary<string, Dictionary<string, object>>();
		
		public ResourceManagerExtensions()
		{
			
		}
		
		public static object GetObject(string resourcename, string propname){
			if(rsxFiles.ContainsKey(resourcename)){
				if(rsxFiles[resourcename].ContainsKey(propname)){
					return rsxFiles[resourcename][propname];
				}
			}
			
			return null;
		}
		
		public static  void LoadResources(string resourceFileName) {

			if(rsxFiles.ContainsKey(resourceFileName)){
				rsxFiles.Remove(resourceFileName);
			}
			
        var assembly = Assembly.GetCallingAssembly();        

        // Assuming your resource file is located in the same directory as the assembly
        string resourceFilePath = Path.Combine(Path.GetDirectoryName(assembly.Location), resourceFileName);

        if (File.Exists(resourceFilePath))
        {        
            using (FileStream stream = File.Open(resourceFilePath, FileMode.Open))
            {
                // Assuming your resource file is of type ResXResourceReader
                // Adjust the reader type based on your resource file format
                using (ResXResourceReader reader = new ResXResourceReader(stream))
                {
                	Dictionary<string, object> values = new Dictionary<string, object>();
                	                   
                    foreach (DictionaryEntry entry in reader)
                    {
                        string key = entry.Key.ToString();
                        object value = entry.Value;
                        values.Add(key, value);
                    }
                    
                    rsxFiles.Add(resourceFileName, values);
                }
            }
        }
        else
        {
            throw new Exception("Resource file not found. "+resourceFileName);
        }
		}
	}
}
