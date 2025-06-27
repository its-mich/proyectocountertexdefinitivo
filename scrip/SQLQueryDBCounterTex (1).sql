-- Crear la base de datos
CREATE DATABASE CounterTex;
GO

USE CounterTex;
GO

-- Tabla: Rol
CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL UNIQUE
);
GO

-- Tabla: Usuarios
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Documento NVARCHAR(20) UNIQUE,
    Correo NVARCHAR(100) UNIQUE,
    Contraseña NVARCHAR(255),
    RolId INT NOT NULL,
    Edad INT,
    Telefono NVARCHAR(20),
	TokenRecuperacion VARCHAR(100) NULL,
	TokenExpiracion DATETIME NULL,
    CONSTRAINT FK_Usuario_Rol FOREIGN KEY (RolId) REFERENCES Roles(Id)
);
GO


-- Tabla: Prendas
CREATE TABLE Prendas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Genero NVARCHAR(20),
    Color NVARCHAR(50),
    CantidadPrendas INT
);
GO

-- Tabla: Operaciones
CREATE TABLE Operaciones (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    ValorUnitario DECIMAL(10,2) CONSTRAINT DF_ValorUnitario DEFAULT 0.00
);
GO


-- Tabla: Produccion
CREATE TABLE Produccion (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE,
    TotalValor DECIMAL(10,2),
    UsuarioId INT NOT NULL,
    PrendaId INT NOT NULL,
    CONSTRAINT FK_Produccion_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Produccion_Prenda FOREIGN KEY (PrendaId) REFERENCES Prendas(Id) ON DELETE CASCADE
);
GO

-- Tabla: ProduccionDetalle
CREATE TABLE ProduccionDetalle (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Cantidad INT NOT NULL,
    ProduccionId INT NOT NULL,
    OperacionId INT NOT NULL,
    ValorTotal DECIMAL(10, 2) NULL,
    FOREIGN KEY (ProduccionId) REFERENCES Produccion(Id) ON DELETE CASCADE,
    FOREIGN KEY (OperacionId) REFERENCES Operaciones(Id)
);
GO

-- Tabla: Horarios
CREATE TABLE Horarios (
    HorarioId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  
    EmpleadoId INT NOT NULL,
    Tipo NVARCHAR(50) NOT NULL,
    Hora TIME NOT NULL,
    Fecha DATE NOT NULL,
    Observaciones NVARCHAR(MAX) NULL,
    CONSTRAINT FK_Horarios_Usuarios FOREIGN KEY (EmpleadoId) REFERENCES Usuarios(Id) ON DELETE CASCADE
);
GO

-- Tabla: Metas
CREATE TABLE Metas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE,
    MetaCorte INT,
    ProduccionReal INT,
    UsuarioId INT,
    FechaHora DATETIME,
    Mensaje NVARCHAR(500),
    CONSTRAINT FK_Meta_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id) ON DELETE CASCADE
);
GO

-- Tabla: MensajesChat
CREATE TABLE MensajesChat (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FechaHora DATETIME,
    Mensaje NVARCHAR(MAX),
    RemitenteId INT NOT NULL,
    DestinatarioId INT NOT NULL,
    CONSTRAINT FK_Chat_Remitente FOREIGN KEY (RemitenteId) REFERENCES Usuarios(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Chat_Destinatario FOREIGN KEY (DestinatarioId) REFERENCES Usuarios(Id) ON DELETE NO ACTION
);


-- Tabla: Contactos
CREATE TABLE Contactos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    NombreCompleto NVARCHAR(100),
    Telefono NVARCHAR(20),
    Correo NVARCHAR(100),
    Observacion NVARCHAR(MAX)
);
GO

-- Tabla: Pagos
CREATE TABLE Pagos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL,
    FechaInicio DATE NOT NULL,
    FechaFin DATE NOT NULL,
    TotalPagado DECIMAL(10,2) NOT NULL,
    FechaPago DATETIME DEFAULT GETDATE(),
    Observaciones NVARCHAR(MAX),
    CONSTRAINT FK_Pagos_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id) ON DELETE CASCADE
);


-- Tabla: PagosProveedor
CREATE TABLE PagosProveedor (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProveedorId INT NOT NULL,
    CantidadPrendas INT NOT NULL,
    PrecioUnitario DECIMAL(10, 2) NOT NULL,
    TotalPagado DECIMAL(12, 2) NOT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    Observaciones NVARCHAR(500),
    FOREIGN KEY (ProveedorId) REFERENCES Usuarios(Id)
);

