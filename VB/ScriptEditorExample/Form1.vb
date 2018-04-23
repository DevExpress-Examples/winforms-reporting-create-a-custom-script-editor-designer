Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports DevExpress.XtraReports.UI

Namespace ScriptEditorExample
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
            Dim tool As New ReportDesignTool(New XtraReport())
            tool.DesignForm.DesignMdiController.AddService(GetType(DevExpress.XtraReports.Design.IScriptEditorService), New ScriptEditorService())
            tool.ShowDesignerDialog()
        End Sub
    End Class
End Namespace
