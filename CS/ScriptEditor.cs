using System;
using System.Windows.Forms;
using DevExpress.XtraReports.Design;
using DevExpress.XtraReports.Scripting;

namespace ScriptEditorExample {
    class ScriptEditor : TextBox, IScriptEditor {
        public ScriptEditor() {
            Multiline = true;
            BorderStyle = System.Windows.Forms.BorderStyle.None;
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        }


        bool IScriptEditor.Modified {
            get { return base.Modified; }
            set { base.Modified = value; }
        }

        string IScriptEditor.Text {
            get { return base.Text; }
            set { base.Text = value; }
        }

        void IScriptEditor.AppendText(string text) {
            base.AppendText(text);
        }

        int IScriptEditor.LinesCount {
            get { return Lines.Length; }
        }

        void IScriptEditor.SetCaretPosition(int line, int column) {
            SetCaretPositionCore(line, column);
        }

        void SetCaretPositionCore(int line, int column) {
            int start = column;
            for(int i = 0; i < Lines.Length && i < line; i++) {
                start += Lines[i].Length;
                start += "\r\n".Length;
            }
            this.Focus();
            this.Select(start, 0);
        }

        void IScriptEditor.HighlightErrors(ScriptErrorCollection errors) {
            if(errors.Count == 0) return;
            int line = Math.Max(0, errors[0].Line - 1);
            int column = Math.Max(0, errors[0].Column - 1);
            BeginInvoke(new Action<int, int>(SetCaretPositionCore), line, column);
        }

        string IScriptEditor.GetText(int line) {
            return Lines[line];
        }
    }
    class ScriptEditorService : IScriptEditorService {
        IScriptEditor IScriptEditorService.CreateEditor(IServiceProvider serviceProvider) {
            return new ScriptEditor();
        }
    }
}
