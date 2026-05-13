Imports System
Imports System.Configuration
Imports System.Web.Security
Imports System.Web.UI.WebControls
Imports Fundamicro.Services


Partial Public Class [Default]
        Inherits System.Web.UI.Page

        Private dbService As DatabaseService

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If (Not User.Identity.IsAuthenticated) OrElse Session("UsuarioID") Is Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("Login.aspx")
                Return
            End If

            Dim connString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            dbService = New DatabaseService(connString)

            If Not IsPostBack Then
                If Session("NombreUsuario") IsNot Nothing Then
                    lblUsuario.Text = Session("NombreUsuario").ToString()
                End If
                LoadClientes()
            End If
        End Sub

        Private Sub LoadClientes()
            Try
                Dim clientes = dbService.ObtenerClientes()
                gvClientes.DataSource = clientes
                gvClientes.DataBind()
            Catch ex As Exception
                lblError.Text = "Error al cargar la lista de clientes: " & ex.Message
            End Try
        End Sub

        Protected Sub gvClientes_RowCommand(sender As Object, e As GridViewCommandEventArgs)
            If e.CommandName = "Eliminar" Then
                Try
                    Dim id = Convert.ToInt32(e.CommandArgument)
                    Dim usuarioId = Convert.ToInt32(Session("UsuarioID"))

                    dbService.EliminarCliente(id, usuarioId)
                    lblMessage.Text = "Cliente eliminado exitosamente."
                    LoadClientes()
                Catch ex As Exception
                    lblError.Text = "Error al eliminar el cliente: " & ex.Message
                End Try
            End If
        End Sub

        Protected Sub btnLogout_Click(sender As Object, e As EventArgs)
            Session.Clear()
            FormsAuthentication.SignOut()
            Response.Redirect("Login.aspx")
        End Sub
    End Class
