Imports System.Data.SqlClient
Imports System.Configuration
Imports Entidades


Public Class ClienteDAL
    ' Método para obtener la conexión desde App.config
    Private Shared Function ObtenerConexion() As SqlConnection
        Dim conexion As String = ConfigurationManager.ConnectionStrings("MiConexion").ConnectionString
        Return New SqlConnection(conexion)
    End Function

    ' Método para agregar un cliente
    Public Shared Function AgregarCliente(cliente As Cliente) As Boolean
        Try
            Using con As SqlConnection = ObtenerConexion()
                con.Open()
                Dim query As String = "INSERT INTO clientes (cliente, telefono, correo) VALUES (@nombre, @telefono, @correo)"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@nombre", cliente.Nombre)
                    cmd.Parameters.AddWithValue("@telefono", cliente.Telefono)
                    cmd.Parameters.AddWithValue("@correo", cliente.Correo)

                    Dim filasAfectadas As Integer = cmd.ExecuteNonQuery()
                    Return filasAfectadas > 0
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al agregar cliente: " & ex.Message)
        End Try
    End Function

    ' Método para obtener todos los clientes
    Public Shared Function ObtenerClientes() As List(Of Cliente)
        Dim listaClientes As New List(Of Cliente)()

        Try
            Using con As SqlConnection = ObtenerConexion()
                con.Open()
                Dim query As String = "SELECT id, cliente, telefono, correo FROM clientes"
                Using cmd As New SqlCommand(query, con)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            listaClientes.Add(New Cliente(reader("id"), reader("cliente").ToString(), reader("telefono").ToString(), reader("correo").ToString()))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al obtener clientes: " & ex.Message)
        End Try

        Return listaClientes
    End Function

    ' Método para actualizar un cliente
    Public Shared Function ActualizarCliente(cliente As Cliente) As Boolean
        Try
            Using con As SqlConnection = ObtenerConexion()
                con.Open()
                Dim query As String = "UPDATE clientes SET cliente = @nombre, telefono = @telefono, correo = @correo WHERE id = @id"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@id", cliente.Id)
                    cmd.Parameters.AddWithValue("@nombre", cliente.Nombre)
                    cmd.Parameters.AddWithValue("@telefono", cliente.Telefono)
                    cmd.Parameters.AddWithValue("@correo", cliente.Correo)

                    Dim filasAfectadas As Integer = cmd.ExecuteNonQuery()
                    Return filasAfectadas > 0
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al actualizar cliente: " & ex.Message)
        End Try
    End Function

    ' Método para eliminar un cliente
    Public Shared Function EliminarCliente(id As Integer) As Boolean
        Try
            Using con As SqlConnection = ObtenerConexion()
                con.Open()
                Dim query As String = "DELETE FROM clientes WHERE id = @id"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@id", id)

                    Dim filasAfectadas As Integer = cmd.ExecuteNonQuery()
                    Return filasAfectadas > 0
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al eliminar cliente: " & ex.Message)
        End Try
    End Function

    ' Metodo para busqueda de clientes
    Public Shared Function BuscarClientes(filtro As String) As List(Of Cliente)
        Dim listaClientes As New List(Of Cliente)()

        Try
            Using con As SqlConnection = ObtenerConexion()
                con.Open()
                Dim query As String = "SELECT id, cliente, telefono, correo FROM clientes WHERE cliente LIKE @filtro OR id LIKE @filtro"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@filtro", "%" & filtro & "%")

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            listaClientes.Add(New Cliente(reader("id"), reader("cliente").ToString(), reader("telefono").ToString(), reader("correo").ToString()))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al buscar clientes: " & ex.Message)
        End Try

        Return listaClientes
    End Function
End Class
