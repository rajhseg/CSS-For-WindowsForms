# CascadingStylesheet For WindowsForms
This repo is consists of CSS kind of implementation for windows forms, we can apply styles using classnames, controlnames and controltype.

**Steps:**
1. DragDrop this component in UI.
2. Set the ApplyStyle property to True for each control which needs to apply style.
3. Default style file is **styles.json** , and default sheetname must present in file is **sheet1** .	**"sheetname" : "sheet1" in styles.json** ,
4. if you configure the style with class name, please specify the classname in the control property.
5. you can assign styles using controlType or classname or controlname etc.
6. Property value which needs styles can be separated through "." dot, collections needs to represent as "[1]".
7. For applying a image to a control, first we have to add **resource.resx** file and add item image as resource. for example in our demo we have added a **"ImageResource.resx"** file, then set the property **"copy to output": "always"** for that resx. then add that **file path to styles.json** in resxfiles section as mentioned in below snippet. then in BackgroundImage property value must be specified as **"resx_path||filekey_name_inResx"**


**Sample Styles Definition: styles.json**

 1. "resxfiles": ["ResxFiles//ImageResource.resx", "ResxFiles//ImageResource1.resx"],
 2. { "name":"TabPages[0].BackColor", "value":"Blue" },
 3. { "name":"Font", "value":"Microsoft Sans Serif, 10pt" },
 4. { "name":"BackColor", "value":"Blue" },
 5. { "name": "BackgroundImage", "value":"ResxFiles//ImageResource.resx||sample1" },
 6. { "name": "BackgroundImageLayout", "value":"Stretch" }   




![sample4](https://github.com/rajhseg/CascadingStylesheet-For-WindowsForms/assets/9523832/714aee85-0ccb-46fe-804b-cb59d6f2e75b)





![sample3](https://github.com/rajhseg/CascadingStylesheet-For-WindowsForms/assets/9523832/29a5cd43-3144-4fef-a847-bebb648cffbd)




![sample1](https://github.com/rajhseg/CascadingStylesheet-For-WindowsForms/assets/9523832/2aa5b44c-dbbb-4717-a0bf-75ca62449eab)




 
