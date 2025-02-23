Public Class VentaItem
    Public Property Id As Integer
    Public Property IdVenta As Integer
    Public Property IdProducto As Integer
    Public Property PrecioUnitario As Decimal
    Public Property Cantidad As Integer
    Public Property PrecioTotal As Decimal

    Public Sub New()
    End Sub

    Public Sub New(id As Integer, idVenta As Integer, idProducto As Integer, precioUnitario As Decimal, cantidad As Integer, precioTotal As Decimal)
        Me.Id = id
        Me.IdVenta = idVenta
        Me.IdProducto = idProducto
        Me.PrecioUnitario = precioUnitario
        Me.Cantidad = cantidad
        Me.PrecioTotal = precioTotal
    End Sub
End Class
