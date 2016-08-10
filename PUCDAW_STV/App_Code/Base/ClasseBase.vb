Imports Microsoft.VisualBasic

Namespace STV.Base

    Public Class ClasseBase

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

    End Class

End Namespace
