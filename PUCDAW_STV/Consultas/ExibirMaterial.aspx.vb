
Imports System.IO
Imports STV.Entidades

Partial Class Consultas_ExibirMaterial : Inherits STV.Base.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Session("ArquivoPDF") Is Nothing Then Exit Sub

            Dim arquivo As FileInfo = Session("ArquivoPDF")
            Session("ArquivoPDF") = Nothing

            Response.Clear()
            Response.AddHeader("Content-Disposition", "inline;filename=" + arquivo.Name)
            Response.AddHeader("Content-Length", arquivo.Length.ToString())
            Response.ContentType = "Application/pdf"
            'Response.WriteFile(arquivo.FullName)
            Response.TransmitFile(arquivo.FullName)
            Response.End()
        Catch ex As Exception

        End Try

    End Sub
End Class
