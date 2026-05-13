Imports System
Imports System.Configuration
Imports Fundamicro.Services

Partial Public Class ManageCliente
        Inherits System.Web.UI.Page

        Private dbService As DatabaseService

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If (Not User.Identity.IsAuthenticated) OrElse Session("UsuarioID") Is Nothing Then
                Response.Redirect("Login.aspx")
                Return
            End If

            Dim connString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            dbService = New DatabaseService(connString)

            If Not IsPostBack Then
                If Request.QueryString("id") IsNot Nothing Then
                    Dim id = Convert.ToInt32(Request.QueryString("id"))
                    CargarCliente(id)
                End If
            End If
        End Sub

        Private Sub CargarCliente(id As Integer)
            Try
                Dim cliente = dbService.ObtenerCliente(id)
                If cliente IsNot Nothing Then
                    litTitulo.Text = "Editar Cliente"
                    hfClienteID.Value = cliente.ClienteID.ToString()
                    txtNombre.Text = cliente.Nombre
                    txtApellido.Text = cliente.Apellido
                    txtEmail.Text = cliente.Email
                    txtTelefono.Text = cliente.Telefono
                    txtDireccion.Text = cliente.Direccion
                Else
                    lblError.Text = "No se encontró el cliente."
                    lblError.Visible = True
                End If
            Catch ex As Exception
                lblError.Text = "Error al cargar el cliente: " & ex.Message
                lblError.Visible = True
            End Try
        End Sub

        Protected Sub btnSave_Click(sender As Object, e As EventArgs)
            Try
                Dim usuarioId = Convert.ToInt32(Session("UsuarioID"))

                Dim cliente As New Cliente With {
                    .Nombre = txtNombre.Text.Trim(),
                    .Apellido = txtApellido.Text.Trim(),
                    .Email = txtEmail.Text.Trim(),
                    .Telefono = txtTelefono.Text.Trim(),
                    .Direccion = txtDireccion.Text.Trim()
                }

                If Not String.IsNullOrEmpty(hfClienteID.Value) Then
                    cliente.ClienteID = Convert.ToInt32(hfClienteID.Value)
                    dbService.EditarCliente(cliente, usuarioId)
                Else
                    dbService.AgregarCliente(cliente, usuarioId)
                End If

                Response.Redirect("Default.aspx")
            Catch ex As Exception
                lblError.Text = "Error al guardar: " & ex.Message
                lblError.Visible = True
            End Try
        End Sub
    End Class
