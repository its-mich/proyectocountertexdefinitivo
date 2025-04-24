-- Crear base de datos
CREATE DATABASE CounterTexDB;
GO

USE CounterTexDB;
GO

-- Tabla: Usuarios
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombres NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(100),
    Documento NVARCHAR(20) NOT NULL UNIQUE,
    Correo NVARCHAR(100) UNIQUE,
    Contraseña NVARCHAR(255),
    Rol NVARCHAR(20),
    Edad INT,
    Telefono NVARCHAR(20)
);

-- Tabla: Prendas
CREATE TABLE Prendas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Genero NVARCHAR(20),
    Color NVARCHAR(50)
);

-- Tabla: Operaciones
CREATE TABLE Operaciones (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    ValorUnitario DECIMAL(10,2)
);

CREATE TABLE Produccion (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE,
    TotalValor DECIMAL(10,2),
    UsuarioId INT NOT NULL,
    PrendaId INT NOT NULL,
    CONSTRAINT FK_Produccion_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id),
    CONSTRAINT FK_Produccion_Prenda FOREIGN KEY (PrendaId) REFERENCES Prendas(Id)
);


-- Tabla: ProduccionDetalle
CREATE TABLE ProduccionDetalle (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Cantidad INT NOT NULL,
    ProduccionId INT NOT NULL,
    OperacionId INT NOT NULL,
    ValorTotal DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (ProduccionId) REFERENCES Produccion(Id),
    FOREIGN KEY (OperacionId) REFERENCES Operaciones(Id)
);


-- Tabla: Horarios
CREATE TABLE Horarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE,
    HoraEntrada TIME,
    HoraSalida TIME,
    UsuarioId INT FOREIGN KEY REFERENCES Usuarios(Id)
);

-- Tabla: Metas
CREATE TABLE Metas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE,
    MetaCorte INT,
    ProduccionReal INT,
    UsuarioId INT FOREIGN KEY REFERENCES Usuarios(Id)
);

-- Tabla: MensajesChat
CREATE TABLE MensajesChat (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FechaHora DATETIME,
    Mensaje NVARCHAR(MAX),
    RemitenteId INT NOT NULL,
    DestinatarioId INT NOT NULL,
    CONSTRAINT FK_Remitente FOREIGN KEY (RemitenteId) REFERENCES Usuarios(Id) ON DELETE NO ACTION,
    CONSTRAINT FK_Destinatario FOREIGN KEY (DestinatarioId) REFERENCES Usuarios(Id) ON DELETE NO ACTION
);

-- Tabla: Contacto
CREATE TABLE Contacto (
    Id INT PRIMARY KEY IDENTITY(1,1),
    NombreCompleto NVARCHAR(100),
    Telefono NVARCHAR(20),
    Correo NVARCHAR(100),
    Observacion NVARCHAR(MAX)
);

-- INSERTS ---------------------------

-- Usuarios
INSERT INTO Usuarios (Nombres, Apellidos, Documento, Correo, Contraseña, Rol, Edad, Telefono)
VALUES 
('Laura', 'Pérez', '12345678', 'laura.perez@example.com', '1234hashed', 'Administrador', 30, '3101234567'),
('Carlos', 'Martínez', '87654321', 'carlos.martinez@example.com', 'abcdhashed', 'Operario', 25, '3129876543');

-- Prendas
INSERT INTO Prendas (Nombre, Genero, Color)
VALUES 
('Camisa Clásica', 'Unisex', 'Blanco'),
('Pantalón Slim', 'Masculino', 'Azul');

-- Operaciones
INSERT INTO Operaciones (Nombre, ValorUnitario)
VALUES 
('Corte', 500.00),
('Costura', 700.00),
('Revisión', 300.00);

-- Produccion
INSERT INTO Produccion (Fecha, TotalValor, UsuarioId, PrendaId)
VALUES 
('2025-04-19', 3500.00, 2, 1),
('2025-04-19', 2100.00, 2, 2);

-- ProduccionDetalle
INSERT INTO ProduccionDetalle (Cantidad, ProduccionId, OperacionId)
VALUES 
(5, 1, 1),
(3, 1, 2),
(2, 2, 3);

-- Horarios
INSERT INTO Horarios (Fecha, HoraEntrada, HoraSalida, UsuarioId)
VALUES 
('2025-04-19', '07:00', '15:00', 2),
('2025-04-18', '08:00', '16:00', 2);

-- Metas
INSERT INTO Metas (Fecha, MetaCorte, ProduccionReal, UsuarioId)
VALUES 
('2025-04-19', 50, 45, 2),
('2025-04-18', 60, 62, 2);

-- MensajesChat
INSERT INTO MensajesChat (FechaHora, Mensaje, RemitenteId, DestinatarioId)
VALUES 
(GETDATE(), 'Hola, ¿cómo va la producción?', 1, 2),
(GETDATE(), 'Todo en orden, ya casi terminamos.', 2, 1);

-- Contacto
INSERT INTO Contacto (NombreCompleto, Telefono, Correo, Observacion)
VALUES 
('Andrés López', '3001234567', 'andres.lopez@correo.com', 'Posible proveedor de insumos.'),
('María Torres', '3017654321', 'maria.torres@correo.com', 'Contacto para nuevos diseños.');