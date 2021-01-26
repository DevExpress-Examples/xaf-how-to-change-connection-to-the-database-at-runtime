Public Partial Class XafSplashScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XafSplashScreen))
        Me.peImage = New DevExpress.XtraEditors.PictureEdit()
        Me.peLogo = New DevExpress.XtraEditors.PictureEdit()
        Me.labelStatus = New DevExpress.XtraEditors.LabelControl()
        Me.labelCopyright = New DevExpress.XtraEditors.LabelControl()
        Me.progressBarControl = New DevExpress.XtraEditors.MarqueeProgressBarControl()
        Me.pcApplicationName = New DevExpress.XtraEditors.PanelControl()
        Me.lableSubtitle = New DevExpress.XtraEditors.LabelControl()
        Me.labelApplicationName = New DevExpress.XtraEditors.LabelControl()
        CType(Me.peImage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.peLogo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarControl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pcApplicationName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pcApplicationName.SuspendLayout()
        Me.SuspendLayout()
        '
        'peImage
        '
        Me.peImage.Cursor = System.Windows.Forms.Cursors.Default
        Me.peImage.EditValue = CType(resources.GetObject("peImage.EditValue"), Object)
        Me.peImage.Location = New System.Drawing.Point(12, 12)
        Me.peImage.Name = "peImage"
        Me.peImage.Properties.AllowFocused = False
        Me.peImage.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.peImage.Properties.Appearance.Options.UseBackColor = True
        Me.peImage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.peImage.Properties.ShowMenu = False
        Me.peImage.Size = New System.Drawing.Size(426, 180)
        Me.peImage.TabIndex = 14
        '
        'peLogo
        '
        Me.peLogo.Cursor = System.Windows.Forms.Cursors.Default
        Me.peLogo.EditValue = CType(resources.GetObject("peLogo.EditValue"), Object)
        Me.peLogo.Location = New System.Drawing.Point(400, 328)
        Me.peLogo.Name = "peLogo"
        Me.peLogo.Properties.AllowFocused = False
        Me.peLogo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.peLogo.Properties.Appearance.Options.UseBackColor = True
        Me.peLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.peLogo.Properties.ShowMenu = False
        Me.peLogo.Size = New System.Drawing.Size(70, 20)
        Me.peLogo.TabIndex = 13
        '
        'labelStatus
        '
        Me.labelStatus.Location = New System.Drawing.Point(75, 253)
        Me.labelStatus.Name = "labelStatus"
        Me.labelStatus.Size = New System.Drawing.Size(50, 13)
        Me.labelStatus.TabIndex = 12
        Me.labelStatus.Text = "Starting..."
        '
        'labelCopyright
        '
        Me.labelCopyright.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.labelCopyright.Location = New System.Drawing.Point(24, 324)
        Me.labelCopyright.Name = "labelCopyright"
        Me.labelCopyright.Size = New System.Drawing.Size(47, 13)
        Me.labelCopyright.TabIndex = 11
        Me.labelCopyright.Text = "Copyright"
        '
        'progressBarControl
        '
        Me.progressBarControl.EditValue = 0
        Me.progressBarControl.Location = New System.Drawing.Point(74, 271)
        Me.progressBarControl.Name = "progressBarControl"
        Me.progressBarControl.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(195, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(194, Byte), Integer))
        Me.progressBarControl.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.progressBarControl.Properties.EndColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.progressBarControl.Properties.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.progressBarControl.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
        Me.progressBarControl.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.progressBarControl.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid
        Me.progressBarControl.Properties.StartColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.progressBarControl.Size = New System.Drawing.Size(350, 16)
        Me.progressBarControl.TabIndex = 10
        '
        'pcApplicationName
        '
        Me.pcApplicationName.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pcApplicationName.Appearance.Options.UseBackColor = True
        Me.pcApplicationName.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pcApplicationName.Controls.Add(Me.lableSubtitle)
        Me.pcApplicationName.Controls.Add(Me.labelApplicationName)
		Me.pcApplicationName.Dock = System.Windows.Forms.DockStyle.Top
        Me.pcApplicationName.Location = New System.Drawing.Point(0, 0)
        Me.pcApplicationName.Name = "pcApplicationName"
        Me.pcApplicationName.Size = New System.Drawing.Size(496, 220)
        Me.pcApplicationName.TabIndex = 15
        '
        'lableSubtitle
        '
        Me.lableSubtitle.Appearance.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.lableSubtitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.lableSubtitle.Appearance.Options.UseFont = True
        Me.lableSubtitle.Appearance.Options.UseForeColor = True
        Me.lableSubtitle.Location = New System.Drawing.Point(215, 131)
        Me.lableSubtitle.Name = "lableSubtitle"
        Me.lableSubtitle.Size = New System.Drawing.Size(64, 25)
        Me.lableSubtitle.TabIndex = 1
        Me.lableSubtitle.Text = "Subtitle"
        '
        'labelApplicationName
        '
        Me.labelApplicationName.Appearance.Font = New System.Drawing.Font("Segoe UI", 26.25!)
        Me.labelApplicationName.Appearance.ForeColor = System.Drawing.Color.White
        Me.labelApplicationName.Appearance.Options.UseFont = True
        Me.labelApplicationName.Appearance.Options.UseForeColor = True
        Me.labelApplicationName.Location = New System.Drawing.Point(108, 84)
        Me.labelApplicationName.Name = "labelApplicationName"
        Me.labelApplicationName.Size = New System.Drawing.Size(278, 47)
        Me.labelApplicationName.TabIndex = 0
        Me.labelApplicationName.Text = "Application Name"
        '
        'XafSplashScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
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
        Me.Text = "Form1"
        CType(Me.peImage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.peLogo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarControl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pcApplicationName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pcApplicationName.ResumeLayout(False)
        Me.pcApplicationName.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents peImage As DevExpress.XtraEditors.PictureEdit
    Private WithEvents peLogo As DevExpress.XtraEditors.PictureEdit
    Private WithEvents labelStatus As DevExpress.XtraEditors.LabelControl
    Private WithEvents labelCopyright As DevExpress.XtraEditors.LabelControl
    Private WithEvents progressBarControl As DevExpress.XtraEditors.MarqueeProgressBarControl
    Friend WithEvents pcApplicationName As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lableSubtitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents labelApplicationName As DevExpress.XtraEditors.LabelControl
End Class
