Public Class VentasPanel
    Public Function CrearPanel() As Panel
        Dim panel As New Panel With {.Dock = DockStyle.Fill}

        ' Cliente y Fecha
        Dim lblCliente As New Label With {.Text = "Cliente:", .Top = 10, .Left = 10}
        Dim cmbCliente As New ComboBox With {.Top = 10, .Left = 70, .Width = 200}
        Dim lblFecha As New Label With {.Text = "Fecha:", .Top = 10, .Left = 300}
        Dim dtpFecha As New DateTimePicker With {.Top = 10, .Left = 350}

        ' DataGridView para productos agregados a la venta
        Dim dgvItems As New DataGridView With {
            .Top = 50,
            .Left = 10,
            .Width = 750,
            .Height = 250
        }

        ' Botón para agregar producto
        Dim btnAgregarProducto As New Button With {.Text = "Agregar Producto", .Top = 320, .Left = 10}
        Dim lblTotal As New Label With {.Text = "Total: $0.00", .Top = 320, .Left = 150}

        ' Botones Guardar y Cancelar
        Dim btnGuardar As New Button With {.Text = "Guardar Venta", .Top = 360, .Left = 10}
        Dim btnCancelar As New Button With {.Text = "Cancelar", .Top = 360, .Left = 150}

        ' Agregar controles al panel
        panel.Controls.Add(lblCliente)
        panel.Controls.Add(cmbCliente)
        panel.Controls.Add(lblFecha)
        panel.Controls.Add(dtpFecha)
        panel.Controls.Add(dgvItems)
        panel.Controls.Add(btnAgregarProducto)
        panel.Controls.Add(lblTotal)
        panel.Controls.Add(btnGuardar)
        panel.Controls.Add(btnCancelar)

        Return panel
    End Function
End Class