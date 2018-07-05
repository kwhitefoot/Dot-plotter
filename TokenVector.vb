Imports Dotplotter

Public Class TokenVector

    Private ReadOnly _fileName As String

    Private ReadOnly _tokens As String()

    Public Sub New(fileName As String)
        _fileName = fileName
        _tokens = Tokenize(fileName)
        Debug.Assert(Not _tokens.Any(Function(token) token = ""))
    End Sub

    Friend Function Length() As Integer
        Return _tokens.Count
    End Function

    Private delim As New Text.RegularExpressions.Regex("\W+")

    Friend ReadOnly Property Token(i As Integer) As String
        Get
            Return _tokens(i)
        End Get
    End Property

    Private Function Tokenize(fileName As String) As String()

        Dim s = New IO.StreamReader(fileName).ReadToEnd
        Dim r = delim.Split(s)
        Return If(r.Last = "",
            r.Take(r.Count - 1).ToArray,
            r)

    End Function

End Class
