Imports System.Data.SqlClient
Imports System.Configuration

Public Class ProductoDAL
    ' Método para obtener la conexión desde App.config
    Private Shared Function ObtenerConexion() As SqlConnection
        Dim conexion As String = ConfigurationManager.ConnectionStrings("MiConexion").ConnectionString
        Return New SqlConnection(conexion)
    End Function

    ' Método para agregar un producto
    Public Shared Function AgregarProducto(producto As Producto) As Boolean
        Try
            Using con As SqlConnection = ObtenerConexion()
                con.Open()
                Dim query As String = "INSERT INTO productos (nombre, precio, categoria) VALUES (@nombre, @precio, @categoria)"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@nombre", producto.Nombre)
                    cmd.Parameters.AddWithValue("@precio", producto.Precio)
                    cmd.Parameters.AddWithValue("@categoria", producto.Categoria)

                    Dim filasAfectadas As Integer = cmd.ExecuteNonQuery()
                    Return filasAfectadas > 0
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al agregar producto: " & ex.Message)
        End Try
    End Function

    ' Método para obtener todos los productos
    Public Shared Function ObtenerProductos() As List(Of Producto)
        Dim listaProductos As New List(Of Producto)()

        Try
            Using con As SqlConnection = ObtenerConexion()
                con.Open()
                Dim query As String = "SELECT id, nombre, precio, categoria FROM productos"
                Using cmd As New SqlCommand(query, con)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            listaProductos.Add(New Producto(reader("id"), reader("nombre").ToString(), Convert.ToDecimal(reader("precio")), reader("categoria").ToString()))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al obtener productos: " & ex.Message)
        End Try

        Return listaProductos
    End Function

    ' Método para actualizar un producto
    Public Shared Function ActualizarProducto(producto As Producto) As Boolean
        Try
            Using con As SqlConnection = ObtenerConexion()
                con.Open()
                Dim query As String = "UPDATE productos SET nombre = @nombre, precio = @precio, categoria = @categoria WHERE id = @id"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@id", producto.Id)
                    cmd.Parameters.AddWithValue("@nombre", producto.Nombre)
                    cmd.Parameters.AddWithValue("@precio", producto.Precio)
                    cmd.Parameters.AddWithValue("@categoria", producto.Categoria)

                    Dim filasAfectadas As Integer = cmd.ExecuteNonQuery()
                    Return filasAfectadas > 0
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al actualizar producto: " & ex.Message)
        End Try
    End Function

    ' Método para eliminar un producto
    Public Shared Function EliminarProducto(id As Integer) As Boolean
        Try
            Using con As SqlConnection = ObtenerConexion()
                con.Open()
                Dim query As String = "DELETE FROM productos WHERE id = @id"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@id", id)

                    Dim filasAfectadas As Integer = cmd.ExecuteNonQuery()
                    Return filasAfectadas > 0
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al eliminar producto: " & ex.Message)
        End Try
    End Function
End Class