-- ===========================
-- DATOS DE PRUEBA (INSERTs)
-- ===========================
-- Insertar roles
INSERT INTO Roles (Nombre) VALUES 
('Administrador'),
('Empleado'),
('Proveedor');
GO
-- Usuarios
INSERT INTO Usuarios (Nombre, Documento, Correo, Contraseña, RolId, Edad, Telefono)
VALUES 
('Juan Pérez', '123456789', 'juan.perez@example.com', 'hashed_password_1', 2, 30, '3001234567'),
('María Gómez', '987654321', 'maria.gomez@example.com', 'hashed_password_2', 1, 40, '3007654321'),
('Carlos López', '456789123', 'carlos.lopez@example.com', 'hashed_password_3', 3, 25, '3005551234'),
('Yessica Franco', '111222333', 'yessica.franco@example.com', 'hashed_password_4', 2, 28, '3001112223'),
('Viviana Sanchez', '222333444', 'viviana.sanchez@example.com', 'hashed_password_5', 2, 34, '3002223334'),
('Camilo Perez', '333444555', 'camilo.perez@example.com', 'hashed_password_6', 2, 29, '3003334445'),
('Andrea Vargas', '444555666', 'andrea.vargas@example.com', 'hashed_password_7', 2, 31, '3004445556'),
('Luis Ramirez', '555666777', 'luis.ramirez@example.com', 'hashed_password_8', 2, 26, '3005556667'),
('Maria Lopez', '666777888', 'maria.lopez2@example.com', 'hashed_password_9', 2, 27, '3006667778'),
('Daniela Ruales', '777888999', 'daniela.ruales@example.com', 'hashed_password_10', 2, 30, '3007778889'),
('Fernanda Moreno', '888999000', 'fernanda.moreno@example.com', 'hashed_password_11', 2, 33, '3008889990');
GO

-- Prendas
INSERT INTO Prendas (Nombre, Genero, Color, CantidadPrendas)
VALUES 
('Camisa Clásica', 'Unisex', 'Blanco', 2000),   --1
('Pantalón Slim', 'Masculino', 'Azul', 1500),   --2
('Camisa Crop Top', 'Dama', 'Rosa', 2500),      --3
('Camiseta Básica', 'Hombre', 'Negro', 2400),   --4
('Jogger Casual', 'Niño', 'Gris', 1800),        --5
('Jogger Deportivo', 'Dama', 'Azul', 2000),     --6
('Leggins Deportivo', 'Juvenil', 'Negro', 2500),--7
('Falda', 'Femenino', 'Rojo', 1500);            --8
GO

-- Operaciones
INSERT INTO Operaciones (Nombre, ValorUnitario)
VALUES 
('Corte', 500.00),              --1
('Costura', 400.00),			--2
('Revisión', 200.00),			--3
('Pegar Cuello', 300.00),		--4
('Pegar Manga', 100.00),		--5
('Unir Hombros', 200.00),		--6
('Cerrar Camiseta', 300.00),	--7
('Centrar Cuello', 400.00),		--8
('Dobladillo Mangas', 500.00),	--9
('Dobladillo Ruedo', 200.00),	--10
('Camiseta Dama', 200.00),		--11		
('Camiseta Hombre', 200.00),	--12
('Planchado', 100.00);			--13
GO

-- Produccion
INSERT INTO Produccion (Fecha, TotalValor, UsuarioId, PrendaId)
VALUES 
('2025-04-19', 3500.00, 1, 1),  -- 1 Juan Perez - Camisa Clásica
('2025-04-19', 2100.00, 4, 2),  -- 2 Yessica Franco - Pantalón Slim
('2025-04-18', 4200.00, 5, 7),  -- 3 Viviana Sanchez - Leggins Deportivo
('2025-04-18', 2800.00, 11, 3),  -- 4 Fernanda Moreno - Camisa Crop Top
('2025-12-10', 5000.00, 1, 6),  -- 5 Juan Perez - Jogger Deportivo
('2025-12-10', 3000.00, 5, 8),  -- 6 Viviana Sanchez - Pantalón Slim
('2025-12-09', 4000.00, 1, 1),  -- 7 Juan Perez - Camisa Clásica
('2025-12-09', 3500.00, 11, 5),  -- 8 Fernanda Moreno - Jogger Casual
('2025-09-05', 8100.00, 1, 2),  -- 9 Juan Perez - Pantalón Slim
('2025-05-01', 2800.00, 6, 8),  -- 10 Camilo Perez - Falda
('2025-05-01', 2600.00, 4, 4), -- 11 Yessica Franco - Camiseta Básica
('2025-05-02', 3000.00, 6, 5),  -- 12 Camilo Perez - Jogger Casual
('2025-05-02', 3200.00, 7, 6),  -- 13 Andrea Vargas - Jogger Deportivo
('2025-05-03', 2500.00, 8, 7),  -- 14 Luis Ramirez - Leggins Deportivo
('2025-05-03', 2700.00, 9, 3),  -- 15 Maria Lopez - Camisa Crop Top
('2025-05-04', 2900.00, 10, 4), -- 16 Daniela Ruales - Camiseta Básica
('2025-05-04', 3100.00, 11, 5), -- 17 Fernanda Moreno - Jogger Casual
('2025-05-20', 8500.00, 10, 7), -- 18 Daniela Ruales - Leggins Deportivo
('2025-05-05', 284200.00, 11, 4), -- 19 Fernanda Moreno - Camiseta Básica
('2025-05-10', 300000.00, 6, 5), -- 20 Camilo Perez - Jogger Casual
('2025-05-12', 163150.00, 7, 6), -- 21 Andrea Vargas - Jogger Deportivo
('2025-05-15', 280000.00, 8, 7), -- 22 Luis Ramirez - Leggins Deportivo
('2025-05-29', 890000.00, 11, 3), -- 23 Fernanda Moreno - Camisa Crop Top	
('2025-05-05', 4000.00, 4, 3),  -- 24 Yessica Franco - Camisa Crop Top
('2025-06-01', 3000.00, 9, 2),  -- 25 Maria Lopez - Pantalón Slim
('2025-06-02', 4500.00, 1, 8); -- 26 Juan Perez - Falda		
GO

