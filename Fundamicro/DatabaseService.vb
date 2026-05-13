Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text
'Imports TLogic.Models

Namespace Services
    Public Class DatabaseService
        Private ReadOnly connectionString As String

        Public Sub New(connString As String)
            Me.connectionString = connString
        End Sub

        ' Utilidad para Hash SHA256
        Public Shared Function HashPassword(password As String) As String
            Using sha256 As SHA256 = SHA256.Create()
                Dim bytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
                Dim builder As New StringBuilder()
                For Each b As Byte In bytes
                    builder.Append(b.ToString("x2"))
                Next
                Return builder.ToString()
            End Using
        End Function

        ' Autenticación
        Public Function ValidarUsuario(username As String, password As String) As Usuario
            Dim hash As String = HashPassword(password)
            Using conn As New SqlConnection(connectionString)
                Dim cmd As New SqlCommand("SELECT UsuarioID, NombreUsuario, PasswordHash FROM Usuarios WHERE NombreUsuario = @user AND PasswordHash = @hash", conn)
                cmd.Parameters.AddWithValue("@user", username)
                cmd.Parameters.AddWithValue("@hash", hash)
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Return New Usuario With {
                            .UsuarioID = Convert.ToInt32(reader("UsuarioID")),
                            .NombreUsuario = reader("NombreUsuario").ToString(),
                            .PasswordHash = reader("PasswordHash").ToString()
                        }
                    End If
                End Using
            End Using
            Return Nothing
        End Function

        ' Clientes - Listar
        Public Function ObtenerClientes() As List(Of Cliente)
            Dim list As New List(Of Cliente)
            Using conn As New SqlConnection(connectionString)
                Dim cmd As New SqlCommand("SELECT * FROM Clientes ORDER BY Nombre, Apellido", conn)
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(New Cliente With {
                            .ClienteID = Convert.ToInt32(reader("ClienteID")),
                            .Nombre = reader("Nombre").ToString(),
                            .Apellido = reader("Apellido").ToString(),
                            .Email = reader("Email").ToString(),
                            .Telefono = reader("Telefono").ToString(),
                            .Direccion = reader("Direccion").ToString(),
                            .FechaRegistro = Convert.ToDateTime(reader("FechaRegistro"))
                        })
                    End While
                End Using
            End Using
            Return list
        End Function

        ' Clientes - Obtener por ID
        Public Function ObtenerCliente(id As Integer) As Cliente
            Using conn As New SqlConnection(connectionString)
                Dim cmd As New SqlCommand("SELECT * FROM Clientes WHERE ClienteID = @id", conn)
                cmd.Parameters.AddWithValue("@id", id)
                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Return New Cliente With {
                            .ClienteID = Convert.ToInt32(reader("ClienteID")),
                            .Nombre = reader("Nombre").ToString(),
                            .Apellido = reader("Apellido").ToString(),
                            .Email = reader("Email").ToString(),
                            .Telefono = reader("Telefono").ToString(),
                            .Direccion = reader("Direccion").ToString()
                        }
                    End If
                End Using
            End Using
            Return Nothing
        End Function

        ' Bitácora - Registrar
        Public Sub RegistrarBitacora(accion As String, clienteID As Integer?, detalles As String, usuarioID As Integer, conn As SqlConnection, transaction As SqlTransaction)
            Dim cmd As New SqlCommand("INSERT INTO Bitacora (Accion, ClienteID, Detalles, UsuarioID) VALUES (@accion, @clienteId, @detalles, @usuarioId)", conn, transaction)
            cmd.Parameters.AddWithValue("@accion", accion)
            cmd.Parameters.AddWithValue("@clienteId", If(clienteID.HasValue, CObj(clienteID.Value), DBNull.Value))
            cmd.Parameters.AddWithValue("@detalles", If(String.IsNullOrEmpty(detalles), DBNull.Value, detalles))
            cmd.Parameters.AddWithValue("@usuarioId", usuarioID)
            cmd.ExecuteNonQuery()
        End Sub

        ' Clientes - Agregar
        Public Sub AgregarCliente(cliente As Cliente, usuarioID As Integer)
            Using conn As New SqlConnection(connectionString)
                conn.Open()
                Using transaction = conn.BeginTransaction()
                    Try
                        Dim cmd As New SqlCommand("INSERT INTO Clientes (Nombre, Apellido, Email, Telefono, Direccion) OUTPUT INSERTED.ClienteID VALUES (@nombre, @apellido, @email, @telefono, @direccion)", conn, transaction)
                        cmd.Parameters.AddWithValue("@nombre", cliente.Nombre)
                        cmd.Parameters.AddWithValue("@apellido", cliente.Apellido)
                        cmd.Parameters.AddWithValue("@email", cliente.Email)
                        cmd.Parameters.AddWithValue("@telefono", cliente.Telefono)
                        cmd.Parameters.AddWithValue("@direccion", cliente.Direccion)

                        Dim nuevoClienteID As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                        RegistrarBitacora("Agregar", nuevoClienteID, $"Cliente {cliente.Nombre} agregado.", usuarioID, conn, transaction)

                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Sub

        ' Clientes - Editar
        Public Sub EditarCliente(cliente As Cliente, usuarioID As Integer)
            Using conn As New SqlConnection(connectionString)
                conn.Open()
                Using transaction = conn.BeginTransaction()
                    Try
                        Dim cmd As New SqlCommand("UPDATE Clientes SET Nombre=@nombre, Apellido=@apellido, Email=@email, Telefono=@telefono, Direccion=@direccion WHERE ClienteID = @id", conn, transaction)
                        cmd.Parameters.AddWithValue("@nombre", cliente.Nombre)
                        cmd.Parameters.AddWithValue("@apellido", cliente.Apellido)
                        cmd.Parameters.AddWithValue("@email", cliente.Email)
                        cmd.Parameters.AddWithValue("@telefono", cliente.Telefono)
                        cmd.Parameters.AddWithValue("@direccion", cliente.Direccion)
                        cmd.Parameters.AddWithValue("@id", cliente.ClienteID)

                        cmd.ExecuteNonQuery()
                        RegistrarBitacora("Editar", cliente.ClienteID, $"Cliente {cliente.Nombre} actualizado.", usuarioID, conn, transaction)

                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Sub

        ' Clientes - Eliminar
        Public Sub EliminarCliente(clienteID As Integer, usuarioID As Integer)
            Using conn As New SqlConnection(connectionString)
                conn.Open()
                Using transaction = conn.BeginTransaction()
                    Try
                        Dim cmd As New SqlCommand("DELETE FROM Clientes WHERE ClienteID = @id", conn, transaction)
                        cmd.Parameters.AddWithValue("@id", clienteID)

                        cmd.ExecuteNonQuery()
                        RegistrarBitacora("Eliminar", clienteID, $"Cliente con ID {clienteID} fue eliminado.", usuarioID, conn, transaction)

                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Sub
    End Class
End Namespace
