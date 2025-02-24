Public Class ProductosPanel

    Private txtNombre As TextBox
    Private txtPrecio As TextBox
    Private txtCategoria As TextBox

    Public Function CrearPanel() As Panel
        Dim panel As New Panel With {.Dock = DockStyle.Fill}

        ' DataGridView para listar productos
        Dim dgvProductos As New DataGridView With {
            .Dock = DockStyle.Top,
            .Height = 300
        }

        ' Controles para CRUD
        txtNombre = New TextBox With {.Top = 320, .Left = 10, .Width = 200}
        txtPrecio = New TextBox With {.Top = 320, .Left = 220, .Width = 100}
        txtCategoria = New TextBox With {.Top = 320, .Left = 330, .Width = 200}

        Dim btnAgregar As New Button With {.Text = "Agregar", .Top = 360, .Left = 10, .Width = 100}
        AddHandler btnAgregar.Click, AddressOf AgregarProducto
        Dim btnEditar As New Button With {.Text = "Editar", .Top = 360, .Left = 120, .Width = 100}
        Dim btnEliminar As New Button With {.Text = "Eliminar", .Top = 360, .Left = 230, .Width = 100}

        ' Agregar controles al panel
        panel.Controls.Add(dgvProductos)
        panel.Controls.Add(txtNombre)
        panel.Controls.Add(txtPrecio)
        panel.Controls.Add(txtCategoria)
        panel.Controls.Add(btnAgregar)
        panel.Controls.Add(btnEditar)
        panel.Controls.Add(btnEliminar)

        Return panel
    End Function

    Private Sub AgregarProducto(sender As Object, e As EventArgs)
        Try
            Dim nuevoProducto As New Producto(0, txtNombre.Text, Convert.ToDecimal(txtPrecio.Text), txtCategoria.Text)
            If ProductoDAL.AgregarProducto(nuevoProducto) Then
                MessageBox.Show("Producto agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("No se pudo agregar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class