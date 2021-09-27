USE [master]
GO
/****** Object:  Database [Prueba_LD_DiegoMatarritaPereira]    Script Date: 9/26/2021 6:39:47 PM ******/
CREATE DATABASE [Prueba_LD_DiegoMatarritaPereira]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Prueba_LD_DiegoMatarritaPereira', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Prueba_LD_DiegoMatarritaPereira.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Prueba_LD_DiegoMatarritaPereira_log', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Prueba_LD_DiegoMatarritaPereira_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Prueba_LD_DiegoMatarritaPereira].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET ARITHABORT OFF 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET RECOVERY FULL 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET  MULTI_USER 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Prueba_LD_DiegoMatarritaPereira', N'ON'
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET QUERY_STORE = OFF
GO
USE [Prueba_LD_DiegoMatarritaPereira]
GO
/****** Object:  UserDefinedTableType [dbo].[TYPE_FE]    Script Date: 9/26/2021 6:39:48 PM ******/
CREATE TYPE [dbo].[TYPE_FE] AS TABLE(
	[IdArticulo] [int] NOT NULL,
	[cantidadProducto] [int] NOT NULL,
	[cedulaCliente] [varchar](15) NOT NULL
)
GO
/****** Object:  Table [dbo].[Articulo]    Script Date: 9/26/2021 6:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articulo](
	[IdArticulo] [int] IDENTITY(1,1) NOT NULL,
	[NombreArticulo] [varchar](30) NOT NULL,
	[PrecioArticulo] [numeric](18, 2) NOT NULL,
	[DescripcionArticulo] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Articulo] PRIMARY KEY CLUSTERED 
(
	[IdArticulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 9/26/2021 6:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[CedulaCliente] [varchar](15) NOT NULL,
	[NombreCliente] [varchar](20) NOT NULL,
	[Apellido] [varchar](40) NOT NULL,
	[FechaCreacion] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[CedulaCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleFactura]    Script Date: 9/26/2021 6:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleFactura](
	[LineaDetalle] [bigint] IDENTITY(1,1) NOT NULL,
	[IdFactura] [bigint] NOT NULL,
	[IdArticulo] [int] NOT NULL,
	[Precio] [numeric](18, 2) NOT NULL,
	[TotalLinea] [numeric](18, 2) NULL,
 CONSTRAINT [PK_DetalleFactura] PRIMARY KEY CLUSTERED 
(
	[LineaDetalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EncabezadoFactura]    Script Date: 9/26/2021 6:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EncabezadoFactura](
	[IdFactura] [bigint] IDENTITY(1,1) NOT NULL,
	[CedulaCliente] [varchar](15) NOT NULL,
	[FechaFactura] [datetime2](7) NULL,
 CONSTRAINT [PK_EncabezadoFactura] PRIMARY KEY CLUSTERED 
(
	[IdFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ErrorLog]    Script Date: 9/26/2021 6:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorLog](
	[IdLog] [int] IDENTITY(1,1) NOT NULL,
	[FechaLog] [datetime2](7) NOT NULL,
	[Clase] [varchar](30) NOT NULL,
	[Metodo] [varchar](50) NOT NULL,
	[MensajeErrorLog] [varchar](8000) NOT NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[IdLog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DetalleFactura]  WITH CHECK ADD  CONSTRAINT [FK_DetalleFactura_EncabezadoFactura] FOREIGN KEY([IdFactura])
REFERENCES [dbo].[EncabezadoFactura] ([IdFactura])
GO
ALTER TABLE [dbo].[DetalleFactura] CHECK CONSTRAINT [FK_DetalleFactura_EncabezadoFactura]
GO
ALTER TABLE [dbo].[EncabezadoFactura]  WITH CHECK ADD  CONSTRAINT [FK_EncabezadoFactura_Cliente] FOREIGN KEY([CedulaCliente])
REFERENCES [dbo].[Cliente] ([CedulaCliente])
GO
ALTER TABLE [dbo].[EncabezadoFactura] CHECK CONSTRAINT [FK_EncabezadoFactura_Cliente]
GO
/****** Object:  StoredProcedure [dbo].[usp_ActualizaArticulo]    Script Date: 9/26/2021 6:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************
-- Author: Diego Matarrita Pereira
-- Create date: 22/09/2021
-- Description:  Actualiza los articulos
********************************************************************************************
										MODIFICATION
********************************************************************************************
USER                                    DATE(dd/MM/YYYY)                  DESCRIPTION
*********************************************************************************************/
CREATE PROCEDURE [dbo].[usp_ActualizaArticulo]
	@idArticulo				INT,
	@nombreArticulo			VARCHAR(30),
	@precioArticulo			NUMERIC(18,2),
	@descripcionArticulo	VARCHAR(100)
