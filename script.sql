﻿USE [master]
GO
/****** Object:  Database [ClassSRM]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
CREATE DATABASE [ClassSRM]
GO
ALTER DATABASE [ClassSRM] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ClassSRM].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ClassSRM] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ClassSRM] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ClassSRM] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ClassSRM] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ClassSRM] SET ARITHABORT OFF 
GO
ALTER DATABASE [ClassSRM] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ClassSRM] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ClassSRM] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ClassSRM] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ClassSRM] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ClassSRM] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ClassSRM] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ClassSRM] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ClassSRM] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ClassSRM] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ClassSRM] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ClassSRM] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ClassSRM] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ClassSRM] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ClassSRM] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ClassSRM] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ClassSRM] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ClassSRM] SET RECOVERY FULL 
GO
ALTER DATABASE [ClassSRM] SET  MULTI_USER 
GO
ALTER DATABASE [ClassSRM] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ClassSRM] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ClassSRM] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ClassSRM] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ClassSRM] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ClassSRM', N'ON'
GO
USE [ClassSRM]
GO
/****** Object:  Table [dbo].[Cars]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cars](
	[ID] [int] NOT NULL,
	[Trademark] [nvarchar](50) NULL,
	[Model] [nvarchar](50) NULL,
	[HP] [smallint] NULL,
	[Liter] [float] NULL,
	[Cyl] [tinyint] NULL,
	[TransmissSpeedCount] [tinyint] NULL,
	[TransmissAutomatic] [nvarchar](3) NULL,
	[MPG_City] [tinyint] NULL,
	[MPG_Highway] [tinyint] NULL,
	[Category] [nvarchar](7) NULL,
	[Description] [nvarchar](max) NULL,
	[Hyperlink] [nvarchar](50) NULL,
	[Picture] [image] NULL,
	[Price] [money] NULL,
	[RtfContent] [nvarchar](max) NULL,
	[Color] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CarScheduling]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarScheduling](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Status] [int] NULL,
	[Subject] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[Label] [int] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[Location] [nvarchar](50) NULL,
	[AllDay] [bit] NOT NULL,
	[EventType] [int] NULL,
	[RecurrenceInfo] [nvarchar](max) NULL,
	[ReminderInfo] [nvarchar](max) NULL,
	[CarIdShared] [nvarchar](max) NULL,
	[Price] [float] NULL,
	[CarId] [int] NULL,
 CONSTRAINT [PK_CarScheduling] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_ActPoint]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_ActPoint](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NULL,
	[Score] [int] NULL,
	[Date] [nvarchar](12) NULL,
	[Descr] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Check]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Check](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NULL,
	[Exist] [bit] NULL,
	[Date] [nvarchar](12) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_EvaPoint]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_EvaPoint](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NULL,
	[Score] [int] NULL,
	[Book] [nvarchar](50) NULL,
	[Date] [nvarchar](12) NULL,
	[Descr] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Gifts]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Gifts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NULL,
	[HasGift] [bit] NULL,
	[Date] [nvarchar](12) NULL,
	[Desc] [nchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Quastion]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Quastion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SchId] [int] NULL,
	[StudentId] [int] NULL,
	[Book] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_School]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_School](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SchName] [nvarchar](100) NULL,
	[SchAdmin] [nvarchar](100) NULL,
	[SchClass] [nvarchar](100) NULL,
	[SchDate] [nvarchar](12) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Student]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Student](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StuClassId] [int] NULL,
	[StuName] [nvarchar](100) NULL,
	[StuLName] [nvarchar](100) NULL,
	[StuFName] [nvarchar](100) NULL,
	[StuGender] [nvarchar](50) NULL,
	[StuImage] [image] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_TaskCheckList]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_TaskCheckList](
	[CheckListId] [int] IDENTITY(1,1) NOT NULL,
	[TaskID] [int] NULL,
	[Capt] [nvarchar](max) NULL,
	[Checked] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[CheckListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Tasks]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Tasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Caption] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NULL,
	[Label] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_User]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[Pass] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  View [dbo].[ActV]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ActV]
AS SELECT tbl_ActPoint.StudentId,tbl_ActPoint.Score,tbl_ActPoint.Date, tbl_Student.StuName, tbl_Student.StuLName,tbl_Student.StuFName,tbl_Student.StuImage,tbl_Student.StuClassId
  FROM [tbl_ActPoint] JOIN tbl_Student
    ON tbl_Student.Id = tbl_ActPoint.StudentId

GO
/****** Object:  View [dbo].[EvaV]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[EvaV]
AS SELECT tbl_EvaPoint.StudentId,tbl_EvaPoint.Score,tbl_EvaPoint.Date, tbl_Student.StuName, tbl_Student.StuLName,tbl_Student.StuFName,tbl_Student.StuImage,tbl_Student.StuClassId
  FROM [tbl_EvaPoint] JOIN tbl_Student
    ON tbl_Student.Id = tbl_EvaPoint.StudentId

GO
/****** Object:  View [dbo].[SumAE]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[SumAE]
AS
	SELECT StudentId,StuClassId, ISNULL(SUM(Score),0) As HighScoreUser
	,CONCAT(StuName, N' ',StuLName,N' (',StuFName,N')') AS StudentName
FROM (SELECT StudentId,StuClassId, Score,StuName,StuLName,StuFName FROM ActV
      UNION ALL
      SELECT StudentId,StuClassId, Score,StuName,StuLName,StuFName FROM EvaV
     ) as T 
GROUP BY  StudentId,StuClassId,StuName,StuLName,StuFName

GO
/****** Object:  View [dbo].[CheckV]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CheckV]
AS SELECT tbl_Check.Id,tbl_Check.StudentId,tbl_Student.StuName, tbl_Student.StuLName, tbl_Student.StuFName,tbl_Student.StuImage,tbl_Check.Exist,tbl_Check.Date
  FROM [tbl_Student] JOIN tbl_Check
    ON tbl_Student.Id = tbl_Check.StudentId

GO
/****** Object:  View [dbo].[SumEva]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[SumEva]
AS
	SELECT StudentId, ISNULL(SUM(Score),0) As HighScoreUser
	,Book,Date
FROM  tbl_EvaPoint
GROUP BY  StudentId,Book,Date

GO
/****** Object:  StoredProcedure [dbo].[AddQuastion]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddQuastion]
	@SchId int,
	@StuId int,
	@Book  nvarchar(50)
AS
	INSERT INTO tbl_Quastion	(SchId,StudentId,Book)
	VALUES(@SchId,@StuId,@Book)
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[DeleteActPoint]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteActPoint]
	@Id	INT
AS
	DELETE  tbl_ActPoint
	WHERE Id = @Id
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[DeleteAllQuastion]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAllQuastion]
	@SchId int,
	@Book	NVARCHAR(50)
	AS
	DELETE	tbl_Quastion
	WHERE	SchId = @SchId	AND Book = @Book
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[DeleteCheck]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteCheck]
	@Id	INT
	

AS
	DELETE  tbl_Check	
	WHERE Id = @Id
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[DeleteEvaBook]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteEvaBook]
	@Id	INT
AS
	DELETE  tbl_EvaPoint
	
	WHERE Id = @Id
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[DeleteSchool]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteSchool]
	@Id	  INT
	
AS
	DELETE tbl_Student
	WHERE StuClassId = @Id
	DELETE tbl_School 
	WHERE Id = @Id

RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[DeleteStudent]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteStudent]
	@Id		INT
AS
	DELETE tbl_ActPoint
	WHERE StudentId = @Id
	DELETE tbl_EvaPoint
	WHERE StudentId=@Id
	DELETE tbl_Check
	WHERE StudentId = @Id
	DELETE  tbl_Student
	WHERE Id = @id
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[DeleteTask]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteTask]
	@Id	INT
AS
	DELETE tbl_TaskCheckList
	WHERE  TaskID=@Id
	DELETE	tbl_Tasks	
	WHERE	Id=@Id
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteUser]
	@UserId		INT
AS
	DELETE tbl_User
	WHERE Id = @UserId
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[InsertActPoint]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertActPoint]
	@StudentId	INT,
	@Score	INT,
	@Date	NVARCHAR(12),
	@Desc	NVARCHAR(MAX)
AS
	INSERT INTO tbl_ActPoint (StudentId,Score,tbl_ActPoint.Date, tbl_ActPoint.Descr)
	VALUES(@StudentId,@Score,@Date,@Desc)
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[InsertCheck]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertCheck]
	@StudentId	INT,
	@Exist	BIT,
	@Date	NVARCHAR(12)

AS
	INSERT INTO tbl_Check	(StudentId,Exist,Date)
	VALUES(@StudentId,@Exist,@Date)
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[InsertEvaBook]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertEvaBook]
	@StudentId	INT,
	@Score	INT,
	@Book	NVARCHAR(50),
	@Date	NVARCHAR(12),
	@Desc	NVARCHAR(MAX)
AS
	INSERT INTO tbl_EvaPoint	(StudentId,Score,Book,Date,Descr)
	VALUES(@StudentId,@Score,@Book,@Date,@Desc)
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[InsertSchool]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertSchool]
	@Name NVARCHAR(100),
	@Admin NVARCHAR(100),
	@Class NVARCHAR(100),
	@Date NVARCHAR(10)
AS
	INSERT INTO tbl_School (SchName,SchAdmin,SchClass,SchDate)
		VALUES (@Name,@Admin,@Class,@Date)
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[InsertStudent]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertStudent]
	@SchoolId INT,
	@Name NVARCHAR(100),
	@LName NVARCHAR(100),
	@FName NVARCHAR(100),
	@Gender NVARCHAR(50),
	@Image IMAGE
AS
	INSERT INTO tbl_Student (StuClassId,StuName,StuLName,StuFName,StuGender,StuImage)
		VALUES (@SchoolId,@Name,@LName,@FName,@Gender,@Image)
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[InsertTask]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertTask]
	
	@Caption	NVARCHAR(MAX),
	@Description	NVARCHAR(MAX),
	@Status	NVARCHAR(MAX),
	@Label	INT
AS
	Insert INTO	tbl_Tasks	(Caption,Description,Status,Label)
	VALUES(@Caption,@Description,@Status,@Label)
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUser]
	@UserName	NVARCHAR(20),
	@HashPass	NVARCHAR(MAX)
AS
	INSERT INTO tbl_User	(Name,Pass)
	VALUES(@UserName,@HashPass)
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[Select2Dates]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Select2Dates]
	@Id	INT,
	@Date1	NVARCHAR(12),
	@Date2	NVARCHAR(12)
AS
SELECT Id, StudentId, StuName, StuFName, StuLName, Exist, Date
FROM	CheckV
WHERE
StudentId = @Id
AND
Date >= @Date1
AND
Date <= @Date2
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[SelectAskStatus]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectAskStatus]
	@ClassId		INT,
	@Book			NVARCHAR(50)
AS
BEGIN
	SELECT Id, StuName, StuLName, StuFName
FROM tbl_Student
WHERE NOT EXISTS
    (SELECT * 
     FROM tbl_Quastion
     WHERE StudentId = 
            @ClassId
        AND Book = @Book)
END

GO
/****** Object:  StoredProcedure [dbo].[SelectCurScore]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectCurScore]
	@Id		INT
AS
BEGIN
	IF NOT EXISTS (
	SELECT ISNULL(SUM([t1].[Score]),0) AS [ScoreSum]
	FROM (
    SELECT [t0].[Score], @Id AS [value], [t0].[StudentId]
    FROM [dbo].[tbl_ActPoint] AS [t0]
    ) AS [t1]
	WHERE [t1].[StudentId] = @Id
	GROUP BY [t1].[value])
	SELECT 0 as [ScoreSum]
	ELSE
	SELECT ISNULL(SUM([t1].[Score]),0) AS [ScoreSum]
	FROM (
    SELECT [t0].[Score], @Id AS [value], [t0].[StudentId]
    FROM [dbo].[tbl_ActPoint] AS [t0]
    ) AS [t1]
	WHERE [t1].[StudentId] = @Id
	GROUP BY [t1].[value]
END

GO
/****** Object:  StoredProcedure [dbo].[SelectExistCheck]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectExistCheck]
	@Id		INT
AS
BEGIN
	SELECT ISNULL(Count([t0].[Exist]),0) as [Count]
	FROM [dbo].[tbl_Check] AS [t0]
	WHERE ([t0].[StudentId] = @Id) AND ([t0].[Exist] = 1)
END

GO
/****** Object:  StoredProcedure [dbo].[SelectGifts]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectGifts]
	@SchoolId		INT,
	@DateMinCur		NVARCHAR(12),
	@DateMaxCur		NVARCHAR(12),
	@DateMinPrev	NVARCHAR(12),
	@DateMaxPrev	NVARCHAR(12)

AS
	WITH Prev AS
(
    SELECT StudentId, ISNULL(SUM(Score),0) As HighScoreUser
FROM (SELECT StudentId, Score FROM tbl_ActPoint
UNION ALL
      SELECT StudentId, Score FROM tbl_EvaPoint		WHERE Date>=@DateMinPrev AND Date <= @DateMaxPrev
	  ) as T 
	  GROUP BY  StudentId
),
Cur AS 
(
    SELECT StudentId, ISNULL(SUM(Score),0) As HighScoreUser
FROM (SELECT StudentId, Score FROM tbl_ActPoint
UNION ALL
      SELECT StudentId, Score FROM tbl_EvaPoint		WHERE Date>=@DateMinCur AND Date <= @DateMaxCur
	  ) as T 
	  GROUP BY  StudentId
) 
SELECT CASE 
		WHEN(Prev.HighScoreUser <= Cur.HighScoreUser)
		THEN N'مجاز برای دریافت'
		ELSE N'غیرمجاز، تعلق نمی گیرد'
		END as HaveGift,Prev.StudentId,std.StuName,std.StuLName,std.StuFName
		
FROM Prev
INNER JOIN Cur
ON Prev.StudentId = Cur.StudentId
INNER JOIN tbl_Student std
ON std.Id = Cur.StudentId
WHERE std.StuClassId=@SchoolId
ORDER BY HaveGift DESC
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[SelectNOTExistCheck]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectNOTExistCheck]
	@Id		INT
AS
BEGIN
	SELECT ISNULL(Count([t0].[Exist]),0) as [Count]
	FROM [dbo].[tbl_Check] AS [t0]
	WHERE ([t0].[StudentId] = @Id) AND ([t0].[Exist] = 0)
END

GO
/****** Object:  StoredProcedure [dbo].[SelectQActivity]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectQActivity]
	@Id		INT
AS
BEGIN
	IF NOT EXISTS(SELECT ISNULL(SUM([t0].[Score]),0) as Score
	FROM [dbo].[tbl_ActPoint] AS [t0]
	WHERE [t0].[StudentId] = @Id)
	SELECT	0 as Score
	ELSE
	SELECT ISNULL(SUM([t0].[Score]),0) as Score
	FROM [dbo].[tbl_ActPoint] AS [t0]
	WHERE [t0].[StudentId] = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[SelectQueryWin]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectQueryWin]
AS
BEGIN
	IF NOT EXISTS(
	SELECT TOP (1) [t1].[StudentId], [t1].[value2] AS [HighScore]
	FROM (
		SELECT ISNULL(SUM([t0].[Score]),0) AS [value], ISNULL(SUM([t0].[Score]),0) AS [value2], [t0].[StudentId]
		FROM [dbo].[tbl_ActPoint] AS [t0]
		GROUP BY [t0].[StudentId]
		) AS [t1]
	ORDER BY [t1].[value] DESC)
	SELECT	0	as [HighScore]
	ELSE
	SELECT TOP (1) [t1].[StudentId], [t1].[value2] AS [HighScore]
	FROM (
		SELECT ISNULL(SUM([t0].[Score]),0) AS [value], ISNULL(SUM([t0].[Score]),0) AS [value2], [t0].[StudentId]
		FROM [dbo].[tbl_ActPoint] AS [t0]
		GROUP BY [t0].[StudentId]
		) AS [t1]
	ORDER BY [t1].[value] DESC
	
END

GO
/****** Object:  StoredProcedure [dbo].[SelectSchool]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectSchool]
	
AS
	SELECT * FROM tbl_School
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[SelectStudentByClassId]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectStudentByClassId]
	@Id		INT
AS
BEGIN
	SELECT * From tbl_Student
	WHERE StuClassId = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[SelectStudentByClassIdNoIMG]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectStudentByClassIdNoIMG]
	@Id		INT
AS
	SELECT Id, StuName, StuLName, StuFName From tbl_Student
	WHERE StuClassId = @Id
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[SelectSumBook]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectSumBook]
	@Id		INT,
	@Book	NVARCHAR(50)
AS
BEGIN
	IF NOT EXISTS(SELECT [t0].[StudentId], [t0].[HighScoreUser], [t0].[Book], [t0].[Date]
	FROM [dbo].[SumEva] AS [t0]
	WHERE ([t0].[Book] = @Book) AND ([t0].[StudentId] = @Id))
	SELECT	0 as HighScoreUser
	ELSE
	SELECT [t0].[StudentId], [t0].[HighScoreUser], [t0].[Book], [t0].[Date]
	FROM [dbo].[SumEva] AS [t0]
	WHERE ([t0].[Book] = @Book) AND ([t0].[StudentId] = @Id)
END

GO
/****** Object:  StoredProcedure [dbo].[SelectSumEvaDate]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectSumEvaDate]
	@StudentId	INT,
	@Date1	NVARCHAR(12),
	@Date2	NVARCHAR(12)
AS
	SELECT * FROM SumEva	WHERE StudentId =@StudentId		AND Date >= @Date1	AND		Date <= @Date2
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[SelectSumEvaPoint]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectSumEvaPoint]
	@Id		INT
AS
BEGIN
	SELECT ISNULL(SUM([t0].[Score]),0) as SUMEVA
	FROM [dbo].[tbl_EvaPoint] AS [t0]
	WHERE [t0].[StudentId] = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[SelectTopMonth]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectTopMonth]
	@SchoolId	INT,
	@Date1	NVARCHAR(12),
	@Date2	NVARCHAR(12)
AS
	SELECT StudentId,StuName,StuLName,StuFName, ISNULL(SUM(Score),0) As HighScoreUser
FROM (SELECT StudentId, Score,StuName,StuLName,StuFName FROM ActV		WHERE Date >= @Date1	AND		Date <= @Date2		AND StuClassId = @SchoolId
      UNION ALL
      SELECT StudentId, Score,StuName,StuLName,StuFName FROM EvaV		WHERE Date >= @Date1	AND		Date <= @Date2		AND	StuClassId = @SchoolId
     ) as T 
GROUP BY  StudentId,StuName,StuLName,StuFName ORDER BY HighScoreUser DESC
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[SelectTopYear]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectTopYear]
	@SchoolId	INT
	AS
	WITH s as (
      SELECT TOP 5 StudentId, ISNULL(SUM(Score),0) As HighScoreUser
      FROM (SELECT StudentId, Score FROM ActV	WHERE StuClassId = @SchoolId
            UNION ALL
            SELECT StudentId, Score FROM EvaV	WHERE StuClassId = @SchoolId
           ) s
      GROUP BY  StudentId
      ORDER BY HighScoreUser DESC
     )
SELECT StudentId,HighScoreUser,StuName,StuLName,StuFName
FROM s JOIN
     tbl_Student ot
     ON s.StudentId = ot.Id;
--AS
--	WITH s as (
--      SELECT TOP 5 StudentId, ISNULL(SUM(Score),0) As HighScoreUser
--      FROM (SELECT StudentId, Score FROM tbl_ActPoint
--            UNION ALL
--            SELECT StudentId, Score FROM tbl_EvaPoint
--           ) s
--      GROUP BY  StudentId
--      ORDER BY HighScoreUser DESC
--     )
--SELECT StudentId,HighScoreUser,StuName,StuLName,StuImage
--FROM s JOIN
--     tbl_Student ot
--     ON s.StudentId = ot.Id;
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[UpdateActBook]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateActBook]
	@Id	INT,
	@StudentId	INT,
	@Score	INT,
	@Date	NVARCHAR(12),
	@Desc	NVARCHAR(MAX)
AS
	UPDATE  tbl_ActPoint
	SET
	StudentId=@StudentId,Score=@Score,Date=@Date,Descr=@Desc
	WHERE Id = @Id
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[UpdateCheck]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCheck]
	@Id	INT,
	@StudentId	INT,
	@Exist	BIT,
	@Date	NVARCHAR(12)

AS
	UPDATE  tbl_Check	
	SET
	StudentId=@StudentId,Exist=@Exist,Date=@Date
	WHERE Id = @Id
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[UpdateEvaBook]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateEvaBook]
	@Id	INT,
	@StudentId	INT,
	@Score	INT,
	@Book	NVARCHAR(50),
	@Date	NVARCHAR(12),
	@Desc	NVARCHAR(MAX)
AS
	UPDATE  tbl_EvaPoint
	SET
	StudentId=@StudentId,Score=@Score,Book=@Book,Date=@Date,Descr=@Desc
	WHERE Id = @Id
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[UpdateSchool]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateSchool]
	@Id	  INT,
	@Name NVARCHAR(100),
	@Admin NVARCHAR(100),
	@Class NVARCHAR(100),
	@Date NVARCHAR(10)
AS
	UPDATE tbl_School 
	SET
		SchName = @Name,
		SchAdmin = @Admin,
		SchClass = @Class,
		SchDate =@Date
		WHERE Id = @Id
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[UpdateStudent]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateStudent]
	@Id		INT,
	@SchoolId INT,
	@Name NVARCHAR(100),
	@LName NVARCHAR(100),
	@FName NVARCHAR(100),
	@Gender NVARCHAR(50),
	@Image IMAGE
AS
	UPDATE  tbl_Student
	SET
	StuClassId=@SchoolId,StuName=@Name,StuLName=@LName,StuFName=@FName,StuGender=@Gender,StuImage=@Image
	WHERE Id = @id
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[UpdateTask]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateTask]
	@Id	INT,
	@Caption	NVARCHAR(MAX),
	@Desc	NVARCHAR(MAX),
	@Status	NVARCHAR(MAX),
	@Label	INT
AS
	UPDATE	tbl_Tasks	
	SET
	Caption=@Caption,
	Description=@Desc,
	Status=@Status,
	Label=@Label
	WHERE	Id=@Id
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[UpdateTaskLabel]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateTaskLabel]
	@Id	INT,
	@Status	NVARCHAR(MAX),
	@Label  INT
AS
	UPDATE	tbl_Tasks	
	SET
	Status=@Status,
	Label = @Label
	WHERE	Id=@Id
RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 02/08/1396 08:33:15 ق.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateUser]
	@UserId		INT,
	@UserName	NVARCHAR(20),
	@HashPass	NVARCHAR(MAX)
AS
	UPDATE tbl_User
	SET
	Name=@UserName,
	Pass=@HashPass
	WHERE Id = @UserId
RETURN 0

GO
USE [master]
GO
ALTER DATABASE [ClassSRM] SET  READ_WRITE 
GO
