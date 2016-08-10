Imports Microsoft.VisualBasic
Imports STV

Namespace STV.Base

    Public Class MasterPage : Inherits System.Web.UI.MasterPage

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

    End Class

End Namespace
