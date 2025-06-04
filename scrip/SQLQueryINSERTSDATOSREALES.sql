INSERT INTO Prendas (Nombre, Genero, Color)
VALUES
('Camisa Crop Top', 'Dama', 'Rosa'),
('Camiseta Básica', 'Hombre', 'Negro'),
('Jogger Casual', 'Niño', 'Gris'),
('Jogger Deportivo', 'Dama', 'Azul'),
('Leggins Deportivo', 'Juvenil', 'Negro');
GO

-- Insertar más empleados en Usuarios
INSERT INTO Usuarios (Nombre, Documento, Correo, Contraseña, Rol, Edad, Telefono, OperacionId)
VALUES
('Yessica Franco', '111222333', 'yessica.franco@example.com', 'hashed_password_4', 'Empleado', 28, '3001112223', NULL),
('Viviana Sanchez', '222333444', 'viviana.sanchez@example.com', 'hashed_password_5', 'Empleado', 34, '3002223334', NULL),
('Camilo Perez', '333444555', 'camilo.perez@example.com', 'hashed_password_6', 'Empleado', 29, '3003334445', NULL),
('Andrea Vargas', '444555666', 'andrea.vargas@example.com', 'hashed_password_7', 'Empleado', 31, '3004445556', NULL),
('Luis Ramirez', '555666777', 'luis.ramirez@example.com', 'hashed_password_8', 'Empleado', 26, '3005556667', NULL),
('Maria Lopez', '666777888', 'maria.lopez2@example.com', 'hashed_password_9', 'Empleado', 27, '3006667778', NULL),
('Daniela Ruales', '777888999', 'daniela.ruales@example.com', 'hashed_password_10', 'Empleado', 30, '3007778889', NULL),
('Fernanda Moreno', '888999000', 'fernanda.moreno@example.com', 'hashed_password_11', 'Empleado', 33, '3008889990', NULL);
GO


-- Insertar más operaciones en la tabla Operaciones
INSERT INTO Operaciones (Nombre, ValorUnitario)
VALUES
('Pegar Cuello', 800.00),
('Pegar Manga', 700.00),
('Unir Hombros', 600.00),
('Cerrar Camiseta', 750.00),
('Centrar Cuello', 650.00),
('Dobladillo Mangas', 500.00),
('Dobladillo Ruedo', 550.00),
('Camiseta Dama', 2000.00),
('Camiseta Hombre', 2200.00);
GO

-- Producciones diarias de los empleados con las prendas nuevas
INSERT INTO Produccion (Fecha, TotalValor, UsuarioId, PrendaId)
VALUES 
('2025-05-01', 2800.00, 6, 3),  -- Yessica Franco - Camisa Crop Top
('2025-05-01', 2600.00, 12, 4),  -- Viviana Sanchez - Camiseta Básica
('2025-05-02', 3000.00, 6, 5),  -- Camilo Perez - Jogger Casual
('2025-05-02', 3200.00, 7, 6),  -- Andrea Vargas - Jogger Deportivo
('2025-05-03', 2500.00, 8, 7),  -- Luis Ramirez - Leggins Deportivo
('2025-05-03', 2700.00, 9, 3),  -- Maria Lopez - Camisa Crop Top
('2025-05-04', 2900.00, 10, 4), -- Daniela Ruales - Camiseta Básica
('2025-05-04', 3100.00, 11, 5); -- Fernanda Moreno - Jogger Casual
GO

-- ProduccionDetalle para Producciones del 3 al 8
INSERT INTO ProduccionDetalle (Cantidad, ProduccionId, OperacionId, ValorTotal)
VALUES 
(4, 3, 4, 2400.00), -- Cerrar Camiseta (600 x 4)
(2, 3, 5, 1300.00), -- Centrar Cuello (650 x 2)

(3, 4, 6, 1500.00), -- Dobladillo Mangas (500 x 3)
(3, 4, 7, 1650.00), -- Dobladillo Ruedo (550 x 3)

(1, 5, 8, 2000.00), -- Camiseta Dama (2000 x 1)
(1, 5, 2, 700.00),  -- Costura

(2, 6, 9, 4400.00), -- Camiseta Hombre (2200 x 2)

(5, 7, 3, 1500.00), -- Revisión (300 x 5)
(2, 7, 1, 1000.00), -- Corte (500 x 2)

(3, 8, 10, 2400.00); -- Pegar Cuello (800 x 3)
GO