AS

BEGIN

    SET NOCOUNT ON;

    BEGIN TRY
		
		UPDATE dbo.Articulo
		SET NombreArticulo = COALESCE(@nombreArticulo, NombreArticulo),
			PrecioArticulo = COALESCE(@precioArticulo, PrecioArticulo),
			DescripcionArticulo = COALESCE(@descripcionArticulo, DescripcionArticulo)
		WHERE IdArticulo = @idArticulo

    END TRY

    BEGIN CATCH

        THROW;

    END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[usp_ActualizaCliente]    Script Date: 9/26/2021 6:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************
-- Author: Diego Matarrita Pereira
-- Create date: 23/09/2021
-- Description:  Actualiza Datos del cliente
********************************************************************************************
										MODIFICATION
********************************************************************************************
USER                                    DATE(dd/MM/YYYY)                  DESCRIPTION
*********************************************************************************************/
CREATE PROCEDURE [dbo].[usp_ActualizaCliente]
	@cedulaCliente		VARCHAR(15),
	@nombreCliente		VARCHAR(20),
	@apellidoCliente	VARCHAR(40)
AS

BEGIN

    SET NOCOUNT ON;

    BEGIN TRY
		
		UPDATE dbo.Cliente
		SET NombreCliente = COALESCE(@nombreCliente, NombreCliente),
			Apellido = COALESCE(@apellidoCliente, Apellido)
		WHERE CedulaCliente = @cedulaCliente

    END TRY

    BEGIN CATCH

        THROW;

    END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[usp_EliminaArticulo]    Script Date: 9/26/2021 6:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************
-- Author: Diego Matarrita Pereira
-- Create date: 22/09/2021
-- Description:  Elimina articulo
********************************************************************************************
										MODIFICATION
********************************************************************************************
USER                                    DATE(dd/MM/YYYY)                  DESCRIPTION
*********************************************************************************************/
CREATE PROCEDURE [dbo].[usp_EliminaArticulo]
	@idArticulo INT
AS

BEGIN

    SET NOCOUNT ON;

    BEGIN TRY
		
		DELETE dbo.Articulo
		WHERE IdArticulo = @idArticulo

    END TRY

    BEGIN CATCH

        THROW;

    END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[usp_EliminaCliente]    Script Date: 9/26/2021 6:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************
-- Author: Diego Matarrita Pereira
-- Create date: 23/09/2021
-- Description:  Elimina cliente
********************************************************************************************
										MODIFICATION
********************************************************************************************
USER                                    DATE(dd/MM/YYYY)                  DESCRIPTION
*********************************************************************************************/
CREATE PROCEDURE [dbo].[usp_EliminaCliente]
	@cedulaCliente	VARCHAR(15)
AS

BEGIN

    SET NOCOUNT ON;

    BEGIN TRY
		
		DECLARE @tieneFac INT

		SELECT @tieneFac = COUNT(IdFactura)
		FROM dbo.EncabezadoFactura
		WHERE cedulaCliente = @cedulaCliente

		IF (@tieneFac > 0)
			BEGIN 
				SELECT 'No se puede eliminar el cliente ya que posee facturas' AS Mensaje
			END
		ELSE
			BEGIN
				DELETE Cliente 
				WHERE CedulaCliente = @cedulaCliente
				SELECT 'Se eliminó correctamente' AS Mensaje
			END

    END TRY

    BEGIN CATCH

        THROW;

    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[usp_InsertaArticulo]    Script Date: 9/26/2021 6:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************
-- Author: Diego Matarrita Pereira
-- Create date: 22/09/2021
-- Description:  Inserta los articulos
********************************************************************************************
										MODIFICATION
