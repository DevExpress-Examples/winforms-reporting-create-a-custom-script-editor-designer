Imports System
Imports System.Windows.Forms
Imports DevExpress.XtraReports.Design

Namespace ScriptEditorExample

    Friend Class ScriptEditor
        Inherits TextBox
        Implements IScriptEditor

        Public Sub New()
            Multiline = True
            BorderStyle = BorderStyle.None
            Font = New System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        End Sub

'#Region "ISyntaxEditor Members"
        Private Property IScriptEditor_Modified As Boolean Implements IScriptEditor.Modified
            Get
                Return Modified
            End Get

            Set(ByVal value As Boolean)
                Modified = value
            End Set
        End Property

        Private Property IScriptEditor_Text As String Implements IScriptEditor.Text
            Get
                Return Text
            End Get

            Set(ByVal value As String)
                Text = value
            End Set
        End Property

        Private Sub IScriptEditor_AppendText(ByVal text As String) Implements IScriptEditor.AppendText
            AppendText(text)
        End Sub

        Private ReadOnly Property LinesCount As Integer Implements IScriptEditor.LinesCount
            Get
                Return Lines.Length
            End Get
        End Property

        Private Sub SetCaretPosition(ByVal line As Integer, ByVal column As Integer) Implements IScriptEditor.SetCaretPosition
            SetCaretPositionCore(line, column)
        End Sub

        Private Sub SetCaretPositionCore(ByVal line As Integer, ByVal column As Integer)
            Dim start As Integer = column
            Dim i As Integer = 0
            While i < Lines.Length AndAlso i < line
                start += Lines(i).Length
                start += Microsoft.VisualBasic.Constants.vbCrLf.Length
                i += 1
            End While

            Focus()
            [Select](start, 0)
        End Sub

        Private Sub HighlightErrors(ByVal errors As CodeDom.Compiler.CompilerErrorCollection) Implements IScriptEditor.HighlightErrors
            If errors.Count = 0 Then Return
            Dim line As Integer = Math.Max(0, errors(0).Line - 1)
            Dim column As Integer = Math.Max(0, errors(0).Column - 1)
            BeginInvoke(New Action(Of Integer, Integer)(AddressOf SetCaretPositionCore), line, column)
        End Sub
'#End Region
    End Class

    Friend Class ScriptEditorService
        Implements IScriptEditorService

        Private Function CreateEditor(ByVal serviceProvider As IServiceProvider) As IScriptEditor Implements IScriptEditorService.CreateEditor
            Return New ScriptEditor()
        End Function
    End Class
End Namespace