SELECT Id FROM Produccion;
SELECT Id FROM Operaciones;

SELECT Id, Nombre FROM Prendas;
SELECT Id, Nombre FROM Usuarios WHERE Rol = 'Empleado';



SELECT P.Id, P.Nombre, PR.Fecha
FROM Produccion PR
JOIN Prendas P ON PR.PrendaId = P.Id
WHERE MONTH(PR.Fecha) = 5 AND YEAR(PR.Fecha) = 2025;


SELECT PD.ProduccionId, PR.PrendaId, P.Nombre AS Prenda, SUM(PD.Cantidad) AS TotalCantidad
FROM ProduccionDetalle PD
JOIN Produccion PR ON PD.ProduccionId = PR.Id
JOIN Prendas P ON PR.PrendaId = P.Id
WHERE MONTH(PR.Fecha) = 5 AND YEAR(PR.Fecha) = 2025
GROUP BY PD.ProduccionId, PR.PrendaId, P.Nombre;


SELECT Id,Nombre FROM Prendas WHERE Nombre = 'Camisa Crop Top';

INSERT INTO Produccion (Fecha, TotalValor, UsuarioId, PrendaId)
VALUES ('2025-05-05', 4000.00, 4, 3);  -- Fecha en mayo 2025



INSERT INTO ProduccionDetalle (Cantidad, ProduccionId, OperacionId, ValorTotal)
VALUES (5, 30, 2, 3500.00);  -- 700 x 5


-- Producción de Camisa Crop Top
INSERT INTO Produccion (Fecha, TotalValor, UsuarioId, PrendaId) VALUES ('2025-05-20', 8500.00, 10, 7);
DECLARE @prod1 INT = SCOPE_IDENTITY();
INSERT INTO ProduccionDetalle (Cantidad, ProduccionId, OperacionId, ValorTotal) VALUES (70, @prod1, 2, 150500.00);  -- Costura

-- Producción de Camiseta Básica
INSERT INTO Produccion (Fecha, TotalValor, UsuarioId, PrendaId) VALUES ('2025-05-05', 284200.00, 12, 4);
DECLARE @prod2 INT = SCOPE_IDENTITY();
INSERT INTO ProduccionDetalle (Cantidad, ProduccionId, OperacionId, ValorTotal) VALUES (85, @prod2, 2, 284200.00);  -- Costura

-- Producción de Jogger Casual
INSERT INTO Produccion (Fecha, TotalValor, UsuarioId, PrendaId) VALUES ('2025-05-10', 300000.00, 6, 5);
DECLARE @prod3 INT = SCOPE_IDENTITY();
INSERT INTO ProduccionDetalle (Cantidad, ProduccionId, OperacionId, ValorTotal) VALUES (300, @prod3, 1, 150000.00);  -- Corte
INSERT INTO ProduccionDetalle (Cantidad, ProduccionId, OperacionId, ValorTotal) VALUES (35, @prod3, 2, 150000.00);  -- Costura

-- Producción de Jogger Deportivo
INSERT INTO Produccion (Fecha, TotalValor, UsuarioId, PrendaId) VALUES ('2025-05-12', 163150.00, 7, 6);
DECLARE @prod4 INT = SCOPE_IDENTITY();
INSERT INTO ProduccionDetalle (Cantidad, ProduccionId, OperacionId, ValorTotal) VALUES (100, @prod4, 2, 92100.00);  -- Costura
INSERT INTO ProduccionDetalle (Cantidad, ProduccionId, OperacionId, ValorTotal) VALUES (77, @prod4, 3, 71050.00);  -- Revisión

-- Producción de Leggins Deportivo
INSERT INTO Produccion (Fecha, TotalValor, UsuarioId, PrendaId) VALUES ('2025-05-15', 280000.00, 8, 7);
DECLARE @prod5 INT = SCOPE_IDENTITY();
INSERT INTO ProduccionDetalle (Cantidad, ProduccionId, OperacionId, ValorTotal) VALUES (400, @prod5, 2, 280000.00);  -- Costura

-- Producción de Camisa Crop Top
INSERT INTO Produccion (Fecha, TotalValor, UsuarioId, PrendaId) VALUES ('2025-05-29', 890000.00, 11, 3);
DECLARE @prod6 INT = SCOPE_IDENTITY();
INSERT INTO ProduccionDetalle (Cantidad, ProduccionId, OperacionId, ValorTotal) VALUES (200, @prod6, 1, 890000.00);  -- Costura