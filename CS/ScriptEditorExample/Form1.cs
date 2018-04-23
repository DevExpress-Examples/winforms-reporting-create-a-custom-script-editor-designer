using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;

namespace ScriptEditorExample {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            ReportDesignTool tool = new ReportDesignTool(new XtraReport());
            tool.DesignForm.DesignMdiController.AddService(typeof(DevExpress.XtraReports.Design.IScriptEditorService), new ScriptEditorService());
            tool.ShowDesignerDialog();
        }
    }
}
