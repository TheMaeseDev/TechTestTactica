Public Class Venta
    Public Property Id As Integer
    Public Property IdCliente As Integer
    Public Property Fecha As DateTime
    Public Property Total As Decimal

    Public Sub New()
    End Sub

    Public Sub New(id As Integer, idCliente As Integer, fecha As DateTime, total As Decimal)
        Me.Id = id
        Me.IdCliente = idCliente
        Me.Fecha = fecha
        Me.Total = total
    End Sub
End Class
