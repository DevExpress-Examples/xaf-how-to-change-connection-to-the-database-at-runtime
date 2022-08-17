;#DropDB RuntimeDbChooserEasyTest

#Application RuntimeDbChooserWin
#Application RuntimeDbChooserWeb

*FillForm
 User Name = Admin
*Action Log In

#IfDef RuntimeDbChooserWin

*Action About...
 
*Action OK

#EndIf

#IfDef RuntimeDbChooserWeb
*Action Navigation(My Details)
#EndIf
