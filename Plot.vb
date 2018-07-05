Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices

Friend Class Plot
    Private ReadOnly a As TokenVector
    Private ReadOnly b As TokenVector
    Private ReadOnly bm As Bitmap

    Public Sub New(a As TokenVector, b As TokenVector)
        Me.a = a
        Me.b = b
        Dim sw = New Stopwatch
        sw.Start()
        bm = MakeBitmapNaive()
        'bm = MakeBitmapLockAndMarshal()
        sw.Stop()
        Debug.Print($"Time: {sw.ElapsedMilliseconds}")
    End Sub

    Public Function MakeBitmapNaive() As Bitmap
        Dim bm = New Bitmap(a.Length, b.Length)
        For x = 0 To a.Length - 1
            Dim aTok = a.Token(x)
            For y = 0 To b.Length - 1
                Dim bTok = b.Token(y)
                If aTok = bTok Then
                    bm.SetPixel(x, y, Color.Black)
                End If
            Next
        Next
        Return bm
    End Function

    Public Function MakeBitmapLockAndMarshal() As Bitmap
        Dim bm = New Bitmap(a.Length, b.Length)
        Dim bmd = bm.LockBits(New Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadWrite, bm.PixelFormat)
        Dim bpp = Image.GetPixelFormatSize(bm.PixelFormat) \ 8
        Dim bc = bmd.Stride * bm.Height
        Dim pixels(bc) As Byte
        Dim ptfp = bmd.Scan0
        Marshal.Copy(ptfp, pixels, 0, pixels.Length)
        Dim hips = bmd.Height
        Dim wibs = bmd.Width * bpp

        For y = 0 To a.Length - 1
            Dim bTok = b.Token(y)
            Dim cl = y * bmd.Stride
            For x = 0 To b.Length - 1
                Dim aTok = a.Token(x)
                If aTok = bTok Then
                    'bm.SetPixel(x, y, Color.Black)
                    Dim xp = x * bpp
                    pixels(cl + xp) = 0
                    pixels(cl + xp + 1) = 0
                    pixels(cl + xp + 1) = 0

                End If
            Next
        Next
        Marshal.Copy(pixels, 0, ptfp, pixels.Length)
        bm.UnlockBits(bmd)

        Return bm

    End Function

    Friend Function Bitmap() As Bitmap
        Return bm
    End Function

End Class
