CREATE DATABASE CounterTexDB;
 GO
 
 USE CounterTexDB;
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
 INSERT INTO Usuarios (Nombre, Documento, Correo, Contraseña, Rol, Edad, Telefono)
 VALUES 
 ('Laura Pérez', '12345678', 'laura.perez@example.com', '1234hashed', 'Administrador', 30, '3101234567'),
 ('Carlos Martínez', '87654321', 'carlos.martinez@example.com', 'abcdhashed', 'Operario', 25, '3129876543');
 
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
 ('2025-04-19', 2100.00, 2, 2),
 ('2025-04-18', 4200.00, 2, 1),
 ('2025-04-18', 2800.00, 2, 2),
 ('2025-12-10', 5000.00, 1, 1),
 ('2025-12-10', 3000.00, 1, 2),
 ('2025-12-09', 4000.00, 1, 1),
 ('2025-12-09', 3500.00, 2, 1),
('2025-09-05', 8100.00, 1, 2);
 
 -- ProduccionDetalle
 INSERT INTO ProduccionDetalle (Cantidad, ProduccionId, OperacionId, ValorTotal)
 VALUES 
 (5, 1, 1, 500.000),
 (3, 1, 2, 600.000),
 (2, 2, 3, 900.000);
 
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
 
 ALTER TABLE Usuarios ADD OperacionId INT;
 ALTER TABLE Usuarios
 ADD CONSTRAINT FK_Usuario_Operacion FOREIGN KEY (OperacionId) REFERENCES Operaciones(Id);
 
 
 ALTER TABLE Metas ADD FechaHora DATETIME;
 ALTER TABLE Metas ADD Mensaje NVARCHAR(500);
 ALTER TABLE Metas ADD RemitenteId INT;
 ALTER TABLE Metas ADD DestinatarioId INT;
 
 ALTER TABLE Metas ADD CONSTRAINT FK_Meta_Remitente FOREIGN KEY (RemitenteId) REFERENCES Usuarios(Id);
 ALTER TABLE Metas ADD CONSTRAINT FK_Meta_Destinatario FOREIGN KEY (DestinatarioId) REFERENCES Usuarios(Id);
 
 
 ALTER TABLE ProduccionDetalle DROP COLUMN ValorTotal;
 ALTER TABLE ProduccionDetalle ADD ValorTotal AS (Cantidad * (SELECT ValorUnitario FROM Operaciones WHERE Operaciones.Id = OperacionId)) PERSISTED;
 
 
 ALTER TABLE ProduccionDetalle DROP COLUMN ValorTotal;
 ALTER TABLE ProduccionDetalle ADD ValorTotal DECIMAL(10, 2) NULL;
 
 
 ALTER TABLE Operaciones
 ADD CONSTRAINT DF_ValorUnitario DEFAULT 0.00 FOR ValorUnitario;
 
 
 ALTER TABLE Usuarios
 ALTER COLUMN OperacionId INT NULL;
 
 SELECT * FROM Usuarios WHERE OperacionId IS NULL;
 
 SELECT * FROM Operaciones WHERE ValorUnitario IS NULL;
 
 
 SELECT COLUMN_NAME, DATA_TYPE
 FROM INFORMATION_SCHEMA.COLUMNS
 WHERE TABLE_NAME = 'Usuarios';
 
 ALTER TABLE Usuarios
 ALTER COLUMN Id INT;
 
 
 SELECT COLUMN_NAME, DATA_TYPE
 FROM INFORMATION_SCHEMA.COLUMNS
 WHERE TABLE_NAME = 'Usuarios';
 
 -- Cambiar columna OperacionId a INT (nullable)
 ALTER TABLE Usuarios
 ALTER COLUMN OperacionId INT;
 
 -- Cambiar Edad a INT (nullable)
 ALTER TABLE Usuarios
 ALTER COLUMN Edad INT;
 
 select * from Usuarios;
 
 
 UPDATE Usuarios
 SET Rol = 'Empleado'
 WHERE Correo = 'carlos.martinez@example.com';


 -- 1. Eliminar la tabla si ya existe
IF OBJECT_ID('Horarios', 'U') IS NOT NULL
    DROP TABLE Horarios;

-- 2. Crear la tabla de nuevo con solo la columna EmpleadoId
CREATE TABLE Horarios (
    EmpleadoId INT NOT NULL,
    Tipo VARCHAR(50) NOT NULL,           -- entrada, salida, descanso
    Hora TIME NOT NULL,
    Fecha DATE NOT NULL,
    Observaciones NVARCHAR(255),

    CONSTRAINT PK_Horarios PRIMARY KEY (EmpleadoId, Fecha, Tipo) -- Clave compuesta si es necesario permitir varios registros por empleado y día
);

INSERT INTO Horarios (EmpleadoId, Tipo, Hora, Fecha, Observaciones)
VALUES 
(1, 'entrada', '07:00:00', '2025-05-15', 'Llega puntual'),
(1, 'salida',  '15:00:00', '2025-05-15', 'Salida normal'),
(1, 'descanso','11:00:00', '2025-05-15', 'Pausa para almuerzo'),
(2, 'entrada', '08:00:00', '2025-05-15', 'Llegó un poco tarde'),
(2, 'salida',  '16:00:00', '2025-05-15', 'Salida después de hora');