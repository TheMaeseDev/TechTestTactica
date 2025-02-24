Public Class ClientesPanel

    Private txtNewNombre As TextBox
    Private txtNewTelefono As TextBox
    Private txtNewCorreo As TextBox
    Private txtBuscarNombre As TextBox
    Private txtBuscarTelefono As TextBox
    Private txtBuscarCorreo As TextBox

    Private dgvClientes As DataGridView

    Public Function CrearPanel() As Panel
        Dim panel As New Panel With {.Dock = DockStyle.Fill}

        ' DataGridView para listar clientes
        dgvClientes = New DataGridView With {
            .Top = 40,
            .Left = 10,
            .Height = 200,
            .Width = 500
        }
        AddHandler dgvClientes.CellClick, AddressOf SeleccionarCliente

        ' Controles para CRUD
        txtNewNombre = New TextBox With {.Top = 320, .Left = 10, .Width = 150}
        txtNewTelefono = New TextBox With {.Top = 320, .Left = 170, .Width = 150}
        txtNewCorreo = New TextBox With {.Top = 320, .Left = 330, .Width = 150}

        txtBuscarNombre = New TextBox With {.Top = 10, .Left = 10, .Width = 150}
        txtBuscarTelefono = New TextBox With {.Top = 10, .Left = 170, .Width = 150}
        txtBuscarCorreo = New TextBox With {.Top = 10, .Left = 330, .Width = 150}

        Dim btnAgregar As New Button With {.Text = "Agregar", .Top = 360, .Left = 10, .Width = 100}
        AddHandler btnAgregar.Click, AddressOf AgregarCliente

        Dim btnEditar As New Button With {.Text = "Editar", .Top = 360, .Left = 120, .Width = 100}
        AddHandler btnEditar.Click, AddressOf EditarCliente

        Dim btnEliminar As New Button With {.Text = "Eliminar", .Top = 360, .Left = 230, .Width = 100}
        AddHandler btnEliminar.Click, AddressOf EliminarCliente

        Dim btnBuscar As New Button With {.Text = "Buscar", .Top = 10, .Left = 490, .Width = 100}

        ' Agregar controles al panel

        panel.Controls.Add(txtNewNombre)
        panel.Controls.Add(txtNewTelefono)
        panel.Controls.Add(txtNewCorreo)
        panel.Controls.Add(btnAgregar)
        panel.Controls.Add(btnEditar)
        panel.Controls.Add(btnEliminar)
        panel.Controls.Add(txtBuscarNombre)
        panel.Controls.Add(txtBuscarTelefono)
        panel.Controls.Add(txtBuscarCorreo)
        panel.Controls.Add(btnBuscar)
        panel.Controls.Add(dgvClientes)

        ObtenerTodosClientes()

        Return panel
    End Function

    ' Agregar un cliente
    Private Sub AgregarCliente(sender As Object, e As EventArgs)
        Try
            Dim nuevoCliente As New Cliente(0, txtNewNombre.Text, txtNewTelefono.Text, txtNewCorreo.Text)
            If ClienteDAL.AgregarCliente(nuevoCliente) Then
                ' Limpiar TextBox
                txtNewNombre.Clear()
                txtNewTelefono.Clear()
                txtNewCorreo.Clear()
                MessageBox.Show("Cliente agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("No se pudo agregar el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ObtenerTodosClientes()
    End Sub

    ' Editar un cliente
    Private Sub EditarCliente(sender As Object, e As EventArgs)
        Try
            ' Verificar si hay un cliente seleccionado
            If txtNewNombre.Tag Is Nothing Then
                MessageBox.Show("Seleccione un cliente para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Obtener datos
            Dim idCliente As Integer = Convert.ToInt32(txtNewNombre.Tag)
            Dim nombre As String = txtNewNombre.Text
            Dim telefono As String = txtNewTelefono.Text
            Dim correo As String = txtNewCorreo.Text

            ' Crear objeto Cliente
            Dim clienteEditado As New Cliente(idCliente, nombre, telefono, correo)

            ' Actualizar en la base de datos
            ClienteDAL.ActualizarCliente(clienteEditado)

            ' Actualizar DataGridView
            ObtenerTodosClientes()

            ' Limpiar TextBox y Tag
            txtNewNombre.Clear()
            txtNewTelefono.Clear()
            txtNewCorreo.Clear()
            txtNewNombre.Tag = Nothing

            MessageBox.Show("Cliente editado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Eliminar un cliente
    Private Sub EliminarCliente(sender As Object, e As EventArgs)
        Try
            ' Verificar si hay un cliente seleccionado
            If txtNewNombre.Tag Is Nothing Then
                MessageBox.Show("Seleccione un cliente para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Confirmar eliminación
            Dim resultado As DialogResult = MessageBox.Show("¿Está seguro de eliminar este cliente?", "Confirmar eliminación",
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If resultado = DialogResult.No Then Return

            ' Obtener ID del cliente
            Dim idCliente As Integer = Convert.ToInt32(txtNewNombre.Tag)

            ' Eliminar en la base de datos
            ClienteDAL.EliminarCliente(idCliente)

            ' Actualizar DataGridView
            ObtenerTodosClientes()

            ' Limpiar TextBox y Tag
            txtNewNombre.Clear()
            txtNewTelefono.Clear()
            txtNewCorreo.Clear()
            txtNewNombre.Tag = Nothing

            MessageBox.Show("Cliente eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Obtener todos los clientes
    Private Sub ObtenerTodosClientes()
        Try
            Dim clientes As List(Of Cliente) = ClienteDAL.ObtenerClientes()
            dgvClientes.DataSource = clientes
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Evento para seleccionar un cliente del DataGridView y cargar los datos en los TextBox
    Private Sub SeleccionarCliente(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            ' Obtener fila seleccionada
            Dim fila As DataGridViewRow = dgvClientes.Rows(e.RowIndex)

            ' Cargar los datos en los TextBox
            txtNewNombre.Text = fila.Cells("Nombre").Value.ToString()
            txtNewTelefono.Text = fila.Cells("Telefono").Value.ToString()
            txtNewCorreo.Text = fila.Cells("Correo").Value.ToString()

            ' Guardar el ID del cliente en una variable oculta
            txtNewNombre.Tag = fila.Cells("ID").Value
        End If
    End Sub


End Class