********************************************************************************************
USER                                    DATE(dd/MM/YYYY)                  DESCRIPTION
*********************************************************************************************/
CREATE PROCEDURE [dbo].[usp_InsertaArticulo]
	@nombreArticulo			VARCHAR(30),
	@precioArticulo			NUMERIC(18,2),
	@descripcionArticulo	VARCHAR(100)
AS

BEGIN

    SET NOCOUNT ON;

    BEGIN TRY
		
		INSERT INTO dbo.Articulo (NombreArticulo, PrecioArticulo, DescripcionArticulo)
		VALUES (@nombreArticulo, @precioArticulo, @descripcionArticulo)

    END TRY

    BEGIN CATCH

        THROW;

    END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[usp_InsertaCliente]    Script Date: 9/26/2021 6:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************
-- Author: Diego Matarrita Pereira
-- Create date: 22/09/2021
-- Description:  Inserta Datos del cliente
********************************************************************************************
										MODIFICATION
********************************************************************************************
USER                                    DATE(dd/MM/YYYY)                  DESCRIPTION
*********************************************************************************************/
CREATE PROCEDURE [dbo].[usp_InsertaCliente]
	@cedulaCliente		VARCHAR(15),
	@nombreCliente		VARCHAR(20),
	@apellidoCliente	VARCHAR(40)
AS

BEGIN

    SET NOCOUNT ON;

    BEGIN TRY
		
		INSERT INTO dbo.Cliente (CedulaCliente, NombreCliente, Apellido, FechaCreacion)
		VALUES (@cedulaCliente, @nombreCliente, @apellidoCliente, GETDATE())

    END TRY

    BEGIN CATCH

        THROW;

    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[usp_InsertaFactura]    Script Date: 9/26/2021 6:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************
-- Author: Diego Matarrita Pereira
-- Create date: 26/09/2021
-- Description:  Inserta la factura
********************************************************************************************
										MODIFICATION
********************************************************************************************
USER                                    DATE(dd/MM/YYYY)                  DESCRIPTION
*********************************************************************************************/
CREATE PROCEDURE [dbo].[usp_InsertaFactura]
	@TypeFE TYPE_FE  READONLY
AS

BEGIN

	BEGIN TRANSACTION 

		BEGIN TRY

			DECLARE @idFactura INT = 0,
			@cedulaCliente VARCHAR(15);

			SELECT TOP 1  @cedulaCliente = cedulaCliente 
			FROM @TypeFE

			--Inserta encabezado
			INSERT INTO dbo.EncabezadoFactura(CedulaCliente,FechaFactura)
			VALUES (@cedulaCliente, GETDATE())

			SET @idFactura = @@IDENTITY;

			-- Agregar Detalle
			INSERT INTO DetalleFactura(IdFactura, IdArticulo, Precio, TotalLinea)
			SELECT	@idFactura,
					TFE.IdArticulo,
					A.PrecioArticulo,
					TFE.cantidadProducto * A.PrecioArticulo
			FROM @TypeFE AS TFE
			JOIN Articulo AS A WITH(NOLOCK)
				ON A.IdArticulo = TFE.IdArticulo
			
			COMMIT TRANSACTION 

		END TRY

		BEGIN CATCH

			-- rollback a la transaccion
			ROLLBACK TRANSACTION

		END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[usp_InsertaLog]    Script Date: 9/26/2021 6:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************
-- Author: Diego Matarrita Pereira
-- Create date: 25/09/2021
-- Description:  Inserta los logs
********************************************************************************************
										MODIFICATION
********************************************************************************************
USER                                    DATE(dd/MM/YYYY)                  DESCRIPTION
*********************************************************************************************/
CREATE PROCEDURE [dbo].[usp_InsertaLog]
	@clase			VARCHAR(30),
	@metodo			VARCHAR(50),
	@mensajeError	VARCHAR(8000)
AS

BEGIN

    SET NOCOUNT ON;

    BEGIN TRY
		
		INSERT INTO dbo.ErrorLog (FechaLog, Clase, Metodo, MensajeErrorLog)
		VALUES (GETDATE(), @clase, @metodo, @mensajeError)

    END TRY

    BEGIN CATCH

        THROW;

    END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[usp_ObtieneArticulo]    Script Date: 9/26/2021 6:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************
