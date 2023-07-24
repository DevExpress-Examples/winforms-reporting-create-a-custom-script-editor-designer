Imports System
Imports System.Windows.Forms
Imports DevExpress.XtraReports.Design
Imports DevExpress.XtraReports.Scripting

Namespace ScriptEditorExample
	Friend Class ScriptEditor
		Inherits TextBox
		Implements IScriptEditor

		Public Sub New()
			Multiline = True
			BorderStyle = System.Windows.Forms.BorderStyle.None
			Font = New System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
		End Sub


		Private Property IScriptEditor_Modified() As Boolean Implements IScriptEditor.Modified
			Get
				Return MyBase.Modified
			End Get
			Set(ByVal value As Boolean)
				MyBase.Modified = value
			End Set
		End Property

		Private Property IScriptEditor_Text() As String Implements IScriptEditor.Text
			Get
				Return MyBase.Text
			End Get
			Set(ByVal value As String)
				MyBase.Text = value
			End Set
		End Property

		Private Sub IScriptEditor_AppendText(ByVal text As String) Implements IScriptEditor.AppendText
			MyBase.AppendText(text)
		End Sub

		Private ReadOnly Property IScriptEditor_LinesCount() As Integer Implements IScriptEditor.LinesCount
			Get
				Return Lines.Length
			End Get
		End Property

		Private Sub IScriptEditor_SetCaretPosition(ByVal line As Integer, ByVal column As Integer) Implements IScriptEditor.SetCaretPosition
			SetCaretPositionCore(line, column)
		End Sub

		Private Sub SetCaretPositionCore(ByVal line As Integer, ByVal column As Integer)
			Dim start As Integer = column
			Dim i As Integer = 0
			Do While i < Lines.Length AndAlso i < line
				start += Lines(i).Length
				start += vbCrLf.Length
				i += 1
			Loop
			Me.Focus()
			Me.Select(start, 0)
		End Sub

		Private Sub IScriptEditor_HighlightErrors(ByVal errors As ScriptErrorCollection) Implements IScriptEditor.HighlightErrors
			If errors.Count = 0 Then
				Return
			End If
			Dim line As Integer = Math.Max(0, errors(0).Line - 1)
			Dim column As Integer = Math.Max(0, errors(0).Column - 1)
			BeginInvoke(New Action(Of Integer, Integer)(AddressOf SetCaretPositionCore), line, column)
		End Sub

		Private Function IScriptEditor_GetText(ByVal line As Integer) As String Implements IScriptEditor.GetText
			Return Lines(line)
		End Function
	End Class
	Friend Class ScriptEditorService
		Implements IScriptEditorService

		Private Function IScriptEditorService_CreateEditor(ByVal serviceProvider As IServiceProvider) As IScriptEditor Implements IScriptEditorService.CreateEditor
			Return New ScriptEditor()
		End Function
	End Class
End Namespace
