Imports System
Imports System.Collections.Generic
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.DC
Imports DevExpress.ExpressApp.Updating

Namespace RuntimeDbChooser.Module

    ' For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
    Public NotInheritable Class RuntimeDbChooserModule
        Inherits ModuleBase

        Public Sub New()
            AdditionalExportedTypes.Add(GetType(DevExpress.Persistent.BaseImpl.EF.ModelDifference))
            AdditionalExportedTypes.Add(GetType(DevExpress.Persistent.BaseImpl.EF.ModelDifferenceAspect))
            AdditionalExportedTypes.Add(GetType(BusinessObjects.ApplicationUser))
            RequiredModuleTypes.Add(GetType(SystemModule.SystemModule))
            RequiredModuleTypes.Add(GetType(Security.SecurityModule))
            RequiredModuleTypes.Add(GetType(Validation.ValidationModule))
            RequiredModuleTypes.Add(GetType(ReportsV2.ReportsModuleV2))
        End Sub

        Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
            Dim updater As ModuleUpdater = New DatabaseUpdate.Updater(objectSpace, versionFromDB)
            Return New ModuleUpdater() {updater}
        End Function

        Public Overrides Sub Setup(ByVal application As XafApplication)
            MyBase.Setup(application)
        ' Manage various aspects of the application UI and behavior at the module level.
        End Sub

        Public Overrides Sub CustomizeTypesInfo(ByVal typesInfo As ITypesInfo)
            MyBase.CustomizeTypesInfo(typesInfo)
        End Sub
    End Class
End Namespace
