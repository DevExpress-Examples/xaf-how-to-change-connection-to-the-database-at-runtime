Namespace RuntimeDbChooser.Win

    Partial Class XafSplashScreen

        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (Me.components IsNot Nothing) Then
                Me.components.Dispose()
            End If

            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RuntimeDbChooser.Win.XafSplashScreen))
            Me.progressBarControl = New DevExpress.XtraEditors.MarqueeProgressBarControl()
            Me.labelCopyright = New DevExpress.XtraEditors.LabelControl()
            Me.labelStatus = New DevExpress.XtraEditors.LabelControl()
            Me.peImage = New DevExpress.XtraEditors.PictureEdit()
            Me.peLogo = New DevExpress.XtraEditors.PictureEdit()
            Me.pcApplicationName = New DevExpress.XtraEditors.PanelControl()
            Me.labelSubtitle = New DevExpress.XtraEditors.LabelControl()
            Me.labelApplicationName = New DevExpress.XtraEditors.LabelControl()
            CType((Me.progressBarControl.Properties), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.peImage.Properties), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.peLogo.Properties), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.pcApplicationName), System.ComponentModel.ISupportInitialize).BeginInit()
            Me.pcApplicationName.SuspendLayout()
            Me.SuspendLayout()
            ' 
            ' progressBarControl
            ' 
            Me.progressBarControl.EditValue = 0
            Me.progressBarControl.Location = New System.Drawing.Point(74, 271)
            Me.progressBarControl.Name = "progressBarControl"
            Me.progressBarControl.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb((CInt(((CByte((195)))))), (CInt(((CByte((194)))))), (CInt(((CByte((194)))))))
            Me.progressBarControl.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
            Me.progressBarControl.Properties.EndColor = System.Drawing.Color.FromArgb((CInt(((CByte((255)))))), (CInt(((CByte((114)))))), (CInt(((CByte((0)))))))
            Me.progressBarControl.Properties.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
            Me.progressBarControl.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
            Me.progressBarControl.Properties.LookAndFeel.UseDefaultLookAndFeel = False
            Me.progressBarControl.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid
            Me.progressBarControl.Properties.StartColor = System.Drawing.Color.FromArgb((CInt(((CByte((255)))))), (CInt(((CByte((144)))))), (CInt(((CByte((0)))))))
            Me.progressBarControl.Size = New System.Drawing.Size(350, 16)
            Me.progressBarControl.TabIndex = 5
            ' 
            ' labelCopyright
            ' 
            Me.labelCopyright.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            Me.labelCopyright.Location = New System.Drawing.Point(24, 324)
            Me.labelCopyright.Name = "labelCopyright"
            Me.labelCopyright.Size = New System.Drawing.Size(47, 13)
            Me.labelCopyright.TabIndex = 6
            Me.labelCopyright.Text = "Copyright"
            ' 
            ' labelStatus
            ' 
            Me.labelStatus.Location = New System.Drawing.Point(75, 253)
            Me.labelStatus.Name = "labelStatus"
            Me.labelStatus.Size = New System.Drawing.Size(50, 13)
            Me.labelStatus.TabIndex = 7
            Me.labelStatus.Text = "Starting..."
            ' 
            ' peImage
            ' 
            Me.peImage.EditValue =(CObj((resources.GetObject("peImage.EditValue"))))
            Me.peImage.Location = New System.Drawing.Point(12, 12)
            Me.peImage.Name = "peImage"
            Me.peImage.Properties.AllowFocused = False
            Me.peImage.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
            Me.peImage.Properties.Appearance.Options.UseBackColor = True
            Me.peImage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            Me.peImage.Properties.ShowMenu = False
            Me.peImage.Size = New System.Drawing.Size(426, 180)
            Me.peImage.TabIndex = 9
            Me.peImage.Visible = False
            ' 
            ' peLogo
            ' 
            Me.peLogo.EditValue =(CObj((resources.GetObject("peLogo.EditValue"))))
            Me.peLogo.Location = New System.Drawing.Point(400, 328)
            Me.peLogo.Name = "peLogo"
            Me.peLogo.Properties.AllowFocused = False
            Me.peLogo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
            Me.peLogo.Properties.Appearance.Options.UseBackColor = True
            Me.peLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            Me.peLogo.Properties.ShowMenu = False
            Me.peLogo.Size = New System.Drawing.Size(70, 20)
            Me.peLogo.TabIndex = 8
            ' 
            ' pcApplicationName
            ' 
            Me.pcApplicationName.Appearance.BackColor = System.Drawing.Color.FromArgb((CInt(((CByte((255)))))), (CInt(((CByte((114)))))), (CInt(((CByte((0)))))))
            Me.pcApplicationName.Appearance.Options.UseBackColor = True
            Me.pcApplicationName.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            Me.pcApplicationName.Controls.Add(Me.labelSubtitle)
            Me.pcApplicationName.Controls.Add(Me.labelApplicationName)
            Me.pcApplicationName.Dock = System.Windows.Forms.DockStyle.Top
            Me.pcApplicationName.Location = New System.Drawing.Point(1, 1)
            Me.pcApplicationName.LookAndFeel.UseDefaultLookAndFeel = False
            Me.pcApplicationName.Name = "pcApplicationName"
            Me.pcApplicationName.Size = New System.Drawing.Size(494, 220)
            Me.pcApplicationName.TabIndex = 10
            ' 
            ' labelSubtitle
            ' 
            Me.labelSubtitle.Appearance.Font = New System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte((0))))
            Me.labelSubtitle.Appearance.ForeColor = System.Drawing.Color.FromArgb((CInt(((CByte((255)))))), (CInt(((CByte((216)))))), (CInt(((CByte((188)))))))
            Me.labelSubtitle.Appearance.Options.UseFont = True
            Me.labelSubtitle.Appearance.Options.UseForeColor = True
            Me.labelSubtitle.Location = New System.Drawing.Point(222, 131)
            Me.labelSubtitle.Name = "labelSubtitle"
            Me.labelSubtitle.Size = New System.Drawing.Size(64, 25)
            Me.labelSubtitle.TabIndex = 1
            Me.labelSubtitle.Text = "Subtitle"
            ' 
            ' labelApplicationName
            ' 
            Me.labelApplicationName.Appearance.Font = New System.Drawing.Font("Segoe UI", 26.25F)
            Me.labelApplicationName.Appearance.ForeColor = System.Drawing.SystemColors.Window
            Me.labelApplicationName.Appearance.Options.UseFont = True
            Me.labelApplicationName.Appearance.Options.UseForeColor = True
            Me.labelApplicationName.Location = New System.Drawing.Point(123, 84)
            Me.labelApplicationName.Name = "labelApplicationName"
            Me.labelApplicationName.Size = New System.Drawing.Size(278, 47)
            Me.labelApplicationName.TabIndex = 0
            Me.labelApplicationName.Text = "Application Name"
            ' 
            ' XafSplashScreen
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackColor = System.Drawing.Color.White
            Me.ClientSize = New System.Drawing.Size(496, 370)
            Me.Controls.Add(Me.pcApplicationName)
            Me.Controls.Add(Me.peImage)
            Me.Controls.Add(Me.peLogo)
            Me.Controls.Add(Me.labelStatus)
            Me.Controls.Add(Me.labelCopyright)
            Me.Controls.Add(Me.progressBarControl)
            Me.Name = "XafSplashScreen"
            Me.Padding = New System.Windows.Forms.Padding(1)
            Me.Text = "Form1"
            CType((Me.progressBarControl.Properties), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.peImage.Properties), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.peLogo.Properties), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.pcApplicationName), System.ComponentModel.ISupportInitialize).EndInit()
            Me.pcApplicationName.ResumeLayout(False)
            Me.pcApplicationName.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()
        End Sub

#End Region
        Private progressBarControl As DevExpress.XtraEditors.MarqueeProgressBarControl

        Private labelCopyright As DevExpress.XtraEditors.LabelControl

        Private labelStatus As DevExpress.XtraEditors.LabelControl

        Private peLogo As DevExpress.XtraEditors.PictureEdit

        Private peImage As DevExpress.XtraEditors.PictureEdit

        Private pcApplicationName As DevExpress.XtraEditors.PanelControl

        Private labelSubtitle As DevExpress.XtraEditors.LabelControl

        Private labelApplicationName As DevExpress.XtraEditors.LabelControl
    End Class
End Namespace
