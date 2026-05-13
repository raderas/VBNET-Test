<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="Fundamicro.Login" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>Login - Fundamicro</title>
    <style>
        body { font-family: Arial, sans-serif; background-color: #f4f7f6; display: flex; justify-content: center; align-items: center; height: 100vh; margin: 0; }
        .login-container { background: #fff; padding: 30px; border-radius: 8px; box-shadow: 0 4px 8px rgba(0,0,0,0.1); width: 300px; text-align: center; }
        .login-container h2 { margin-top: 0; color: #333; }
        .form-group { margin-bottom: 15px; text-align: left; }
        .form-group label { display: block; margin-bottom: 5px; color: #666; }
        .form-group input { width: 100%; padding: 8px; border: 1px solid #ccc; border-radius: 4px; box-sizing: border-box; }
        .btn-login { width: 100%; padding: 10px; background-color: #0056b3; border: none; color: white; border-radius: 4px; cursor: pointer; font-size: 16px; }
        .btn-login:hover { background-color: #004494; }
        .error-message { color: red; margin-bottom: 15px; font-size: 14px; }
    </style>
</head>
<body>
    <form id="form2" runat="server">
        <div class="login-container">
            <h2>Iniciar Sesión</h2>
            <asp:Label ID="lblError" runat="server" CssClass="error-message" Visible="false"></asp:Label>
            
            <div class="form-group">
                <label for="txtUsername">Usuario</label>
                <asp:TextBox ID="txtUsername" runat="server" required="required"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="txtPassword">Contraseña</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" required="required"></asp:TextBox>
            </div>
            
            <asp:Button ID="btnLogin" runat="server" Text="Entrar" CssClass="btn-login" OnClick="btnLogin_Click" />
        </div>
    </form>
</body>
</html>

