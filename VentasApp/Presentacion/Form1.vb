Imports System.Data.SqlClient

Public Class Form1
    Private Sub btnProbarConexion_Click(sender As Object, e As EventArgs) Handles btnProbarConexion.Click
        Try
            Dim conexion As New ConexionBD()
            Dim con As SqlConnection = conexion.ObtenerConexion()

            con.Open()
            MessageBox.Show("Conexión exitosa a la base de datos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            con.Close()

        Catch ex As Exception
            MessageBox.Show("Error al conectar a la base de datos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class