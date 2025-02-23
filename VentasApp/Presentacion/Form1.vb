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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim nuevoProducto As New Producto(0, TextBox4.Text, Convert.ToDecimal(TextBox5.Text), TextBox6.Text)
            If ProductoDAL.AgregarProducto(nuevoProducto) Then
                MessageBox.Show("Producto agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("No se pudo agregar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Dim nuevaVenta As New Venta(0, Convert.ToInt32(TextBox7.Text), Convert.ToDateTime(TextBox8.Text), Convert.ToDecimal(TextBox9.Text))
            Dim items As New List(Of VentaItem)

            ' Crear ítems de prueba
            items.Add(New VentaItem(0, 0, 1, 500, 2, 1000))
            items.Add(New VentaItem(0, 0, 2, 300, 1, 300))

            If VentaDAL.AgregarVenta(nuevaVenta, items) Then
                MessageBox.Show("Venta registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("No se pudo registrar la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class