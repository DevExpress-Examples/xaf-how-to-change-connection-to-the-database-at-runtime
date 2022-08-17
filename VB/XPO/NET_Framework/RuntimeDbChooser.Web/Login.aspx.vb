Imports System
Imports DevExpress.ExpressApp.Web.Templates

Public Partial Class LoginPage
    Inherits BaseXafPage

    Public Overrides ReadOnly Property InnerContentPlaceHolder As Web.UI.Control
        Get
            Return Content
        End Get
    End Property
End Class
