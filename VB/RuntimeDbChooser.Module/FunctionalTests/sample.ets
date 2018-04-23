;#DropDB RuntimeDbChooserEasyTest

#Application RuntimeDbChooser.Win
#Application RuntimeDbChooser.Web

*FillForm
 User Name = Admin
*Action Log On

#IfDef RuntimeDbChooser.Win


 
*Action OK

#EndIf

#IfDef RuntimeDbChooser.Web
*Action Navigation(My Details)
#EndIf
