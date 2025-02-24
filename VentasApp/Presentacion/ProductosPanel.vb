Public Class ProductosPanel

    Private txtNewNombre As TextBox
    Private txtNewPrecio As TextBox
    Private txtNewCategoria As TextBox
    Private txtBuscarNombre As TextBox
    Private txtBuscarPrecio As TextBox
    Private txtBuscarCategoria As TextBox

    Private dgvProductos As DataGridView

    Public Function CrearPanel() As Panel
        Dim panel As New Panel With {.Dock = DockStyle.Fill}

        ' DataGridView para listar productos
        dgvProductos = New DataGridView With {
            .Top = 40,
            .Left = 10,
            .Height = 200,
            .Width = 500
        }
        AddHandler dgvProductos.CellClick, AddressOf SeleccionarProducto

        ' Controles para CRUD
        txtNewNombre = New TextBox With {.Top = 320, .Left = 10, .Width = 200}
        txtNewPrecio = New TextBox With {.Top = 320, .Left = 220, .Width = 100}
        txtNewCategoria = New TextBox With {.Top = 320, .Left = 330, .Width = 200}

        txtBuscarNombre = New TextBox With {.Top = 10, .Left = 10, .Width = 150}
        txtBuscarPrecio = New TextBox With {.Top = 10, .Left = 170, .Width = 150}
        txtBuscarCategoria = New TextBox With {.Top = 10, .Left = 330, .Width = 150}


        Dim btnAgregar As New Button With {.Text = "Agregar", .Top = 360, .Left = 10, .Width = 100}
        AddHandler btnAgregar.Click, AddressOf AgregarProducto

        Dim btnEditar As New Button With {.Text = "Editar", .Top = 360, .Left = 120, .Width = 100}
        AddHandler btnEditar.Click, AddressOf EditarProducto

        Dim btnEliminar As New Button With {.Text = "Eliminar", .Top = 360, .Left = 230, .Width = 100}
        AddHandler btnEliminar.Click, AddressOf EliminarProducto

        Dim btnBuscar As New Button With {.Text = "Buscar", .Top = 10, .Left = 490, .Width = 100}
        AddHandler btnBuscar.Click, AddressOf BuscarProductos

        ' Agregar controles al panel
        panel.Controls.Add(txtNewNombre)
        panel.Controls.Add(txtNewPrecio)
        panel.Controls.Add(txtNewCategoria)
        panel.Controls.Add(btnAgregar)
        panel.Controls.Add(btnEditar)
        panel.Controls.Add(btnEliminar)
        panel.Controls.Add(txtBuscarNombre)
        panel.Controls.Add(txtBuscarPrecio)
        panel.Controls.Add(txtBuscarCategoria)
        panel.Controls.Add(btnBuscar)
        panel.Controls.Add(dgvProductos)

        ObtenerTodosProductos()

        Return panel
    End Function

    ' Agregar un producto
    Private Sub AgregarProducto(sender As Object, e As EventArgs)
        Try
            ' Validar que el campo Precio no esté vacío y sea numérico
            If String.IsNullOrWhiteSpace(txtNewPrecio.Text) OrElse Not IsNumeric(txtNewPrecio.Text) Then
                MessageBox.Show("Ingrese un precio válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Convertir el texto a Decimal de forma segura
            Dim precio As Decimal = Convert.ToDecimal(txtNewPrecio.Text, Globalization.CultureInfo.InvariantCulture)

            ' Crear el producto
            Dim nuevoProducto As New Producto(0, txtNewNombre.Text, precio, txtNewCategoria.Text)

            If ProductoDAL.AgregarProducto(nuevoProducto) Then
                ' Limpiar TextBox
                txtNewNombre.Clear()
                txtNewPrecio.Clear()
                txtNewCategoria.Clear()
                MessageBox.Show("Producto agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ObtenerTodosProductos()
            Else
                MessageBox.Show("No se pudo agregar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Editar un producto
    Private Sub EditarProducto(sender As Object, e As EventArgs)
        Try
            ' Verificar si hay un producto seleccionado
            If txtNewNombre.Tag Is Nothing Then
                MessageBox.Show("Seleccione un producto para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Obtener datos
            Dim idProducto As Integer = Convert.ToInt32(txtNewNombre.Tag)
            Dim nombre As String = txtNewNombre.Text
            Dim precio As Decimal = Convert.ToDecimal(txtNewPrecio.Text)
            Dim categoria As String = txtNewCategoria.Text

            ' Crear objeto producto
            Dim productoEditado As New Producto(idProducto, nombre, precio, categoria)

            ' Actualizar en la base de datos
            ProductoDAL.ActualizarProducto(productoEditado)

            ' Actualizar DataGridView
            ObtenerTodosProductos()

            ' Limpiar TextBox y Tag
            txtNewNombre.Clear()
            txtNewPrecio.Clear()
            txtNewCategoria.Clear()
            txtNewNombre.Tag = Nothing

            MessageBox.Show("Producto editado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Eliminar un producto
    Private Sub Eliminarproducto(sender As Object, e As EventArgs)
        Try
            ' Verificar si hay un producto seleccionado
            If txtNewNombre.Tag Is Nothing Then
                MessageBox.Show("Seleccione un producto para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Confirmar eliminación
            Dim resultado As DialogResult = MessageBox.Show("¿Está seguro de eliminar este producto?", "Confirmar eliminación",
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If resultado = DialogResult.No Then Return

            ' Obtener ID del producto
            Dim idproducto As Integer = Convert.ToInt32(txtNewNombre.Tag)

            ' Eliminar en la base de datos
            ProductoDAL.EliminarProducto(idproducto)

            ' Actualizar DataGridView
            ObtenerTodosProductos()

            ' Limpiar TextBox y Tag
            txtNewNombre.Clear()
            txtNewPrecio.Clear()
            txtNewCategoria.Clear()
            txtNewNombre.Tag = Nothing

            MessageBox.Show("producto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Obtener todos los productos
    Private Sub ObtenerTodosProductos()
        Try
            Dim producto As List(Of Producto) = ProductoDAL.ObtenerProductos()
            dgvProductos.DataSource = producto
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Evento para seleccionar un producto del DataGridView y cargar los datos en los TextBox
    Private Sub SeleccionarProducto(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            ' Obtener fila seleccionada
            Dim fila As DataGridViewRow = dgvProductos.Rows(e.RowIndex)

            ' Cargar los datos en los TextBox
            txtNewNombre.Text = fila.Cells("Nombre").Value.ToString()
            txtNewPrecio.Text = fila.Cells("Precio").Value.ToString()
            txtNewCategoria.Text = fila.Cells("Categoria").Value.ToString()

            ' Guardar el ID del producto en una variable oculta
            txtNewNombre.Tag = fila.Cells("ID").Value
        End If
    End Sub

    ' Función para buscar productos con filtros combinados (precio máximo)
    Private Sub BuscarProductos(sender As Object, e As EventArgs)
        Try
            ' Obtener los valores de los textboxes
            Dim nombreFiltro As String = txtBuscarNombre.Text.Trim().ToLower()
            Dim categoriaFiltro As String = txtBuscarCategoria.Text.Trim().ToLower()
            Dim precioMaxStr As String = txtBuscarPrecio.Text.Trim()

            ' Intentar convertir el precio máximo a decimal
            Dim precioMax As Decimal
            Dim tienePrecioMax As Boolean = Decimal.TryParse(precioMaxStr, precioMax)

            ' Obtener la lista original de productos
            Dim listaProductos As List(Of Producto) = ProductoDAL.ObtenerProductos()

            ' Filtrar la lista según los valores ingresados
            Dim listaFiltrada As List(Of Producto) = listaProductos.Where(Function(p)
                                                                              Dim coincideNombre As Boolean = String.IsNullOrEmpty(nombreFiltro) OrElse p.Nombre.ToLower().Contains(nombreFiltro)
                                                                              Dim coincideCategoria As Boolean = String.IsNullOrEmpty(categoriaFiltro) OrElse p.Categoria.ToLower().Contains(categoriaFiltro)
                                                                              Dim coincidePrecio As Boolean = Not tienePrecioMax OrElse p.Precio <= precioMax
                                                                              Return coincideNombre AndAlso coincideCategoria AndAlso coincidePrecio
                                                                          End Function).ToList()

            ' Asignar la lista filtrada al DataGridView
            dgvProductos.DataSource = listaFiltrada
        Catch ex As Exception
            MessageBox.Show("Error al filtrar productos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class