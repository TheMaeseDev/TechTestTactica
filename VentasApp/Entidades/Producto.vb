Public Class Producto
    Public Property Id As Integer
    Public Property Nombre As String
    Public Property Precio As Decimal
    Public Property Categoria As String

    Public Sub New()
    End Sub

    Public Sub New(id As Integer, nombre As String, precio As Decimal, categoria As String)
        Me.Id = id
        Me.Nombre = nombre
        Me.Precio = precio
        Me.Categoria = categoria
    End Sub
End Class