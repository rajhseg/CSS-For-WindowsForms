
using System;
using System.Collections.Generic;

namespace CssLibrary
{
	/// <summary>
	/// Description of WinformsControlProperties.
	/// </summary>
	public class WinformsControlProperties
	{
		public IEnumerable<string> Classes {
			get;
			private set;
		}
		
		public WinformsControlProperties(string classnames)
		{
			Classes = new List<string>();
			
			if(!string.IsNullOrEmpty(classnames))
			{
				var clsarray = classnames.Split(',');
				Classes = new List<string>(clsarray);
			}
				
		}
	}
}
