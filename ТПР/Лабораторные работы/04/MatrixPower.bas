Attribute VB_Name = "MatrixPower"
Function ляреоемэ(Matrix As Range, Power As Long) As Variant
    Dim res As Variant
    Dim square As Variant
    Dim i As Long
    
    square = Matrix
    
    For i = 0 To 31
        If (Power And 2 ^ i) Then
            If IsEmpty(res) Then
                res = square
            Else
                res = Application.WorksheetFunction.MMult(square, res)
            End If
        End If
        If 2 ^ i >= Power Then Exit For
        square = Application.WorksheetFunction.MMult(square, square)
    Next
    ляреоемэ = res
End Function
