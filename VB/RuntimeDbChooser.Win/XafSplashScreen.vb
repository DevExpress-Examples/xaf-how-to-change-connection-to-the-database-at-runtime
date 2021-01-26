Imports System.IO
Imports System.Reflection
Imports DevExpress.ExpressApp.Win.Utils
Imports DevExpress.Skins
Imports DevExpress.Utils.Drawing
Imports DevExpress.Utils.Svg
Imports DevExpress.XtraSplashScreen

Public Partial Class XafSplashScreen
    Inherits SplashScreen

    Private Sub LoadBlankLogo()
        Dim _assembly As Assembly = Assembly.GetExecutingAssembly()
        Dim blankLogoResourceName As String = _assembly.GetName().Name & ".Logo.svg"
        Dim svgStream As Stream = _assembly.GetManifestResourceStream(blankLogoResourceName)
        If svgStream IsNot Nothing Then
            svgStream.Position = 0
            peLogo.SvgImage = SvgImage.FromStream(svgStream)
        End If
    End Sub

	Protected Overrides Sub DrawContent(graphicsCache As GraphicsCache, skin As Skin)
        Dim bounds As Rectangle = ClientRectangle
        bounds.Width = (bounds.Width - 1)
        bounds.Height = (bounds.Height - 1)
        graphicsCache.Graphics.DrawRectangle(graphicsCache.GetPen(Color.FromArgb(255, 87, 87, 87), 1), bounds)
    End Sub

    Protected Sub UpdateLabelsPosition()
        labelApplicationName.CalcBestSize()
        Dim newLeft As Integer = CType((Width - labelApplicationName.Width) / 2, Integer)
        labelApplicationName.Location = New Point(newLeft, labelApplicationName.Top)
        lableSubtitle.CalcBestSize()
        newLeft = CType((Width - lableSubtitle.Width) / 2, Integer)
        lableSubtitle.Location = New Point(newLeft, lableSubtitle.Top)
    End Sub

    Sub New()
        InitializeComponent()
		LoadBlankLogo()
        Me.labelCopyright.Text = "Copyright © " & DateTime.Now.Year.ToString() & " Company Name" _
            & System.Environment.NewLine & "All rights reserved."
        UpdateLabelsPosition()
    End Sub
    Public Overrides Sub ProcessCommand(ByVal cmd As System.Enum, ByVal arg As Object)
        MyBase.ProcessCommand(cmd, arg)
        If (CType(cmd, UpdateSplashCommand) = UpdateSplashCommand.Description) Then
            labelStatus.Text = CType(arg, String)
        End If

    End Sub

End Class
