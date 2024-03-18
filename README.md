# CascadingStylesheet For WindowsForms
This repo is consists of cascading stylesheet kind of implementation for windows forms, we can apply styles using classnames, controlnames and controltype.

**Steps:**
1. DragDrop this component in UI.
2. Set the ApplyStyle property to True for each control which needs to apply style.
3. Default style file is styles.json
4. if you configure the style with class name, please specify the classname in the control property.
5. you can assign styles using controlType or classname or controlname etc.
6. Property value which needs styles can be separated through "." dot, collections needs to represent as "[1]".

**Sample Styles Definition: styles.json**

{
  "name": "style1",
  "files": [
    { 
   		"sheetname" : "sheet2",
		"styles":[
        { 
            "controlname": "",
            "classname": "",
            "controltype": "MainForm",
            "excludecontrolnames": [ "txt3", "txt4"],
            "props": [
                { "name":"Font", "value":"Microsoft Sans Serif, 10pt" },
                { "name":"BackColor", "value":"Blue" },
                { "name":"Padding", "value":"2,2,2,2" }                
            ]
        },
        { 
            "controlname": "",
            "classname": "",
            "controltype": "Button",
            "props": [               
                { "name":"BackColor", "value":"Red" },
                { "name":"Padding", "value":"2,2,2,2" }                
            ]
        },
         { 
            "controlname": "",
            "classname": "",
            "controltype": "DataGridView",
            "props": [
                { "name":"BackgroundColor", "value":"Blue" },
                { "name":"Padding", "value":"2,2,2,2" }                
            ]
        },
        {
        	"controlname":"",
        	"classname":"tabclass1",
        	"controltype":"",
        	"props": [
                { "name":"TabPages[0].BackColor", "value":"Blue" },
                { "name":"Padding", "value":"2,2" }                
            ]
        },
        {
        	"controlname":"",
        	"classname":"",
        	"controltype":"Label",
        	"props": [
                { "name":"ForeColor", "value":"White" }                           
            ]
        }
    ]   		
    }
  
  ]
}
