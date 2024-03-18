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

 1. { "name":"TabPages[0].BackColor", "value":"Blue" },
 2. { "name":"Font", "value":"Microsoft Sans Serif, 10pt" },
 3. { "name":"BackColor", "value":"Blue" },



 
