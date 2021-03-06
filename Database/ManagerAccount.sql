USE [master]
GO
/****** Object:  Database [ManagerAccount]    Script Date: 6/17/2020 5:01:39 PM ******/
CREATE DATABASE [ManagerAccount]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ManagerAccount', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ManagerAccount.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ManagerAccount_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ManagerAccount_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ManagerAccount] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ManagerAccount].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ManagerAccount] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ManagerAccount] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ManagerAccount] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ManagerAccount] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ManagerAccount] SET ARITHABORT OFF 
GO
ALTER DATABASE [ManagerAccount] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ManagerAccount] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [ManagerAccount] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ManagerAccount] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ManagerAccount] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ManagerAccount] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ManagerAccount] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ManagerAccount] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ManagerAccount] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ManagerAccount] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ManagerAccount] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ManagerAccount] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ManagerAccount] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ManagerAccount] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ManagerAccount] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ManagerAccount] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ManagerAccount] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ManagerAccount] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ManagerAccount] SET RECOVERY FULL 
GO
ALTER DATABASE [ManagerAccount] SET  MULTI_USER 
GO
ALTER DATABASE [ManagerAccount] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ManagerAccount] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ManagerAccount] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ManagerAccount] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ManagerAccount', N'ON'
GO
USE [ManagerAccount]
GO
/****** Object:  StoredProcedure [dbo].[spSearchAccount]    Script Date: 6/17/2020 5:01:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		minhnd2
-- Create date: 16/06/2020
-- Description:	Tìm kiếm thông tin Account
-- =============================================
CREATE PROCEDURE [dbo].[spSearchAccount] 
	@Name nvarchar(150),
	@Mail nvarchar(150),
	@Phone nvarchar(150),
	@pageIndex INT,
	@pageSize INT,
	@totalRow INT  OUTPUT
AS
BEGIN
	DECLARE @offset INT

    IF(@pageIndex=0)
    begin
       SET @offset = @pageIndex;
    end
    ELSE 
    begin
        SET @offset = ((@pageIndex - 1) * @pageSize) + 1;
    end
    -- SET NOCOUNT ON added to prevent extra result sets from
    SET NOCOUNT ON;

	WITH OrderedSet AS
    (
      SELECT *,
          ROW_NUMBER() OVER (ORDER BY ID DESC) AS 'Index'
      FROM Accounts 
	  WHERE Name LIKE '%'+@Name+'%' AND Mail LIKE '%'+@Mail+'%' AND Phone LIKE '%'+@Phone+'%' AND IsDelete = 0 
    )
   SELECT * 
   FROM OrderedSet 
   WHERE [Index] BETWEEN @offset AND (@offset + @pageSize) - 1

   SET @totalRow = (SELECT COUNT(*) FROM Accounts WHERE Name LIKE '%'+@Name+'%' AND Mail LIKE '%'+@Mail+'%' AND Phone LIKE '%'+@Phone+'%'AND IsDelete = 0 ) 
END

GO
/****** Object:  StoredProcedure [dbo].[spSearchAccount2]    Script Date: 6/17/2020 5:01:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		minhnd2
-- Create date: 16/06/2020
-- Description:	Tìm kiếm thông tin Account
-- =============================================
CREATE PROCEDURE [dbo].[spSearchAccount2]
	-- Add the parameters for the stored procedure here
	@Name nvarchar(150),
	@Mail nvarchar(150),
	@Phone nvarchar(150),
	@pageIndex INT,
	@pageSize INT,
	@totalRow INT  OUTPUT
AS
BEGIN
	DECLARE @offset INT

	IF(@pageIndex=0)
    begin
       SET @offset = @pageIndex;
    end
    ELSE 
    begin
        SET @offset = ((@pageIndex - 1) * @pageSize);
    end
    -- SET NOCOUNT ON added to prevent extra result sets from
    SET NOCOUNT ON;

	SELECT * FROM Accounts 
	WHERE Name LIKE '%'+@Name+'%' AND Mail LIKE '%'+@Mail+'%' AND Phone LIKE '%'+@Phone+'%' AND IsDelete = 0 
	ORDER BY ID DESC OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY;

	SET @totalRow = (SELECT COUNT(*) FROM Accounts WHERE Name LIKE '%'+@Name+'%' AND Mail LIKE '%'+@Mail+'%' AND Phone LIKE '%'+@Phone+'%' AND IsDelete = 0 )
END

GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 6/17/2020 5:01:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[Mail] [nvarchar](150) NULL,
	[Phone] [nvarchar](150) NULL,
	[PassWord] [nvarchar](150) NULL,
	[CreateDate] [datetime] NULL,
	[Creator] [nvarchar](150) NULL,
	[EditDate] [datetime] NULL,
	[Editor] [nvarchar](150) NULL,
	[DeleteDate] [datetime] NULL,
	[Deletor] [nvarchar](150) NULL,
	[IsStatus] [bit] NULL,
	[IsActive] [bit] NULL,
	[IsDelete] [bit] NULL,
	[SecretKey] [nvarchar](150) NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([ID], [Name], [Mail], [Phone], [PassWord], [CreateDate], [Creator], [EditDate], [Editor], [DeleteDate], [Deletor], [IsStatus], [IsActive], [IsDelete], [SecretKey]) VALUES (1, N'Admin', N'admin@bagroup.vn', N'0913 574 686', N'LEOt69a35nY=', CAST(0x0000ABDD00BB011F AS DateTime), N'Admin', CAST(0x0000ABDD00FE29CD AS DateTime), N'Admin', NULL, NULL, 1, 1, 0, N'4d8afb68')
INSERT [dbo].[Accounts] ([ID], [Name], [Mail], [Phone], [PassWord], [CreateDate], [Creator], [EditDate], [Editor], [DeleteDate], [Deletor], [IsStatus], [IsActive], [IsDelete], [SecretKey]) VALUES (30, N'abc', N'abc@gmail.com', N'0123 548 788', N'AyiyC+57D5o=', CAST(0x0000ABDD01073491 AS DateTime), N'Admin', CAST(0x0000ABDD0107742D AS DateTime), N'Admin', NULL, NULL, 1, 1, 0, N'63cececf')
SET IDENTITY_INSERT [dbo].[Accounts] OFF
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [DF_Accounts_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [DF_Accounts_EditDate]  DEFAULT (getdate()) FOR [EditDate]
GO
USE [master]
GO
ALTER DATABASE [ManagerAccount] SET  READ_WRITE 
GO
