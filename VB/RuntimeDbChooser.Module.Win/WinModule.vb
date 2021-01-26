Imports Microsoft.VisualBasic
Imports System
Imports System.Text
Imports System.Linq
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.ExpressApp.DC
Imports System.Collections.Generic
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Model.Core
Imports DevExpress.ExpressApp.Model.DomainLogics
Imports DevExpress.ExpressApp.Model.NodeGenerators
Imports DevExpress.Persistent.BaseImpl

' For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
<ToolboxItemFilter("Xaf.Platform.Win")> _
Partial Public NotInheritable Class RuntimeDbChooserWindowsFormsModule
    Inherits ModuleBase
	'Private Sub Application_CreateCustomModelDifferenceStore(ByVal sender As Object, ByVal e As CreateCustomModelDifferenceStoreEventArgs)
	'	e.Store = New ModelDifferenceDbStore(CType(sender, XafApplication), GetType(ModelDifference), True)
	'	e.Handled = True
	'End Sub
	Private Sub Application_CreateCustomUserModelDifferenceStore(ByVal sender As Object, ByVal e As CreateCustomModelDifferenceStoreEventArgs)
		e.Store = New ModelDifferenceDbStore(CType(sender, XafApplication), GetType(ModelDifference), False)
		e.Handled = True
	End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
    Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
        Return ModuleUpdater.EmptyModuleUpdaters
    End Function

    Public Overrides Sub Setup(application As XafApplication)
        MyBase.Setup(application)
        'AddHandler application.CreateCustomModelDifferenceStore, AddressOf Application_CreateCustomModelDifferenceStore
        AddHandler application.CreateCustomUserModelDifferenceStore, AddressOf Application_CreateCustomUserModelDifferenceStore
        ' Manage various aspects of the application UI and behavior at the module level.
    End Sub
End Class

