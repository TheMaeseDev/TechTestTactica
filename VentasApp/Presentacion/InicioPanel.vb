Imports System.Data.SqlClient

Public Class InicioPanel

    Public Function CrearPanel() As Panel
        Dim panel As New Panel With {.Dock = DockStyle.Fill}

        Dim btnTestConnexion As New Button With {.Text = "Probar Conexion", .Top = 320, .Left = 10, .Width = 100, .Height = 30}
        AddHandler btnTestConnexion.Click, AddressOf ProbarConexion

        panel.Controls.Add(btnTestConnexion)

        Return panel
    End Function

    Private Sub ProbarConexion(sender As Object, e As EventArgs)
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