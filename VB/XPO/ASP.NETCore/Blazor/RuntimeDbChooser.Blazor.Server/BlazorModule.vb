Imports System
Imports System.Collections.Generic
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Persistent.BaseImpl

Namespace RuntimeDbChooser.Module.Blazor

    Public NotInheritable Class RuntimeDbChooserBlazorModule
        Inherits ModuleBase

        Public Sub New()
        End Sub

        Private Sub Application_CreateCustomUserModelDifferenceStore(ByVal sender As Object?, ByVal e As CreateCustomModelDifferenceStoreEventArgs)
            ArgumentNullException.ThrowIfNull(sender)
            e.Store = New ModelDifferenceDbStore(CType(sender, XafApplication), GetType(ModelDifference), False, "Blazor")
            e.Handled = True
        End Sub

        Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
            Return ModuleUpdater.EmptyModuleUpdaters
        End Function

        Public Overrides Sub Setup(ByVal application As XafApplication)
            MyBase.Setup(application)
            application.CreateCustomUserModelDifferenceStore += AddressOf Application_CreateCustomUserModelDifferenceStore
        End Sub
    End Class
End Namespace
