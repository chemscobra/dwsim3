Module Extensions

    <System.Runtime.CompilerServices.Extension()> _
    Public Sub UIThread(control As Control, code As Action)
        If control.InvokeRequired Then
            control.BeginInvoke(code)
        Else
            code.Invoke()
        End If
    End Sub

    <System.Runtime.CompilerServices.Extension()> _
    Public Sub UIThreadInvoke(control As Control, code As Action)
        If control.InvokeRequired Then
            control.Invoke(code)
        Else
            code.Invoke()
        End If
    End Sub

    <System.Runtime.CompilerServices.Extension()> _
    Public Function DropDownWidth(control As ListView) As Integer
        Dim maxWidth As Integer = 0, temp As Integer = 0
        For Each obj As Object In control.Items
            temp = TextRenderer.MeasureText(obj.ToString(), control.Font).Width
            If temp > maxWidth Then
                maxWidth = temp
            End If
        Next
        Return maxWidth
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function DropDownHeight(control As ListView) As Integer
        Dim Height As Integer = 0, temp As Integer = 0
        For Each obj As Object In control.Items
            temp = TextRenderer.MeasureText(obj.ToString(), control.Font).Height
            Height += temp
        Next
        Return Height
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function ToArrayString(vector As Double()) As String

        Dim retstr As String = "{ "
        For Each d In vector
            retstr += d.ToString + ", "
        Next
        retstr.TrimEnd(",")
        retstr += "}"

        Return retstr

    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function ToArrayString(vector As String()) As String

        Dim retstr As String = "{ "
        For Each s In vector
            retstr += s + ", "
        Next
        retstr.TrimEnd(",")
        retstr += "}"

        Return retstr

    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function ToArrayString(vector As Object()) As String

        Dim retstr As String = "{ "
        For Each d In vector
            retstr += d.ToString + ", "
        Next
        retstr.TrimEnd(",")
        retstr += "}"

        Return retstr

    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function ToArrayString(vector As Array) As String

        Dim retstr As String = "{ "
        For Each d In vector
            retstr += d.ToString + ", "
        Next
        retstr.TrimEnd(",")
        retstr += "}"

        Return retstr

    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Sub PasteData(dgv As DataGridView)

        Dim tArr() As String
        Dim arT() As String
        Dim i, ii As Integer
        Dim c, cc, r As Integer

        tArr = Clipboard.GetText().Split(New Char() {vbLf, vbCr, vbCrLf})

        If dgv.SelectedCells.Count > 0 Then
            r = dgv.SelectedCells(0).RowIndex
            c = dgv.SelectedCells(0).ColumnIndex
        Else
            r = 0
            c = 0
        End If
        For i = 0 To tArr.Length - 1
            If tArr(i) <> "" Then
                arT = tArr(i).Split(vbTab)
                For ii = 0 To arT.Length - 1
                    If r > dgv.Rows.Count - 1 Then
                        'dgv.Rows.Add()
                        'dgv.Rows(0).Cells(0).Value = True
                        'dgv.Rows(0).Cells(1).Selected = True
                    End If
                Next
                r = r + 1
            End If
        Next
        If dgv.SelectedCells.Count > 0 Then
            r = dgv.SelectedCells(0).RowIndex
            c = dgv.SelectedCells(0).ColumnIndex
        Else
            r = 0
            c = 0
        End If
        For i = 0 To tArr.Length - 1
            If tArr(i) <> "" Then
                arT = tArr(i).Split(vbTab)
                cc = c
                If r <= dgv.Rows.Count - 1 Then
                    For ii = 0 To arT.Length - 1
                        cc = GetNextVisibleCol(dgv, cc)
                        If cc > dgv.ColumnCount - 1 Then Exit For
                        dgv.Item(cc, r).Value = arT(ii).TrimStart
                        cc = cc + 1
                    Next
                End If
                r = r + 1
            End If
        Next

    End Sub
    Function GetNextVisibleCol(dgv As DataGridView, stidx As Integer) As Integer

        Dim i As Integer

        For i = stidx To dgv.ColumnCount - 1
            If dgv.Columns(i).Visible Then Return i
        Next

        Return Nothing

    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsValid(d As Double) As Boolean
        If Double.IsNaN(d) Or Double.IsInfinity(d) Then Return False Else Return True
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsValid(d As Nullable(Of Double)) As Boolean
        If Double.IsNaN(d.GetValueOrDefault) Or Double.IsInfinity(d.GetValueOrDefault) Then Return False Else Return True
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsPositive(d As Double) As Boolean
        If d.IsValid() Then
            If d > 0.0# Then Return True Else Return False
        Else
            Throw New ArgumentException("invalid double")
        End If
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsPositive(d As Nullable(Of Double)) As Boolean
        If d.GetValueOrDefault.IsValid() Then
            If d.GetValueOrDefault > 0.0# Then Return True Else Return False
        Else
            Throw New ArgumentException("invalid double")
        End If
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsNegative(d As Double) As Boolean
        If d.IsValid() Then
            If d < 0.0# Then Return True Else Return False
        Else
            Throw New ArgumentException("invalid double")
        End If
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsNegative(d As Nullable(Of Double)) As Boolean
        If d.GetValueOrDefault.IsValid() Then
            If d.GetValueOrDefault < 0.0# Then Return True Else Return False
        Else
            Throw New ArgumentException("invalid double")
        End If
    End Function

    ''' <summary>
    ''' Alternative implementation for the Exponential (Exp) function.
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()> Public Function ExpY(val As Double) As Double
        Dim tmp As Long = CLng(1512775 * val + 1072632447)
        Return BitConverter.Int64BitsToDouble(tmp << 32)
    End Function

    ''' <summary>
    ''' Computes the exponential of each vector element.
    ''' </summary>
    ''' <param name="vector"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()> Public Function ExpY(vector As Double()) As Double()

        Dim vector2(vector.Length - 1) As Double

        Yeppp.Math.Exp_V64f_V64f(vector, 0, vector2, 0, vector.Length)

        Return vector2

    End Function

    ''' <summary>
    ''' Sum of the vector elements.
    ''' </summary>
    ''' <param name="vector"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()> Public Function SumY(vector As Double()) As Double

        Return Yeppp.Core.Sum_V64f_S64f(vector, 0, vector.Length)

    End Function

    ''' <summary>
    ''' Absolute sum of the vector elements
    ''' </summary>
    ''' <param name="vector"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()> Public Function AbsSumY(vector As Double()) As Double

        Return Yeppp.Core.SumAbs_V64f_S64f(vector, 0, vector.Length)

    End Function

    ''' <summary>
    ''' Absolute square sum of vector elements.
    ''' </summary>
    ''' <param name="vector"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()> Public Function AbsSqrSumY(vector As Double()) As Double

        Return Yeppp.Core.SumSquares_V64f_S64f(vector, 0, vector.Length)

    End Function

    ''' <summary>
    ''' Multiplies vector elements.
    ''' </summary>
    ''' <param name="vector"></param>
    ''' <param name="vector2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()> Public Function MultiplyY(vector As Double(), vector2 As Double()) As Double()

        Dim vector0(vector.Length - 1) As Double

        Yeppp.Core.Multiply_V64fV64f_V64f(vector, 0, vector2, 0, vector0, 0, vector.Length)

        Return vector0

    End Function

    ''' <summary>
    ''' Subtracts vector elements.
    ''' </summary>
    ''' <param name="vector"></param>
    ''' <param name="vector2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()> Public Function SubtractY(vector As Double(), vector2 As Double()) As Double()

        Dim vector0(vector.Length - 1) As Double

        Yeppp.Core.Subtract_V64fV64f_V64f(vector, 0, vector2, 0, vector0, 0, vector.Length)

        Return vector0

    End Function

    ''' <summary>
    ''' Multiplies vector elements by a constant.
    ''' </summary>
    ''' <param name="vector"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()> Public Function MultiplyConstY(vector As Double(), constant As Double) As Double()

        Dim vector0(vector.Length - 1) As Double

        Yeppp.Core.Multiply_V64fS64f_V64f(vector, 0, constant, vector0, 0, vector.Length)

        Return vector0

    End Function

    ''' <summary>
    ''' Multiplies vector elements by a constant.
    ''' </summary>
    ''' <param name="vector"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()> Public Function NormalizeY(vector As Double()) As Double()

        Dim vector0(vector.Length - 1) As Double

        Dim sum As Double = Yeppp.Core.SumAbs_V64f_S64f(vector, 0, vector.Length)

        Yeppp.Core.Multiply_V64fS64f_V64f(vector, 0, 1 / sum, vector0, 0, vector.Length)

        Return vector0

    End Function


    ''' <summary>
    ''' Adds vector elements.
    ''' </summary>
    ''' <param name="vector"></param>
    ''' <param name="vector2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()> Public Function AddY(vector As Double(), vector2 As Double()) As Double()

        Dim vector0 As Double() = vector.Clone()

        Yeppp.Core.Add_IV64fV64f_IV64f(vector0, 0, vector2, 0, vector.Length)

        Return vector0

    End Function

End Module
