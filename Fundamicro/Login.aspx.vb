Imports System
Imports System.Configuration
Imports System.Web.Security
Imports Fundamicro.Services

'Imports TLogic.Models
'Imports TLogic.Services

Partial Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If User.Identity.IsAuthenticated Then
                Response.Redirect("Default.aspx")
            End If
        End If
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Try
            Dim connString = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            Dim service As New DatabaseService(connString)

            Dim username = txtUsername.Text.Trim()
            Dim password = txtPassword.Text

            Dim usuario As Usuario = service.ValidarUsuario(username, password)

            If usuario IsNot Nothing Then
                Session("UsuarioID") = usuario.UsuarioID
                Session("NombreUsuario") = usuario.NombreUsuario

                FormsAuthentication.RedirectFromLoginPage(username, False)
            Else
                lblError.Text = "Usuario o contraseña incorrectos."
                lblError.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = "Error al intentar iniciar sesión: " & ex.Message
            lblError.Visible = True
        End Try
    End Sub
End Class
