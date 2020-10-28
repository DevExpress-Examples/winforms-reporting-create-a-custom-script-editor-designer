Imports System
Imports System.Windows.Forms
Imports DevExpress.XtraReports.UI

Namespace ScriptEditorExample
	Friend Module Program
		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread>
		Sub Main()
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)

			Dim report = New XtraReport()
			report.ScriptsSource = "// write code here" & vbCrLf
			report.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.CSharp
			Using report
			Using tool = New ReportDesignTool(report)
				Dim form = tool.DesignRibbonForm
				form.DesignMdiController.AddService(GetType(DevExpress.XtraReports.Design.IScriptEditorService), New ScriptEditorService())
'INSTANT VB NOTE: An underscore by itself is not a valid identifier in VB:
'ORIGINAL LINE: form.DesignDockManager.Load += (_, e) => form.DesignMdiController.ActiveDesignPanel.ExecCommand(DevExpress.XtraReports.UserDesigner.ReportCommand.ShowScriptsTab);
				AddHandler form.DesignDockManager.Load, Sub(underscore, e) form.DesignMdiController.ActiveDesignPanel.ExecCommand(DevExpress.XtraReports.UserDesigner.ReportCommand.ShowScriptsTab)
				tool.ShowRibbonDesignerDialog()
			End Using
			End Using
		End Sub
	End Module
End Namespace
