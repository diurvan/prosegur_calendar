USE [master]
GO
/****** Object:  Database [prosegur]    Script Date: 09/01/2020 08:54:09 ******/
CREATE DATABASE [prosegur]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'prosegur', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\prosegur.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'prosegur_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\prosegur_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [prosegur] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [prosegur].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [prosegur] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [prosegur] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [prosegur] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [prosegur] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [prosegur] SET ARITHABORT OFF 
GO
ALTER DATABASE [prosegur] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [prosegur] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [prosegur] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [prosegur] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [prosegur] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [prosegur] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [prosegur] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [prosegur] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [prosegur] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [prosegur] SET  DISABLE_BROKER 
GO
ALTER DATABASE [prosegur] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [prosegur] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [prosegur] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [prosegur] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [prosegur] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [prosegur] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [prosegur] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [prosegur] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [prosegur] SET  MULTI_USER 
GO
ALTER DATABASE [prosegur] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [prosegur] SET DB_CHAINING OFF 
GO
ALTER DATABASE [prosegur] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [prosegur] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [prosegur] SET DELAYED_DURABILITY = DISABLED 
GO
USE [prosegur]
GO
/****** Object:  Table [dbo].[Evento]    Script Date: 09/01/2020 08:54:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Evento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](max) NULL,
	[Nombre] [varchar](max) NULL,
	[Fecha] [date] NULL,
	[Evaluacion] [int] NULL,
 CONSTRAINT [PK_Evento] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[USP_CRUD_EVENTO]    Script Date: 09/01/2020 08:54:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
declare @RowCount INT
exec USP_CRUD_EVENTO @Accion='S', @Correo = '', @RowCount= @RowCount OUT
*/
CREATE Procedure [dbo].[USP_CRUD_EVENTO]
@Id int = 0,
@Fecha varchar(max) = '',
@Correo varchar(max) = '',
@Nombre varchar(max) = '',
@Evaluacion int = '',
@Accion varchar(max)='S',
@RowCount   INT OUTPUT
AS  
IF @Accion = 'S'
BEGIN
	Select Id, Email, Nombre, Fecha, Evaluacion
	From Evento
	where 
	(@Fecha IS NULL OR @Fecha = '' OR Fecha = @Fecha)
	--and (@Correo IS NULL OR @Correo = '' OR Email = @Correo)
	and (Email = @Correo)
END
IF @Accion = 'C'
BEGIN
	DECLARE @EventoExistente INT;
	SELECT @EventoExistente = Count(1) FROM Evento
	where Email = @Correo and Fecha = @Fecha

	If @EventoExistente=0 BEGIN
		INSERT INTO Evento
		(Email,	Nombre, Fecha, Evaluacion)
		VALUES 
		(@Correo, @Nombre, @Fecha, @Evaluacion)
		
		Select @RowCount=SCOPE_IDENTITY()
	END
	ELSE
		Select @RowCount=-99--Periodo Existente
END
IF(@Accion = 'D') BEGIN
	DELETE Evento
	WHERE ID = @Id
		
	Select @RowCount= @Id
END
GO
USE [master]
GO
ALTER DATABASE [prosegur] SET  READ_WRITE 
GO
