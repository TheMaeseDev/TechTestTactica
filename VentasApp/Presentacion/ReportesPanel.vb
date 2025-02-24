Public Class ReportesPanel

    Public Function CrearPanel() As Panel
        Dim panel As New Panel With {.Dock = DockStyle.Fill}

        ' DataGridView para listar ventas
        Dim dgvVentas As New DataGridView With {
            .Dock = DockStyle.Top,
            .Height = 300
        }

        ' Filtros
        Dim lblFiltro As New Label With {.Text = "Filtrar por cliente:", .Top = 320, .Left = 10}
        Dim txtFiltroCliente As New TextBox With {.Top = 320, .Left = 150, .Width = 200}
        Dim btnBuscar As New Button With {.Text = "Buscar", .Top = 320, .Left = 370}

        ' Agregar controles al panel
        panel.Controls.Add(dgvVentas)
        panel.Controls.Add(lblFiltro)
        panel.Controls.Add(txtFiltroCliente)
        panel.Controls.Add(btnBuscar)

        Return panel
    End Function
End Class