<%@ Page Language="VB" AutoEventWireup="true" CodeBehind="ManageCliente.aspx.vb" Inherits="Fundamicro.ManageCliente"
    %>

    <!DOCTYPE html>
    <html lang="es">

    <head runat="server">
        <meta charset="utf-8" />
        <title>Gestionar Cliente - Fundamicro</title>
        <style>
            body {
                font-family: Arial, sans-serif;
                background-color: #f4f7f6;
                margin: 0;
                padding: 0;
            }

            .navbar {
                background-color: #0056b3;
                color: white;
                padding: 15px 20px;
                display: flex;
                justify-content: space-between;
                align-items: center;
            }

            .navbar a {
                color: white;
                text-decoration: none;
                font-weight: bold;
            }

            .container {
                padding: 30px;
                max-width: 600px;
                margin: auto;
                background: white;
                margin-top: 20px;
                border-radius: 8px;
                box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            }

            .form-group {
                margin-bottom: 15px;
            }

            .form-group label {
                display: block;
                margin-bottom: 5px;
                font-weight: bold;
            }

            .form-group input {
                width: 100%;
                padding: 8px;
                border: 1px solid #ccc;
                border-radius: 4px;
                box-sizing: border-box;
            }

            .btn {
                padding: 10px 15px;
                background-color: #0056b3;
                color: white;
                border: none;
                border-radius: 4px;
                cursor: pointer;
                text-decoration: none;
            }

            .btn-cancel {
                background-color: #6c757d;
            }

            .btn:hover {
                opacity: 0.9;
            }

            .error {
                color: red;
                margin-bottom: 15px;
            }
        </style>
    </head>

    <body>
        <form id="form1" runat="server">
            <div class="navbar">
                <span>Gestión de Clientes - Fundamicro</span>
                <a href="Default.aspx">Volver al Inicio</a>
            </div>

            <div class="container">
                <h2>
                    <asp:Literal ID="litTitulo" runat="server" Text="Agregar Cliente"></asp:Literal>
                </h2>
                <asp:Label ID="lblError" runat="server" CssClass="error" Visible="false"></asp:Label>

                <asp:HiddenField ID="hfClienteID" runat="server" />

                <div class="form-group">
                    <label>Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" required="required"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Apellido</label>
                    <asp:TextBox ID="txtApellido" runat="server" required="required"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Teléfono</label>
                    <asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Dirección</label>
                    <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
                </div>

                <div style="margin-top: 20px;">
                    <asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="btn" OnClick="btnSave_Click" />
                    <a href="Default.aspx" class="btn btn-cancel">Cancelar</a>
                </div>
            </div>
        </form>
    </body>

    </html>