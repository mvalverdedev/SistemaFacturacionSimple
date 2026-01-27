USE master;
GO

IF NOT EXISTS (SELECT top 1 1 FROM sys.databases WHERE name = 'SistemaFacturacionDB')
BEGIN
    CREATE DATABASE SistemaFacturacionDB;
END
GO

USE SistemaFacturacionDB;
GO

-- Tabla: Usuarios (Vendedores y Administradores)
IF NOT EXISTS (SELECT top 1 1 FROM sys.tables WHERE name = 'Usuarios')
BEGIN
    CREATE TABLE Usuarios (
        Id INT PRIMARY KEY IDENTITY(1,1),
        NombreUsuario NVARCHAR(50) NOT NULL UNIQUE, -- Login
        Clave NVARCHAR(255) NOT NULL, -- Para la prueba guardaremos texto plano o hash simple
        NombreCompleto NVARCHAR(100) NOT NULL,
        Rol NVARCHAR(20) DEFAULT 'Vendedor', -- 'Admin', 'Vendedor'
        Activo BIT DEFAULT 1,
        FechaCreacion DATETIME DEFAULT GETDATE()
    );
END
GO

-- Tabla: Clientes
IF NOT EXISTS (SELECT top 1 1 FROM sys.tables WHERE name = 'Clientes')
BEGIN
    CREATE TABLE Clientes (
        Id INT PRIMARY KEY IDENTITY(1,1),
        NombreRazonSocial NVARCHAR(100) NOT NULL,
        Identificacion NVARCHAR(20) NOT NULL, -- RUC, Cedula
        Telefono NVARCHAR(20) NULL,
        Correo NVARCHAR(100) NULL,
        Direccion NVARCHAR(250) NULL,
        Activo BIT DEFAULT 1
    );
END
GO

-- Tabla: Productos
IF NOT EXISTS (SELECT top 1 1 FROM sys.tables WHERE name = 'Productos')
BEGIN
    CREATE TABLE Productos (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Codigo NVARCHAR(50) NOT NULL UNIQUE,
        Nombre NVARCHAR(100) NOT NULL,
        PrecioUnitario DECIMAL(18, 2) NOT NULL,
        Stock INT DEFAULT 0,
        Activo BIT DEFAULT 1
    );
END
GO

-- Tabla: MetodosPago (Catalogo de formas de pago)
IF NOT EXISTS (SELECT top 1 1 FROM sys.tables WHERE name = 'MetodosPago')
BEGIN
    CREATE TABLE MetodosPago (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Nombre NVARCHAR(50) NOT NULL, -- Efectivo, Tarjeta, etc
        Activo BIT DEFAULT 1
    );
END
GO



-- Tabla: Facturas (Cabecera)
IF NOT EXISTS (SELECT top 1 1 FROM sys.tables WHERE name = 'Facturas')
BEGIN
    CREATE TABLE Facturas (
        Id INT PRIMARY KEY IDENTITY(1,1),
        NumeroFactura NVARCHAR(50) NOT NULL,
        IdCliente INT NOT NULL,
        IdUsuario INT NOT NULL, -- Vendedor
        Total DECIMAL(18, 2) NOT NULL,
        FechaCreacion DATETIME DEFAULT GETDATE(),

        CONSTRAINT FK_Facturas_Clientes FOREIGN KEY (IdCliente) REFERENCES Clientes(Id),
        CONSTRAINT FK_Facturas_Usuarios FOREIGN KEY (IdUsuario) REFERENCES Usuarios(Id)
    );
END
GO

-- Tabla: DetallesFactura (Detalle)
IF NOT EXISTS (SELECT top 1 1 FROM sys.tables WHERE name = 'DetallesFactura')
BEGIN
    CREATE TABLE DetallesFactura (
        Id INT PRIMARY KEY IDENTITY(1,1),
        IdFactura INT NOT NULL,
        IdProducto INT NOT NULL,
        Cantidad INT NOT NULL,
        PrecioUnitario DECIMAL(18, 2) NOT NULL,
        SubTotal DECIMAL(18, 2) NOT NULL, -- (Cantidad * PrecioUnitario)

        CONSTRAINT FK_Detalles_Facturas FOREIGN KEY (IdFactura) REFERENCES Facturas(Id),
        CONSTRAINT FK_Detalles_Productos FOREIGN KEY (IdProducto) REFERENCES Productos(Id)
    );
END
GO

-- Tabla: PagosFactura 
IF NOT EXISTS (SELECT top 1 1 FROM sys.tables WHERE name = 'PagosFactura')
BEGIN
    CREATE TABLE PagosFactura (
        Id INT PRIMARY KEY IDENTITY(1,1),
        IdFactura INT NOT NULL,
        IdMetodoPago INT NOT NULL,
        Monto DECIMAL(18, 2) NOT NULL,
        FechaPago DATETIME DEFAULT GETDATE(),

        CONSTRAINT FK_Pagos_Facturas FOREIGN KEY (IdFactura) REFERENCES Facturas(Id),
        CONSTRAINT FK_Pagos_Metodos FOREIGN KEY (IdMetodoPago) REFERENCES MetodosPago(Id)
    );
END
GO



-- =============================================
-- Datos de Prueba
-- =============================================

-- Usuario Admin (El que pide el documento: admin / admin)
IF NOT EXISTS (SELECT top 1 1 FROM Usuarios with(nolock) WHERE NombreUsuario = 'admin')
BEGIN
    INSERT INTO Usuarios (NombreUsuario, Clave, NombreCompleto, Rol)
    VALUES ('admin', 'admin', 'Administrador Principal', 'Admin');
END

-- Usuario Vendedor de prueba
IF NOT EXISTS (SELECT top 1 1 FROM Usuarios with(nolock) WHERE NombreUsuario = 'vendedor1')
BEGIN
    INSERT INTO Usuarios (NombreUsuario, Clave, NombreCompleto, Rol)
    VALUES ('vendedor1', '123456', 'Miguel Valverde', 'Vendedor');
END

-- Formas de Pago
IF NOT EXISTS (SELECT top 1 1 FROM MetodosPago with(nolock))
BEGIN
    INSERT INTO MetodosPago (Nombre) VALUES ('Efectivo');
    INSERT INTO MetodosPago (Nombre) VALUES ('Tarjeta de Crédito');
    INSERT INTO MetodosPago (Nombre) VALUES ('Transferencia');
END

-- Cliente Generico
IF NOT EXISTS (SELECT top 1 1 FROM Clientes with(nolock))
BEGIN
    INSERT INTO Clientes (NombreRazonSocial, Identificacion, Telefono, Correo, Direccion)
    VALUES ('Consumidor Final', '9999999999', '0991234567', 'cliente@prueba.com', 'Guayaquil, Ecuador');
END

-- Productos de Ejemplo
IF NOT EXISTS (SELECT top 1 1 FROM Productos with(nolock))
BEGIN
    INSERT INTO Productos (Codigo, Nombre, PrecioUnitario, Stock) VALUES ('P001', 'Laptop HP Developer', 1200.00, 5);
    INSERT INTO Productos (Codigo, Nombre, PrecioUnitario, Stock) VALUES ('P002', 'Mouse Logitech', 25.00, 20);
    INSERT INTO Productos (Codigo, Nombre, PrecioUnitario, Stock) VALUES ('P003', 'Monitor Dell 24"', 180.00, 10);
    INSERT INTO Productos (Codigo, Nombre, PrecioUnitario, Stock) VALUES ('P004', 'Teclado Mecánico', 75.50, 15);
END
GO