-- ProduccionDetalle
INSERT INTO ProduccionDetalle (Cantidad, ProduccionId, OperacionId, ValorTotal)
VALUES 
(5, 1, 1, 2500.00), -- Corte (500 x 5)
(3, 1, 2, 2100.00), -- Costura (700 x 3)
(2, 2, 3, 600.00), -- Revisión (300 x 2)
(4, 3, 4, 3200.00), -- Pegar Cuello (800 x 4)
(2, 3, 5, 1300.00), -- Centrar Cuello (650 x 2)
(3, 4, 6, 1500.00), -- Dobladillo Mangas (500 x 3)
(3, 4, 7, 1650.00), -- Dobladillo Ruedo (550 x 3)
(1, 5, 8, 2000.00), -- Camiseta Dama (2000 x 1)
(1, 5, 2, 700.00),  -- Costura (700 x 1)
(2, 6, 9, 4400.00), -- Camiseta Hombre (2200 x 2)
(5, 7, 3, 1500.00), -- Revisión (300 x 5)
(2, 7, 1, 1000.00), -- Corte (500 x 2)
(3, 8, 10, 2400.00), -- Pegar Cuello (800 x 3)
(5, 12, 2, 3500.00),  -- Costura (700 x 5)
(70, 18, 2, 49000.00), -- Costura (700 x 70)
(85, 19, 10, 46750.00), -- Dobladillo Ruedo (550 x 85)
(35, 20, 2, 24500.00), -- Costura (700 x 35)
(77, 21, 3, 23100.00), -- Revisión (300 x 77)
(300, 22, 2, 210000.00), -- Costura (700 x 300)
(200, 23, 1, 100000.00); -- Corte (500 x 200)
GO

-- Horarios
INSERT INTO Horarios (EmpleadoId, Tipo, Hora, Fecha, Observaciones)
VALUES
(1, 'Entrada', '08:00:00', '2025-05-22', 'Llegada puntual'),
(1, 'Salida', '17:00:00', '2025-05-22', 'Salida normal'),
(4, 'Entrada', '09:00:00', '2025-05-22', 'Llegada tarde'),
(6, 'Entrada', '08:30:00', '2025-05-22', 'Llegada puntual'),
(9, 'Descanso', '12:00:00', '2025-05-22', 'Descanso almuerzo'),
(5, 'Entrada', '08:00:00', '2025-06-01', 'Inicio de jornada'),
(5, 'Descanso', '12:00:00', '2025-05-22', 'Descanso almuerzo'),
(5, 'Salida', '17:00:00', '2025-06-01', 'Fin de jornada');
GO

-- Metas
INSERT INTO Metas (Fecha, MetaCorte, ProduccionReal, UsuarioId, FechaHora, Mensaje)
VALUES 
('2025-04-19', 50, 45, 6, GETDATE(), 'Buena producción, pero faltaron 5 unidades'),
('2025-04-18', 60, 62, 1, GETDATE(), 'Excelente trabajo'),
('2025-06-01', 30, 28, 5, GETDATE(), 'Buena producción, pero faltaron 2 unidades');
GO

-- MensajesChat
INSERT INTO MensajesChat (FechaHora, Mensaje, RemitenteId, DestinatarioId)
VALUES 
(GETDATE(), 'Hola, ¿cómo va la producción de camisas?', 2, 5),
(GETDATE(), 'Todo en orden, ya casi llegamos a la meta.', 5, 2);
GO

-- Contacto
INSERT INTO Contactos (NombreCompleto, Telefono, Correo, Observacion)
VALUES 
('Andres López', '3001234567', 'andres.lopez@proveedor.com', 'Posible proveedor de insumos.'),
('Maria Torres', '3017654321', 'maria.torres@proveedor.com', 'Contacto para nuevos diseños.'),
('Javier Martínez', '600998877', 'javier@proveedor.com', 'Proveedor de telas de algodón');
GO

