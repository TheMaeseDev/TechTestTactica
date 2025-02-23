Public Class Cliente
    Public Property Id As Integer
    Public Property Nombre As String
    Public Property Telefono As String
    Public Property Correo As String

    ' Constructor vacío
    Public Sub New()
    End Sub

    ' Constructor con parámetros
    Public Sub New(id As Integer, nombre As String, telefono As String, correo As String)
        Me.Id = id
        Me.Nombre = nombre
        Me.Telefono = telefono
        Me.Correo = correo
    End Sub
End Class
