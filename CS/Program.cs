using System;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;

namespace ScriptEditorExample {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var report = new XtraReport();
            report.ScriptsSource = "// write code here\r\n";
            report.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.CSharp;
            using(report)
            using(var tool = new ReportDesignTool(report)) {
                var form = tool.DesignRibbonForm;
                form.DesignMdiController.AddService(typeof(DevExpress.XtraReports.Design.IScriptEditorService), new ScriptEditorService());
                form.DesignDockManager.Load += (s, e) => form.DesignMdiController.ActiveDesignPanel.ExecCommand(DevExpress.XtraReports.UserDesigner.ReportCommand.ShowScriptsTab);
                tool.ShowRibbonDesignerDialog();
            }
        }
    }
}
