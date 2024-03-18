
using System;
using Newtonsoft.Json;
using System.Collections.Generic; 

namespace CssLibrary
{

public class WinFormTheme
{
    [JsonProperty("name",Required = Required.Always)]
    public string Name { get; set; }

    [JsonProperty("files")]
    public List<WinFormFiles> Files {get;set;}
}

public class WinFormFiles {
	
	[JsonProperty("sheetname", Required = Required.Always)]
	public string SheetName {get; set;}
	
	[JsonProperty("styles")]
    public List<WinFormsStyles> Styles {get;set;}
}

public class WinFormsStyles
{
    [JsonProperty("controlname")]
    public string ControlName {get;set;} 

    [JsonProperty("classname")]
    public string ClassName {get;set;} 

    [JsonProperty("controltype")]
    public string ControlType { get; set; }

    [JsonProperty("excludecontrolnames")]
    public List<string> ExcludeControlNames {get;set;}

    [JsonProperty("props")]
    public List<WinFormsProps> Props {get;set;} 
}

public class WinFormsProps 
{
    [JsonProperty("name", Required =Required.Always)]
    public string Name {get;set;} 

    [JsonProperty("value", Required =Required.Always)]
    public string Value {get;set;} 
}

}