-- Pagos
INSERT INTO Pagos (UsuarioId, FechaInicio, FechaFin, TotalPagado, Observaciones)
VALUES 
(1, '2025-06-01', '2025-06-15', 85000.00, 'Pago quincenal basado en producción de junio');

-- PagosProveedor
INSERT INTO PagosProveedor (ProveedorId, CantidadPrendas, PrecioUnitario, TotalPagado, Observaciones)
VALUES (2, 100, 500, 50000, 'Pago por lote entregado el 25 de junio');


-- ===========================
-- Consultar producción y detalles:
-- ===========================
SELECT p.Fecha, u.Nombre AS Usuario, pr.Nombre AS Prenda, pd.Cantidad, o.Nombre AS Operacion, pd.ValorTotal
FROM ProduccionDetalle pd
JOIN Produccion p ON p.Id = pd.ProduccionId
JOIN Operaciones o ON o.Id = pd.OperacionId
JOIN Usuarios u ON u.Id = p.UsuarioId
JOIN Prendas pr ON pr.Id = p.PrendaId;


-- ===========================
-- Ver historial de horarios:
-- ===========================
SELECT u.Nombre, h.Tipo, h.Fecha, h.Hora, h.Observaciones
FROM Horarios h
JOIN Usuarios u ON u.Id = h.EmpleadoId;


-- ===========================
-- Comprobar mensajes enviados entre usuarios:
-- ===========================
SELECT u1.Nombre AS Remitente, u2.Nombre AS Destinatario, m.Mensaje, m.FechaHora
FROM MensajesChat m
JOIN Usuarios u1 ON u1.Id = m.RemitenteId
JOIN Usuarios u2 ON u2.Id = m.DestinatarioId;


-- ===========================
-- PROCEDIMIENTOS ALMACENADOS (sp)
-- ===========================

GO
--Este procedimiento devuelve todas las producciones de un usuario específico
CREATE OR ALTER PROCEDURE sp_DetalleProduccionUsuario
    @UsuarioId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        U.Id AS UsuarioId,
        U.Nombre AS NombreUsuario,
        P.Id AS ProduccionId,
        P.Fecha,
        P.TotalValor AS TotalProducido,
        PD.Id AS DetalleId,
        O.Nombre AS Operacion,
        PD.Cantidad,
        PD.ValorTotal
    FROM Produccion P
    INNER JOIN Usuarios U ON P.UsuarioId = U.Id
    INNER JOIN ProduccionDetalle PD ON P.Id = PD.ProduccionId
    INNER JOIN Operaciones O ON PD.OperacionId = O.Id
    WHERE U.Id = @UsuarioId
    ORDER BY P.Fecha DESC, PD.Id ASC;
END;
GO



--Devuelve un detalle diario de producción por usuario para un mes y año específicos.
CREATE OR ALTER PROCEDURE sp_ProduccionMensual
    @Año INT,
    @Mes INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        U.Nombre AS NombreUsuario,
        P.Fecha,
        SUM(P.TotalValor) AS ProduccionTotal
    FROM Produccion P
    INNER JOIN Usuarios U ON U.Id = P.UsuarioId
    WHERE MONTH(P.Fecha) = @Mes AND YEAR(P.Fecha) = @Año
    GROUP BY U.Nombre, P.Fecha
    ORDER BY P.Fecha;
END;
GO

--Devuelve un total mensual de producción general
CREATE OR ALTER PROCEDURE sp_ProduccionMensualResumen
    @Año INT,
    @Mes INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        SUM(P.TotalValor) AS TotalProduccionMensual
    FROM Produccion P
    WHERE MONTH(P.Fecha) = @Mes AND YEAR(P.Fecha) = @Año;
END;
GO

--Devuelve un total de pago quincenal de los empleados
CREATE OR ALTER PROCEDURE sp_GenerarPagoQuincenal
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Pagos (UsuarioId, FechaInicio, FechaFin, TotalPagado, Observaciones)
SELECT 
    P.UsuarioId,
    @FechaInicio,
    @FechaFin,
    SUM(PD.Cantidad * O.ValorUnitario),
    CONCAT('Pago del ', CONVERT(varchar, @FechaInicio, 103), ' al ', CONVERT(varchar, @FechaFin, 103), ' generado automáticamente el ', CONVERT(varchar, GETDATE(), 103))
    FROM ProduccionDetalle PD
    INNER JOIN Produccion P ON P.Id = PD.ProduccionId
    INNER JOIN Operaciones O ON O.Id = PD.OperacionId
    WHERE P.Fecha BETWEEN @FechaInicio AND @FechaFin
    GROUP BY P.UsuarioId;
END;









