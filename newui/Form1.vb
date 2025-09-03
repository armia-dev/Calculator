Public Class Form1
    Dim currentInput As String = ""
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        TextBox1.TabStop = False
        TextBox1.ReadOnly = True
    End Sub

    ' Updator
    Private Sub UpdateDisplay()
        TextBox1.Text = currentInput
    End Sub

    ' Buttons
    Private Sub Button_Click(sender As Object, e As EventArgs) Handles btn0.Click, btn1.Click, btn2.Click, btn3.Click, btn4.Click,
        btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click, btnAdd.Click, btnSub.Click, btnMul.Click, btnDiv.Click, btnDot.Click

        Dim btn As Button = CType(sender, Button)
        Dim value As String = btn.Text

        If "0123456789".Contains(value) Then
            currentInput &= value

        ElseIf value = "." Then
            Dim lastNumber = GetLastNumber(currentInput)
            If Not lastNumber.Contains(".") Then
                currentInput &= "."
            End If

        ElseIf "+-x÷".Contains(value) Then
            If currentInput = "" OrElse "+-x÷".Contains(currentInput.Last()) Then Exit Sub
            currentInput &= value
        End If

        UpdateDisplay()
    End Sub

    ' Extract
    Private Function GetLastNumber(expr As String) As String
        Dim ops = "+-x÷"
        Dim lastOpIndex = expr.LastIndexOfAny(ops.ToCharArray())
        If lastOpIndex = -1 Then Return expr
        Return expr.Substring(lastOpIndex + 1)
    End Function

    ' Equal
    Private Sub btnEquals_Click(sender As Object, e As EventArgs) Handles btnEquals.Click
        Try
            Dim expr As String = currentInput.Replace("x", "*").Replace("÷", "/")
            Dim result = New DataTable().Compute(expr, Nothing)
            currentInput = result.ToString()
        Catch ex As Exception
            currentInput = "Error"
        End Try

        UpdateDisplay()
    End Sub

    ' Clear all input
    Private Sub btnCE_Click(sender As Object, e As EventArgs) Handles btnCE.Click
        currentInput = ""
        UpdateDisplay()
    End Sub

    ' Delete Button
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        If currentInput.Length > 0 Then
            currentInput = currentInput.Substring(0, currentInput.Length - 1)
            UpdateDisplay()
        End If
    End Sub

    ' Keyboard input
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Me.KeyPreview = True
        e.SuppressKeyPress = True

        Dim keyChar As String = ""

        Select Case e.KeyCode
            Case Keys.D0 To Keys.D9
                keyChar = (e.KeyCode - Keys.D0).ToString()
            Case Keys.NumPad0 To Keys.NumPad9
                keyChar = ChrW(e.KeyCode - Keys.NumPad0 + Asc("0"))

            Case Keys.Add, Keys.Oemplus
                keyChar = "+"
            Case Keys.Subtract, Keys.OemMinus
                keyChar = "-"
            Case Keys.Multiply
                keyChar = "x"
            Case Keys.Divide, Keys.OemQuestion
                keyChar = "÷"

            Case Keys.Decimal, Keys.OemPeriod
                Dim lastNumber = GetLastNumber(currentInput)
                If Not lastNumber.Contains(".") Then
                    keyChar = "."
                End If

            Case Keys.OemOpenBrackets
                keyChar = "("
            Case Keys.Oem6
                keyChar = ")"

            Case Keys.C
                btnCE.PerformClick()
                Exit Sub
            Case Keys.Enter, Keys.Oemplus
                btnEquals.PerformClick()
                Exit Sub
            Case Keys.Back
                btnDel.PerformClick()
                Exit Sub
        End Select

        ' Checker 
        If "0123456789".Contains(keyChar) Then
            currentInput &= keyChar
        ElseIf keyChar = "." Then
            currentInput &= keyChar
        ElseIf "+-x÷".Contains(keyChar) Then
            If currentInput = "" OrElse "+-x÷".Contains(currentInput.Last()) Then Exit Sub
            currentInput &= keyChar
        ElseIf "()".Contains(keyChar) Then
            currentInput &= keyChar
        End If

        UpdateDisplay()


    End Sub
End Class