Namespace RuntimeDbChooser.[Module].Win

    Partial Class RuntimeDbChooserWindowsFormsModule

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

#Region "Component Designer generated code"
        ''' <summary> 
        ''' Required method for Designer support - do not modify 
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            ' 
            ' RuntimeDbChooserWindowsFormsModule
            ' 
            Me.RequiredModuleTypes.Add(GetType(RuntimeDbChooser.[Module].RuntimeDbChooserModule))
            Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule))
            Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule))
        End Sub
#End Region
    End Class
End Namespace
