Imports Microsoft.VisualBasic
Imports System

Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Web.Templates

Partial Public Class LoginPage
	Inherits BaseXafPage
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
    End Sub
    Public Overrides ReadOnly Property InnerContentPlaceHolder() As System.Web.UI.Control
        Get
            Return Content
        End Get
    End Property
End Class
