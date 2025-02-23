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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim nuevoCliente As New Cliente(0, TextBox1.Text, TextBox2.Text, TextBox3.Text)
            If ClienteDAL.AgregarCliente(nuevoCliente) Then
                MessageBox.Show("Cliente agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("No se pudo agregar el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class