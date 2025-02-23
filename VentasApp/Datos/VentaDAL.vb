Imports System.Data.SqlClient
Imports System.Configuration
Imports Entidades

Public Class VentaDAL
    ' Método para obtener la conexión desde App.config
    Private Shared Function ObtenerConexion() As SqlConnection
        Dim conexion As String = ConfigurationManager.ConnectionStrings("MiConexion").ConnectionString
        Return New SqlConnection(conexion)
    End Function

    ' Método para registrar una nueva venta con sus ítems
    Public Shared Function AgregarVenta(venta As Venta, items As List(Of VentaItem)) As Boolean
        Dim transaction As SqlTransaction = Nothing

        Try
            Using con As SqlConnection = ObtenerConexion()
                con.Open()
                transaction = con.BeginTransaction()

                ' Insertar la venta
                Dim queryVenta As String = "INSERT INTO ventas (idcliente, fecha, total) OUTPUT INSERTED.id VALUES (@idcliente, @fecha, @total)"
                Dim idVenta As Integer

                Using cmdVenta As New SqlCommand(queryVenta, con, transaction)
                    cmdVenta.Parameters.AddWithValue("@idcliente", venta.IdCliente)
                    cmdVenta.Parameters.AddWithValue("@fecha", venta.Fecha)
                    cmdVenta.Parameters.AddWithValue("@total", venta.Total)

                    idVenta = Convert.ToInt32(cmdVenta.ExecuteScalar())
                End Using

                ' Insertar los ítems de la venta
                For Each item As VentaItem In items
                    Dim queryItem As String = "INSERT INTO ventasitems (idventa, idproducto, precioUnitario, cantidad, precioTotal) VALUES (@idventa, @idproducto, @precioUnitario, @cantidad, @precioTotal)"

                    Using cmdItem As New SqlCommand(queryItem, con, transaction)
                        cmdItem.Parameters.AddWithValue("@idventa", idVenta)
                        cmdItem.Parameters.AddWithValue("@idproducto", item.IdProducto)
                        cmdItem.Parameters.AddWithValue("@precioUnitario", item.PrecioUnitario)
                        cmdItem.Parameters.AddWithValue("@cantidad", item.Cantidad)
                        cmdItem.Parameters.AddWithValue("@precioTotal", item.PrecioTotal)

                        cmdItem.ExecuteNonQuery()
                    End Using
                Next

                transaction.Commit()
                Return True
            End Using
        Catch ex As Exception
            If transaction IsNot Nothing Then transaction.Rollback()
            Throw New Exception("Error al agregar la venta: " & ex.Message)
        End Try
    End Function

    ' Método para obtener todas las ventas
    Public Shared Function ObtenerVentas() As List(Of Venta)
        Dim listaVentas As New List(Of Venta)()

        Try
            Using con As SqlConnection = ObtenerConexion()
                con.Open()
                Dim query As String = "SELECT id, idcliente, fecha, total FROM ventas"
                Using cmd As New SqlCommand(query, con)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            listaVentas.Add(New Venta(reader("id"), reader("idcliente"), Convert.ToDateTime(reader("fecha")), Convert.ToDecimal(reader("total"))))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al obtener ventas: " & ex.Message)
        End Try

        Return listaVentas
    End Function
End Class
