USE [master]
GO

/****** Object:  Database [db_a7c6f8_hefesto]    Script Date: 20-Nov-21 04:01:38 ******/
CREATE DATABASE [db_a7c6f8_hefesto]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'db_a7c6f8_hefesto_Data', FILENAME = N'H:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\db_a7c6f8_hefesto_DATA.mdf' , SIZE = 8192KB , MAXSIZE = 1024000KB , FILEGROWTH = 10%)
 LOG ON 
( NAME = N'db_a7c6f8_hefesto_Log', FILENAME = N'H:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\db_a7c6f8_hefesto_Log.LDF' , SIZE = 3072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [db_a7c6f8_hefesto].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET ARITHABORT OFF 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET  ENABLE_BROKER 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET  MULTI_USER 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET DB_CHAINING OFF 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET QUERY_STORE = OFF
GO

ALTER DATABASE [db_a7c6f8_hefesto] SET  READ_WRITE 
GO

USE [db_a7c6f8_hefesto]
GO
/****** Object:  UserDefinedFunction [dbo].[fnc_ConsultaConta]    Script Date: 20-Nov-21 04:00:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fnc_ConsultaConta]
(
	@usuario VARCHAR(30),
	@senha VARCHAR(30)
)
RETURNS INT AS
BEGIN
	IF(EXISTS(SELECT * FROM Conta WHERE usuario = @usuario AND senha = @senha))
		RETURN 1
	
	RETURN 0
END
GO
/****** Object:  Table [dbo].[Fazenda]    Script Date: 20-Nov-21 04:00:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fazenda](
	[id] [int] NOT NULL,
	[fazendaNome] [varchar](150) NULL,
	[proprietarioID] [int] NOT NULL,
	[tamanho] [int] NULL,
	[numeroSensores] [int] NULL,
	[endereco] [varchar](150) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sensor]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sensor](
	[id] [int] NOT NULL,
	[fazendaID] [int] NOT NULL,
	[temperatura] [float] NULL,
	[umidade] [float] NULL,
	[localizacao] [varchar](max) NULL,
	[dataAtualizacao] [varchar](50) NULL,
	[condicao] [varchar](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_Monitoramento]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[vw_Monitoramento]
as
select f.fazendaNome, s.temperatura, s.umidade, s.dataAtualizacao, s.id
from Fazenda f, Sensor s
where f.id = s.fazendaID
GO
/****** Object:  Table [dbo].[Conta]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Conta](
	[usuario] [varchar](30) NOT NULL,
	[senha] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proprietario]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proprietario](
	[id] [int] NOT NULL,
	[usuario] [varchar](30) NULL,
	[nome] [varchar](max) NULL,
	[telefone] [varchar](30) NULL,
	[email] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Fazenda]  WITH CHECK ADD  CONSTRAINT [FK_Fazenda_Proprietario] FOREIGN KEY([proprietarioID])
REFERENCES [dbo].[Proprietario] ([id])
GO
ALTER TABLE [dbo].[Fazenda] CHECK CONSTRAINT [FK_Fazenda_Proprietario]
GO
ALTER TABLE [dbo].[Proprietario]  WITH CHECK ADD  CONSTRAINT [FK_Proprietario_Conta] FOREIGN KEY([usuario])
REFERENCES [dbo].[Conta] ([usuario])
GO
ALTER TABLE [dbo].[Proprietario] CHECK CONSTRAINT [FK_Proprietario_Conta]
GO
ALTER TABLE [dbo].[Sensor]  WITH CHECK ADD  CONSTRAINT [FK_Sensor_Fazenda] FOREIGN KEY([fazendaID])
REFERENCES [dbo].[Fazenda] ([id])
GO
ALTER TABLE [dbo].[Sensor] CHECK CONSTRAINT [FK_Sensor_Fazenda]
GO
/****** Object:  StoredProcedure [dbo].[spConsulta]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spConsulta]
(
	@id INT ,
	@tabela VARCHAR(MAX)
)
AS BEGIN
	DECLARE @sql VARCHAR(MAX);
	SET @sql = 'SELECT * FROM ' + @tabela +
	' WHERE ID = ' + CAST(@id AS VARCHAR(MAX))
	EXEC(@sql)
END
GO
/****** Object:  StoredProcedure [dbo].[spConsulta_Conta]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spConsulta_Conta]
(
	@usuario VARCHAR(30),
	@senha VARCHAR(30)
)
AS BEGIN
	IF(SELECT dbo.fnc_ConsultaConta(@usuario, @senha)) = 1
	BEGIN
		SELECT * FROM Conta WHERE usuario = @usuario
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spConsultaAvancada_Fazenda]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spConsultaAvancada_Fazenda]
(
	@fazendaNome VARCHAR(MAX),
	@tamanhoInicial INT,
	@tamanhoFinal INT,
	@numeroSensores INT
)
AS BEGIN
	DECLARE @numSensores INT = CASE @numeroSensores WHEN 0 THEN 999999 ELSE @numeroSensores END
	DECLARE @tamFinal INT = CASE @tamanhoFinal  WHEN 0 THEN 999999 ELSE @tamanhoFinal END

	SELECT *
	FROM Fazenda f
	WHERE f.fazendaNome LIKE '%' +@fazendaNome + '%' AND
		  f.tamanho BETWEEN @tamanhoInicial AND @tamFinal AND
		  f.numeroSensores BETWEEN 0 AND @numSensores
END
GO
/****** Object:  StoredProcedure [dbo].[spConsultaAvancada_Monitoramento]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spConsultaAvancada_Monitoramento]
(
	@nomeFazenda VARCHAR(MAX),
	@Id INT
)
AS BEGIN
	DECLARE @cod INT = CASE @Id WHEN 0  THEN 999999 ELSE @Id END

	SELECT v.*
	FROM vw_Monitoramento v
	WHERE v.id BETWEEN 0 AND @cod AND
	v.fazendaNome  LIKE '%' +@nomeFazenda + '%' 

END
GO
/****** Object:  StoredProcedure [dbo].[spConsultaAvancada_Sensor]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spConsultaAvancada_Sensor]
(
	@fazendaID INT,
	@localizacao VARCHAR(MAX)
)
AS BEGIN
	DECLARE @id INT = CASE @fazendaID WHEN 0 THEN 999999 ELSE @fazendaID END

	SELECT s.*
	FROM Sensor s
	WHERE s.fazendaID BETWEEN 1 AND @id AND
		  s.localizacao  LIKE '%' +@localizacao
END
GO
/****** Object:  StoredProcedure [dbo].[spCount_numeroSensores]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCount_numeroSensores]
(
	@fazendaID INT
)
AS BEGIN
	SELECT COUNT(*) 
	FROM Sensor
	WHERE fazendaID = @fazendaID
END
GO
/****** Object:  StoredProcedure [dbo].[spDelete]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spDelete]
(
	@id INT ,
	@tabela VARCHAR(MAX)
)
AS BEGIN
	DECLARE @sql VARCHAR(MAX);
	SET @sql = ' DELETE ' + @tabela +
		' WHERE ID = ' + CAST(@id AS VARCHAR(MAX))
	EXEC(@sql)
END
GO
/****** Object:  StoredProcedure [dbo].[spInsert_Conta]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsert_Conta]
(
	@usuario VARCHAR(30),
	@senha VARCHAR(30)
)
AS BEGIN
	INSERT INTO Conta (usuario, senha) 
			VALUES (@usuario, @senha)
END
GO
/****** Object:  StoredProcedure [dbo].[spInsert_Fazenda]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsert_Fazenda]
(
	@id INT,
	@fazendaNome VARCHAR(150), --Campo novo
	@proprietarioID INT,
	@tamanho INT,
	@endereco VARCHAR(150)
)
AS BEGIN
	INSERT INTO Fazenda (id, fazendaNome, proprietarioID, tamanho, numeroSensores, endereco)
				VALUES(@id, @fazendaNome, @proprietarioID, @tamanho, 0, @endereco) --Inicia com 0 sensores
END
GO
/****** Object:  StoredProcedure [dbo].[spInsert_Proprietario]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsert_Proprietario]
(
	@id INT,
	@usuario VARCHAR(30),
	@nome VARCHAR(MAX),
	@telefone VARCHAR(30), --Alterado para VARCHAR
	@email VARCHAR(100)
)
AS BEGIN

		INSERT INTO Proprietario (id, usuario, nome, telefone, email)
					VALUES (@id, @usuario, @nome, @telefone, @email)
END
GO
/****** Object:  StoredProcedure [dbo].[spInsert_Sensor]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsert_Sensor]
(
	@id INT,
	@fazendaID INT,
	@temperatura FLOAT,
	@umidade FLOAT,
	@localizacao VARCHAR(MAX),
	@dataAtualizacao VARCHAR (50), --Campo novo
	@condicao VARCHAR(1)
)
AS BEGIN	
	INSERT INTO Sensor (id, fazendaID, temperatura, umidade, localizacao, dataAtualizacao, condicao)
				VALUES (@id, @fazendaID, @temperatura, @umidade, @localizacao, @dataAtualizacao, @condicao)

	UPDATE Fazenda
	SET numeroSensores = numeroSensores + 1
	WHERE id = @fazendaID
END
GO
/****** Object:  StoredProcedure [dbo].[spListagem]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spListagem]
(
	@tabela VARCHAR(MAX),
	@ordem VARCHAR(MAX))
AS BEGIN
	EXEC('SELECT * FROM ' + @tabela +
	' ORDER BY ' + @ordem)
END
GO
/****** Object:  StoredProcedure [dbo].[spMonitoramento]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spMonitoramento] as
BEGIN
	select * from vw_Monitoramento
END
GO
/****** Object:  StoredProcedure [dbo].[spProximoId]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spProximoId]
	(@tabela VARCHAR(MAX))
AS BEGIN
	EXEC('SELECT ISNULL(MAX(ID) +1, 1) AS MAIOR FROM '
		+@tabela)
END
GO
/****** Object:  StoredProcedure [dbo].[spUpdate_Conta]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUpdate_Conta]
(
	@usuario VARCHAR(30),
	@senha VARCHAR(30)
)
AS BEGIN
	UPDATE Conta 
	SET senha = @senha
	WHERE usuario = @usuario
END
GO
/****** Object:  StoredProcedure [dbo].[spUpdate_Fazenda]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUpdate_Fazenda]
(
	@fazendaNome VARCHAR(150),
	@proprietarioID INT,
	@tamanho INT,
	@endereco VARCHAR(150),
	@id INT

)
AS BEGIN
	UPDATE Fazenda 
	SET fazendaNome = @fazendaNome,
		proprietarioID = @proprietarioID,
		tamanho = @tamanho,
		endereco = @endereco
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[spUpdate_Proprietario]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUpdate_Proprietario]
(
	@usuario VARCHAR(30),
	@nome VARCHAR(MAX),
	@telefone VARCHAR(30),
	@email VARCHAR(100),
	@id INT
)
AS BEGIN
	UPDATE Proprietario
	SET nome = @nome,
		usuario = @usuario,
		telefone = @telefone,
		email = @email
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[spUpdate_Sensor]    Script Date: 20-Nov-21 04:00:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUpdate_Sensor]
(
	@id INT,
	@fazendaID INT,
	@temperatura FLOAT,
	@umidade FLOAT,
	@localizacao VARCHAR(MAX),
	@dataAtualizacao VARCHAR (50), --Campo novo
	@condicao VARCHAR(1)
)

AS BEGIN
	UPDATE Sensor
	SET fazendaID = @fazendaID,
		temperatura = @temperatura,
		umidade = @umidade,
		localizacao = @localizacao,
		dataAtualizacao = @dataAtualizacao,
		condicao = @condicao
	WHERE id = @id
END
GO
