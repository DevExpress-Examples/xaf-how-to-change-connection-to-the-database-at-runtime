Imports System.Web.UI
Imports DevExpress.ExpressApp.Templates
Imports DevExpress.ExpressApp.Web.Templates

Public Partial Class [Default]
    Inherits BaseXafPage

    Protected Overrides Function CreateContextActionsMenu() As ContextActionsMenu
        Return New ContextActionsMenu(Me, "Edit", "RecordEdit", "ObjectsCreation", "ListView", "Reports")
    End Function

    Public Overrides ReadOnly Property InnerContentPlaceHolder As Control
        Get
            Return Content
        End Get
    End Property
End Class
