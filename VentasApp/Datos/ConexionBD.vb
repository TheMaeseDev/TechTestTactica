Imports System.Data.SqlClient
Imports System.Configuration

Public Class ConexionBD
    Private conexion As SqlConnection

    Public Sub New()
        Dim cadenaConexion As String = ConfigurationManager.ConnectionStrings("MiConexion").ConnectionString
        conexion = New SqlConnection(cadenaConexion)
    End Sub

    Public Function ObtenerConexion() As SqlConnection
        Return conexion
    End Function
End Class
