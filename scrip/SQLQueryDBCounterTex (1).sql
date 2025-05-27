-- Crear la base de datos
CREATE DATABASE CounterTexDatabase;
GO

USE CounterTexDatabase;
GO

-- Tabla: Usuarios
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Documento NVARCHAR(20) NOT NULL UNIQUE,
    Correo NVARCHAR(100) UNIQUE,
    Contraseña NVARCHAR(255),
    Rol NVARCHAR(20),
    Edad INT,
    Telefono NVARCHAR(20),
    OperacionId INT NULL -- se agrega para referencia después
);
GO

-- Tabla: Prendas
CREATE TABLE Prendas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Genero NVARCHAR(20),
    Color NVARCHAR(50)
);
GO

-- Tabla: Operaciones
CREATE TABLE Operaciones (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    ValorUnitario DECIMAL(10,2) CONSTRAINT DF_ValorUnitario DEFAULT 0.00
);
GO

-- Foreign Key: Usuarios → Operaciones
ALTER TABLE Usuarios
ADD CONSTRAINT FK_Usuario_Operacion FOREIGN KEY (OperacionId) REFERENCES Operaciones(Id);
GO

-- Tabla: Produccion
CREATE TABLE Produccion (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE,
    TotalValor DECIMAL(10,2),
    UsuarioId INT NOT NULL,
    PrendaId INT NOT NULL,
    CONSTRAINT FK_Produccion_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id),
    CONSTRAINT FK_Produccion_Prenda FOREIGN KEY (PrendaId) REFERENCES Prendas(Id)
);
GO

-- Tabla: ProduccionDetalle
CREATE TABLE ProduccionDetalle (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Cantidad INT NOT NULL,
    ProduccionId INT NOT NULL,
    OperacionId INT NOT NULL,
    ValorTotal DECIMAL(10, 2) NULL,
    FOREIGN KEY (ProduccionId) REFERENCES Produccion(Id),
    FOREIGN KEY (OperacionId) REFERENCES Operaciones(Id)
);
GO

-- Tabla: Horarios
CREATE TABLE Horarios
(
    HorarioId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  -- Clave primaria autoincremental
    EmpleadoId INT NOT NULL,                           -- FK hacia Usuarios.Id
    Tipo NVARCHAR(50) NOT NULL,
    Hora TIME NOT NULL,
    Fecha DATE NOT NULL,
    Observaciones NVARCHAR(MAX) NULL,

    CONSTRAINT FK_Horarios_Usuarios FOREIGN KEY (EmpleadoId) REFERENCES Usuarios(Id)
);
GO

-- Tabla: Metas
CREATE TABLE Metas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE,
    MetaCorte INT,
    ProduccionReal INT,
    UsuarioId INT FOREIGN KEY REFERENCES Usuarios(Id),
    FechaHora DATETIME,
    Mensaje NVARCHAR(500),
    RemitenteId INT,
    DestinatarioId INT,
    CONSTRAINT FK_Meta_Remitente FOREIGN KEY (RemitenteId) REFERENCES Usuarios(Id),
    CONSTRAINT FK_Meta_Destinatario FOREIGN KEY (DestinatarioId) REFERENCES Usuarios(Id)
);
GO

-- Tabla: MensajesChat
CREATE TABLE MensajesChat (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FechaHora DATETIME,
    Mensaje NVARCHAR(MAX),
    RemitenteId INT NOT NULL,
    DestinatarioId INT NOT NULL,
    CONSTRAINT FK_Remitente FOREIGN KEY (RemitenteId) REFERENCES Usuarios(Id),
    CONSTRAINT FK_Destinatario FOREIGN KEY (DestinatarioId) REFERENCES Usuarios(Id)
);
GO

-- Tabla: Contacto
CREATE TABLE Contacto (
    Id INT PRIMARY KEY IDENTITY(1,1),
    NombreCompleto NVARCHAR(100),
    Telefono NVARCHAR(20),
    Correo NVARCHAR(100),
    Observacion NVARCHAR(MAX)
);
GO

-- ===========================
-- DATOS DE PRUEBA (INSERTs)
-- ===========================

-- Usuarios
INSERT INTO Usuarios (Nombre, Documento, Correo, Contraseña, Rol, Edad, Telefono, OperacionId)
VALUES 
('Juan Pérez', '123456789', 'juan.perez@example.com', 'hashed_password_1', 'Empleado', 30, '3001234567', NULL),
('María Gómez', '987654321', 'maria.gomez@example.com', 'hashed_password_2', 'Administrador', 40, '3007654321', NULL),
('Carlos López', '456789123', 'carlos.lopez@example.com', 'hashed_password_3', 'Proveedor', 25, '3005551234', NULL);
GO

-- Prendas
INSERT INTO Prendas (Nombre, Genero, Color)
VALUES 
('Camisa Clásica', 'Unisex', 'Blanco'),
('Pantalón Slim', 'Masculino', 'Azul');
GO

-- Operaciones
INSERT INTO Operaciones (Nombre, ValorUnitario)
VALUES 
('Corte', 500.00),
('Costura', 700.00),
('Revisión', 300.00);
GO

-- Produccion
INSERT INTO Produccion (Fecha, TotalValor, UsuarioId, PrendaId)
VALUES 
('2025-04-19', 3500.00, 2, 1),
('2025-04-19', 2100.00, 2, 2),
('2025-04-18', 4200.00, 2, 1),
('2025-04-18', 2800.00, 2, 2),
('2025-12-10', 5000.00, 1, 1),
('2025-12-10', 3000.00, 1, 2),
('2025-12-09', 4000.00, 1, 1),
('2025-12-09', 3500.00, 2, 1),
('2025-09-05', 8100.00, 1, 2);
GO

-- ProduccionDetalle
INSERT INTO ProduccionDetalle (Cantidad, ProduccionId, OperacionId, ValorTotal)
VALUES 
(5, 1, 1, 2500.00),
(3, 1, 2, 2100.00),
(2, 2, 3, 600.00);
GO

-- Horarios
INSERT INTO Horarios (EmpleadoId, Tipo, Hora, Fecha, Observaciones)
VALUES
(1, 'entrada', '08:00:00', '2025-05-22', 'Llegada puntual'),
(1, 'salida', '17:00:00', '2025-05-22', 'Salida normal'),
(2, 'entrada', '09:00:00', '2025-05-22', 'Llegada tarde'),
(3, 'entrada', '08:30:00', '2025-05-22', 'Llegada puntual'),
(3, 'descanso', '12:00:00', '2025-05-22', 'Descanso almuerzo');
GO

-- Metas
INSERT INTO Metas (Fecha, MetaCorte, ProduccionReal, UsuarioId, FechaHora, Mensaje, RemitenteId, DestinatarioId)
VALUES 
('2025-04-19', 50, 45, 2, GETDATE(), 'Buena producción', 1, 2),
('2025-04-18', 60, 62, 2, GETDATE(), 'Excelente trabajo', 1, 2);
GO

-- MensajesChat
INSERT INTO MensajesChat (FechaHora, Mensaje, RemitenteId, DestinatarioId)
VALUES 
(GETDATE(), 'Hola, ¿cómo va la producción?', 1, 2),
(GETDATE(), 'Todo en orden, ya casi terminamos.', 2, 1);
GO

-- Contacto
INSERT INTO Contacto (NombreCompleto, Telefono, Correo, Observacion)
VALUES 
('Andres López', '3001234567', 'andres.lopez@correo.com', 'Posible proveedor de insumos.'),
('Maria Torres', '3017654321', 'maria.torres@correo.com', 'Contacto para nuevos diseños.');
GO