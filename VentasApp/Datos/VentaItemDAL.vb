Imports System.Data.SqlClient
Imports System.Configuration
Imports Entidades

Public Class VentaItemDAL
    ' Método para obtener la conexión desde App.config
    Private Shared Function ObtenerConexion() As SqlConnection
        Dim conexion As String = ConfigurationManager.ConnectionStrings("MiConexion").ConnectionString
        Return New SqlConnection(conexion)
    End Function

    ' Método para obtener los ítems de una venta específica
    Public Shared Function ObtenerItemsPorVenta(idVenta As Integer) As List(Of VentaItem)
        Dim listaItems As New List(Of VentaItem)()

        Try
            Using con As SqlConnection = ObtenerConexion()
                con.Open()
                Dim query As String = "SELECT id, idventa, idproducto, precioUnitario, cantidad, precioTotal FROM ventasitems WHERE idventa = @idventa"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@idventa", idVenta)

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            listaItems.Add(New VentaItem(reader("id"), reader("idventa"), reader("idproducto"), Convert.ToDecimal(reader("precioUnitario")), Convert.ToInt32(reader("cantidad")), Convert.ToDecimal(reader("precioTotal"))))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al obtener ítems de la venta: " & ex.Message)
        End Try

        Return listaItems
    End Function
End Class
