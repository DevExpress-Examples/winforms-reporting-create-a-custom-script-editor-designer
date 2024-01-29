Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraReports.UI

Namespace ScriptEditorExample

    Public Partial Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim tool As ReportDesignTool = New ReportDesignTool(New XtraReport())
            tool.DesignForm.DesignMdiController.AddService(GetType(DevExpress.XtraReports.Design.IScriptEditorService), New ScriptEditorService())
            tool.ShowDesignerDialog()
        End Sub
    End Class
End Namespace
