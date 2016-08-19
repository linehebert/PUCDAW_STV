Imports Microsoft.VisualBasic
Imports STV.Entidades

Namespace STV.Base

    Public Class Page : Inherits System.Web.UI.Page

        Dim _Biblio As Biblio
        Public ReadOnly Property Biblio As Biblio
            Get
                If IsNothing(_Biblio) Then _
                    _Biblio = New Biblio(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)

                Return _Biblio
            End Get
        End Property

        Dim _Util As Util
        Public ReadOnly Property Util As Util
            Get
                If IsNothing(_Util) Then _
                    _Util = New Util

                Return _Util
            End Get
        End Property

        Public Sub RegistrarScript(Script As String)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, Guid.NewGuid.ToString, Script, True)
        End Sub

        Public Enum Tipo_Operacao_Cadastro
            Inclusao
            Edicao
            Exclusao
        End Enum

        Public Overrides Property StyleSheetTheme() As String
            Get
                Return (New Layout()).Retorna_Tema()
            End Get
            Set(value As String)
            End Set
        End Property

    End Class

End Namespace