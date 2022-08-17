Imports System
Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports DevExpress.ExpressApp.Win.Utils
Imports DevExpress.Skins
Imports DevExpress.Utils.Drawing
Imports DevExpress.Utils.Svg
Imports DevExpress.XtraSplashScreen

Namespace RuntimeDbChooser.Win

    Public Partial Class XafSplashScreen
        Inherits SplashScreen

        Private Sub LoadBlankLogo()
            Dim assembly As Assembly = Assembly.GetExecutingAssembly()
            Dim blankLogoResourceName As String = assembly.GetName().Name & ".Images.Logo.svg"
            Dim svgStream As Stream = assembly.GetManifestResourceStream(blankLogoResourceName)
            If svgStream IsNot Nothing Then
                svgStream.Position = 0
                peLogo.SvgImage = SvgImage.FromStream(svgStream)
            End If
        End Sub

        Protected Overrides Sub DrawContent(ByVal graphicsCache As GraphicsCache, ByVal skin As Skin)
            Dim bounds As Rectangle = ClientRectangle
            bounds.Width -= 1
            bounds.Height -= 1
            graphicsCache.Graphics.DrawRectangle(graphicsCache.GetPen(Color.FromArgb(255, 87, 87, 87), 1), bounds)
        End Sub

        Protected Sub UpdateLabelsPosition()
            labelApplicationName.CalcBestSize()
            Dim newLeft As Integer =(Width - labelApplicationName.Width) \ 2
            labelApplicationName.Location = New Point(newLeft, labelApplicationName.Top)
            labelSubtitle.CalcBestSize()
            newLeft =(Width - labelSubtitle.Width) \ 2
            labelSubtitle.Location = New Point(newLeft, labelSubtitle.Top)
        End Sub

        Public Sub New()
            InitializeComponent()
            LoadBlankLogo()
            labelCopyright.Text = "Copyright © " & Date.Now.Year.ToString() & " Company Name" & Environment.NewLine & "All rights reserved."
            UpdateLabelsPosition()
        End Sub

#Region "Overrides"
        Public Overrides Sub ProcessCommand(ByVal cmd As [Enum], ByVal arg As Object)
            MyBase.ProcessCommand(cmd, arg)
            If CType(cmd, UpdateSplashCommand) = UpdateSplashCommand.Description Then
                labelStatus.Text = CStr(arg)
            End If
        End Sub
#End Region
    End Class
End Namespace
