Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class frmMain
    Friend imgOriginal As Image
    Friend TokenVectorA As TokenVector
    Friend TokenVectorB As TokenVector

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim args = Environment.GetCommandLineArgs
        Const f = "d:\home\kj\Source\Workspaces\TSTAT\originalVB6\ptdt\tstat2001\source\common\br\MCommon.bas"
        'Dim f = "d:\home\kj\Source\Workspaces\TSTAT\TstatAll\TstatWCFInterfaces\WCFClient.vb"
        TokenVectorA = New TokenVector(f)
        TokenVectorB = TokenVectorA
        'TokenVectorB = New TokenVector(args(2))
        'TokenVectorB = New TokenVector(args(1))
        Dim bm = New Plot(TokenVectorA, TokenVectorA)
        txtHorizontalCount.Text = $"{TokenVectorA.Length}"
        txtVerticalCount.Text = $"{TokenVectorB.Length}"
        imgOriginal = bm.Bitmap

        picBox.Image = imgOriginal

        ' set Picture Box Attributes
        picBox.BackgroundImageLayout = ImageLayout.Stretch

        '' set Slider Attributes
        'zoomSlider.Minimum = 1
        'zoomSlider.Maximum = 5
        'zoomSlider.SmallChange = 1
        'zoomSlider.LargeChange = 1
        'zoomSlider.UseWaitCursor = False

        ' reduce flickering
        Me.DoubleBuffered = True
    End Sub

    Public Shared Function PictureBoxZoom(img As Image, size As Double) As Image
        Debug.Print($"Size: {img.Width * size}.")
        Dim bm As Bitmap = New Bitmap(img,
                                      CInt(img.Width * size),
                                      CInt(img.Height * size))
        Dim grap As Graphics = Graphics.FromImage(bm)
        grap.InterpolationMode = InterpolationMode.HighQualityBicubic
        Return bm
    End Function

    Private Sub DoZoom()
        txtZoom.Text = $"{zoom}"
        picBox.Image = Nothing
        picBox.Image = PictureBoxZoom(imgOriginal, zoom)
    End Sub

    Private Sub PicBox_MouseMove(sender As Object, e As MouseEventArgs) Handles picBox.MouseMove
        Debug.Print($"{e.Location.X}")
        'Dim x = e.X \ zoomSlider.Value
        'Dim y = e.Y \ zoomSlider.Value
        Dim x = CInt(e.X / zoom)
        Dim y = CInt(e.Y / zoom)

        txtHorizontalToken.Text = If(x < TokenVectorA.Length,
                                     $"{TokenVectorA.Token(x)}",
                                     "")
        txtVerticalToken.Text = If(y < TokenVectorB.Length,
                                     $"{TokenVectorB.Token(y)}",
                                     "")
    End Sub

    Private zoom As Double = 1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        zoom /= 2
        DoZoom()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        zoom *= 2
        DoZoom()
    End Sub

End Class
