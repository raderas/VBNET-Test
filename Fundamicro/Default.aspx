<%@ Page Language="VB" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="Fundamicro.Default" %>

    <!DOCTYPE html>
    <html lang="es">

    <head runat="server">
        <meta charset="utf-8" />
        <title>Clientes - Fundamicro</title>
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
                max-width: 900px;
                margin: auto;
                background: white;
                margin-top: 20px;
                border-radius: 8px;
                box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            }

            .btn {
                padding: 8px 12px;
                background-color: #28a745;
                color: white;
                border: none;
                border-radius: 4px;
                cursor: pointer;
                text-decoration: none;
            }

            .btn-danger {
                background-color: #dc3545;
            }

            .btn-edit {
                background-color: #ffc107;
                color: black;
            }

            .header-actions {
                display: flex;
                justify-content: space-between;
                margin-bottom: 20px;
            }

            table {
                width: 100%;
                border-collapse: collapse;
                margin-top: 20px;
            }

            table,
            th,
            td {
                border: 1px solid #ddd;
            }

            th,
            td {
                padding: 12px;
                text-align: left;
            }

            th {
                background-color: #f2f2f2;
            }
        </style>
    </head>

    <body>
        <form id="form1" runat="server">
            <div class="navbar">
                <span>Gestión de Clientes - Fundamicro</span>
                <div>
                    Bienvenido, <asp:Label ID="lblUsuario" runat="server"></asp:Label>
                    | <asp:LinkButton ID="btnLogout" runat="server" OnClick="btnLogout_Click">Cerrar Sesión
                    </asp:LinkButton>
                </div>
            </div>

            <div class="container">
                <div class="header-actions">
                    <h2>Listado de Clientes</h2>
                    <a href="ManageCliente.aspx" class="btn">Agregar Cliente</a>
                </div>

                <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>

                <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="False" DataKeyNames="ClienteID"
                    OnRowCommand="gvClientes_RowCommand" CssClass="table">
                    <Columns>
                        <asp:BoundField DataField="ClienteID" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <a href='ManageCliente.aspx?id=<%# Eval("ClienteID") %>' class="btn btn-edit">Editar</a>
                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Eliminar"
                                    CommandArgument='<%# Eval("ClienteID") %>' CssClass="btn btn-danger"
                                    OnClientClick="return confirm('¿Está seguro de eliminar este cliente?');">Eliminar
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </form>
    </body>

    </html>