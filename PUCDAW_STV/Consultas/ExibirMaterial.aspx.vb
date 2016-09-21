
Imports System.IO

Partial Class Consultas_ExibirMaterial : Inherits STV.Base.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Session("ArquivoPDF") Is Nothing Then Exit Sub

            Dim arquivo As FileInfo = Session("ArquivoPDF")

            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment;filename=" + arquivo.Name)
            Response.AddHeader("Content-Length", arquivo.Length.ToString())
            Response.ContentType = "application/pdf"
            'Response.WriteFile(arquivo.FullName)
            Response.TransmitFile(arquivo.FullName)
            Response.Flush()
            Response.End()
        Catch ex As Exception

        End Try

    End Sub
End Class
