-- Script de procedimientos almacenados para tabla Productos (NeptunoDB)
-- Comprueba y agrega la columna Estado si no existe
IF COL_LENGTH('Productos','Estado') IS NULL
BEGIN
	ALTER TABLE dbo.Productos ADD Estado BIT NOT NULL CONSTRAINT DF_Productos_Estado DEFAULT(1);
END

GO

IF OBJECT_ID('dbo.sp_ListarProductos', 'P') IS NOT NULL
	DROP PROCEDURE dbo.sp_ListarProductos;
GO
CREATE PROCEDURE dbo.sp_ListarProductos
AS
BEGIN
	SET NOCOUNT ON;
	SELECT idproducto, nombreProducto, precioUnidad, unidadesEnExistencia, Estado
	FROM dbo.Productos
	WHERE Estado = 1
	ORDER BY nombreProducto;
END
GO

IF OBJECT_ID('dbo.sp_InsertarProducto', 'P') IS NOT NULL
	DROP PROCEDURE dbo.sp_InsertarProducto;
GO
CREATE PROCEDURE dbo.sp_InsertarProducto
	@nombreProducto NVARCHAR(100),
	@precioUnidad DECIMAL(18,2),
	@unidadesEnExistencia INT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.Productos (nombreProducto, precioUnidad, unidadesEnExistencia, Estado)
	VALUES (@nombreProducto, @precioUnidad, @unidadesEnExistencia, 1);

	SELECT SCOPE_IDENTITY() AS NewId;
END
GO

IF OBJECT_ID('dbo.sp_ActualizarProducto', 'P') IS NOT NULL
	DROP PROCEDURE dbo.sp_ActualizarProducto;
GO
CREATE PROCEDURE dbo.sp_ActualizarProducto
	@idproducto INT,
	@nombreProducto NVARCHAR(100),
	@precioUnidad DECIMAL(18,2),
	@unidadesEnExistencia INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.Productos
	SET nombreProducto = @nombreProducto,
		precioUnidad = @precioUnidad,
		unidadesEnExistencia = @unidadesEnExistencia
	WHERE idproducto = @idproducto;
END
GO

IF OBJECT_ID('dbo.sp_EliminarProducto', 'P') IS NOT NULL
	DROP PROCEDURE dbo.sp_EliminarProducto;
GO
CREATE PROCEDURE dbo.sp_EliminarProducto
	@idproducto INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.Productos
	SET Estado = 0
	WHERE idproducto = @idproducto;
END
GO

IF OBJECT_ID('dbo.sp_BuscarProductos', 'P') IS NOT NULL
	DROP PROCEDURE dbo.sp_BuscarProductos;
GO
CREATE PROCEDURE dbo.sp_BuscarProductos
	@Nombre NVARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT idproducto, nombreProducto, precioUnidad, unidadesEnExistencia, Estado
	FROM dbo.Productos
	WHERE Estado = 1 AND nombreProducto LIKE '%' + @Nombre + '%'
	ORDER BY nombreProducto;
END
GO
