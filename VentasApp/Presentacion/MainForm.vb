Imports System.Data.SqlClient

Public Class MainForm
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Configurar la interfaz
        Me.Text = "Gestión de Ventas"
        Me.Width = 800
        Me.Height = 600

        ' Crear TabControl
        Dim tabControl As New TabControl()
        tabControl.Dock = DockStyle.Fill

        ' Crear pestañas
        Dim tabInicio As New TabPage("Inicio")
        Dim tabClientes As New TabPage("Clientes")
        Dim tabProductos As New TabPage("Productos")
        Dim tabVentas As New TabPage("Ventas")
        Dim tabReportes As New TabPage("Reportes")

        ' Crear instancias de los paneles
        Dim inicioPanel As New InicioPanel()
        Dim clientesPanel As New ClientesPanel()
        Dim productosPanel As New ProductosPanel()
        Dim ventasPanel As New VentasPanel()
        Dim reportesPanel As New ReportesPanel()

        ' Agregar contenido a cada pestaña
        tabInicio.Controls.Add(inicioPanel.CrearPanel())
        tabClientes.Controls.Add(clientesPanel.CrearPanel())
        tabProductos.Controls.Add(productosPanel.CrearPanel())
        tabVentas.Controls.Add(ventasPanel.CrearPanel())
        tabReportes.Controls.Add(reportesPanel.CrearPanel())

        ' Agregar pestañas al TabControl
        tabControl.TabPages.Add(tabInicio)
        tabControl.TabPages.Add(tabClientes)
        tabControl.TabPages.Add(tabProductos)
        tabControl.TabPages.Add(tabVentas)
        tabControl.TabPages.Add(tabReportes)

        ' Agregar TabControl al formulario
        Me.Controls.Add(tabControl)
    End Sub

End Class