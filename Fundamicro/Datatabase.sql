CREATE DATABASE Fundamicro;
GO

USE Fundamicro;
GO

-- 1. Tabla de Usuarios
CREATE TABLE Usuarios (
    UsuarioID INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    FechaCreacion DATETIME DEFAULT GETDATE()
);
GO

-- 2. Tabla de Clientes
CREATE TABLE Clientes (
    ClienteID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100),
    Telefono NVARCHAR(20),
    Direccion NVARCHAR(200),
    FechaRegistro DATETIME DEFAULT GETDATE()
);
GO

-- 3. Tabla de Bitácora
CREATE TABLE Bitacora (
    BitacoraID INT IDENTITY(1,1) PRIMARY KEY,
    Accion NVARCHAR(50) NOT NULL, -- e.g., 'Agregar', 'Editar', 'Eliminar'
    ClienteID INT NULL, -- Puede ser nulo si el cliente se eliminó (opcional, o guardar un string)
    Detalles NVARCHAR(MAX) NULL,
    UsuarioID INT NOT NULL,
    FechaHora DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Bitacora_Usuarios FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID)
);
GO

-- Insertar un usuario administrador por defecto
-- El PasswordHash corresponde a la contraseña 'admin123'
-- Usaremos SHA256 para el hash, así que precalculamos para 'admin123' (240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9)
INSERT INTO Usuarios (NombreUsuario, PasswordHash) 
VALUES ('admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9');
GO