-- Author: Diego Matarrita Pereira
-- Create date: 23/09/2021
-- Description:  Obtiene todos los articulos
********************************************************************************************
										MODIFICATION
********************************************************************************************
USER                                    DATE(dd/MM/YYYY)                  DESCRIPTION
*********************************************************************************************/
CREATE PROCEDURE [dbo].[usp_ObtieneArticulo]

AS

BEGIN

    SET NOCOUNT ON;

    BEGIN TRY
		
		SELECT	IdArticulo,
				NombreArticulo,
				PrecioArticulo,
				DescripcionArticulo
		FROM dbo.Articulo

    END TRY

    BEGIN CATCH

        THROW;

    END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[usp_ObtieneArticuloXId]    Script Date: 9/26/2021 6:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************
-- Author: Diego Matarrita Pereira
-- Create date: 25/09/2021
-- Description:  Obtiene todos el articulo x Id
********************************************************************************************
										MODIFICATION
********************************************************************************************
USER                                    DATE(dd/MM/YYYY)                  DESCRIPTION
*********************************************************************************************/
CREATE PROCEDURE [dbo].[usp_ObtieneArticuloXId]
	@idArticulo INT
AS

BEGIN

    SET NOCOUNT ON;

    BEGIN TRY
		
		SELECT	IdArticulo,
				NombreArticulo,
				PrecioArticulo,
				DescripcionArticulo
		FROM dbo.Articulo
		WHERE IdArticulo = @idArticulo

    END TRY

    BEGIN CATCH

        THROW;

    END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[usp_ObtieneClientes]    Script Date: 9/26/2021 6:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************
-- Author: Diego Matarrita Pereira
-- Create date: 22/09/2021
-- Description:  Obtiene todos los usuarios
********************************************************************************************
										MODIFICATION
********************************************************************************************
USER                                    DATE(dd/MM/YYYY)                  DESCRIPTION
*********************************************************************************************/
CREATE PROCEDURE [dbo].[usp_ObtieneClientes]

AS

BEGIN

    SET NOCOUNT ON;

    BEGIN TRY
		
		SELECT	CedulaCliente,
				NombreCliente,
				Apellido
		FROM dbo.Cliente

    END TRY

    BEGIN CATCH

        THROW;

    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[usp_ObtieneClientesXCedula]    Script Date: 9/26/2021 6:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************
-- Author: Diego Matarrita Pereira
-- Create date: 22/09/2021
-- Description:  Obtiene todos los usuarios por cedula
********************************************************************************************
										MODIFICATION
********************************************************************************************
USER                                    DATE(dd/MM/YYYY)                  DESCRIPTION
*********************************************************************************************/
CREATE PROCEDURE [dbo].[usp_ObtieneClientesXCedula]
	@cedulaCliente VARCHAR(15)
AS

BEGIN

    SET NOCOUNT ON;

    BEGIN TRY
		
		SELECT	CedulaCliente,
				NombreCliente,
				Apellido
		FROM dbo.Cliente
		WHERE CedulaCliente = @cedulaCliente

    END TRY

    BEGIN CATCH

        THROW;

    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[usp_ObtieneClientesXNombre]    Script Date: 9/26/2021 6:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************
-- Author: Diego Matarrita Pereira
-- Create date: 22/09/2021
-- Description:  Obtiene todos los usuarios por nombre
********************************************************************************************
										MODIFICATION
********************************************************************************************
USER                                    DATE(dd/MM/YYYY)                  DESCRIPTION
*********************************************************************************************/
CREATE PROCEDURE [dbo].[usp_ObtieneClientesXNombre]
	@nombreCliente VARCHAR(20)
AS

BEGIN

    SET NOCOUNT ON;

    BEGIN TRY
		
		SELECT	CedulaCliente,
				NombreCliente,
				Apellido
		FROM dbo.Cliente
		WHERE NombreCliente LIKE '%' + @nombreCliente + '%'

    END TRY

    BEGIN CATCH

        THROW;

    END CATCH
END
GO
USE [master]
GO
ALTER DATABASE [Prueba_LD_DiegoMatarritaPereira] SET  READ_WRITE 
GO
