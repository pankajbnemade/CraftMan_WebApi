USE [master]
GO
/****** Object:  Database [Craftman_old]    Script Date: 3/5/2025 6:05:08 PM ******/
CREATE DATABASE [Craftman_old]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Craftman', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Craftman.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Craftman_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Craftman_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Craftman_old] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Craftman_old].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Craftman_old] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Craftman_old] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Craftman_old] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Craftman_old] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Craftman_old] SET ARITHABORT OFF 
GO
ALTER DATABASE [Craftman_old] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Craftman_old] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Craftman_old] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Craftman_old] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Craftman_old] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Craftman_old] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Craftman_old] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Craftman_old] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Craftman_old] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Craftman_old] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Craftman_old] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Craftman_old] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Craftman_old] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Craftman_old] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Craftman_old] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Craftman_old] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Craftman_old] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Craftman_old] SET RECOVERY FULL 
GO
ALTER DATABASE [Craftman_old] SET  MULTI_USER 
GO
ALTER DATABASE [Craftman_old] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Craftman_old] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Craftman_old] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Craftman_old] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Craftman_old] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Craftman_old] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Craftman_old', N'ON'
GO
ALTER DATABASE [Craftman_old] SET QUERY_STORE = OFF
GO
USE [Craftman_old]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 3/5/2025 6:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ImagesDescription] [nvarchar](255) NULL,
	[ImageData] [varbinary](max) NULL,
	[TicketId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobProfilePicture]    Script Date: 3/5/2025 6:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobProfilePicture](
	[JobProfilePictureId] [int] NOT NULL,
	[TicketId] [nvarchar](255) NULL,
	[ImageName] [nvarchar](255) NULL,
	[ImagePath] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[JobProfilePictureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCompanyCountyRel]    Script Date: 3/5/2025 6:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCompanyCountyRel](
	[pCompId] [int] NULL,
	[CountyId] [int] NULL,
	[MunicipalityId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCompanyJobMaster]    Script Date: 3/5/2025 6:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCompanyJobMaster](
	[pCompJobId] [int] IDENTITY(1,1) NOT NULL,
	[pCompId] [int] NULL,
	[PJobDescription] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[pCompJobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCompanyMaster]    Script Date: 3/5/2025 6:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCompanyMaster](
	[Username] [varchar](255) NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[Active] [varchar](50) NOT NULL,
	[UserType] [int] NULL,
	[pCompId] [int] IDENTITY(1,1) NOT NULL,
	[LocationId] [int] NULL,
	[MobileNumber] [varchar](15) NULL,
	[ContactPerson] [varchar](255) NULL,
	[EmailId] [varchar](255) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[CompanyName] [varchar](255) NULL,
	[CompanyRegistrationNumber] [varchar](255) NULL,
	[CompanyPresentation] [varchar](255) NULL,
	[Logotype] [varchar](50) NULL,
	[CompetenceDescription] [varchar](255) NULL,
	[CompanyReferences] [varchar](255) NULL,
	[JobList] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[pCompId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCountyMaster]    Script Date: 3/5/2025 6:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCountyMaster](
	[CountyId] [int] IDENTITY(1,1) NOT NULL,
	[CountyName] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[CountyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCraftMaster]    Script Date: 3/5/2025 6:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCraftMaster](
	[CraftId] [int] IDENTITY(1,1) NOT NULL,
	[LocationId] [int] NOT NULL,
	[CompanyName] [varchar](255) NOT NULL,
	[RegistrationNumber] [varchar](100) NOT NULL,
	[CompanyPresentation] [varchar](100) NULL,
	[Logotype] [varchar](100) NULL,
	[CompetenceDescription] [varchar](100) NULL,
	[Refer] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[CraftId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblErrorLogs]    Script Date: 3/5/2025 6:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblErrorLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MethodName] [nvarchar](255) NULL,
	[ErrorMessage] [nvarchar](max) NULL,
	[StackTrace] [nvarchar](max) NULL,
	[LogDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblIssueTicketChat]    Script Date: 3/5/2025 6:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblIssueTicketChat](
	[ChatId] [int] IDENTITY(1,1) NOT NULL,
	[ChatDateTime] [datetime] NULL,
	[TicketId] [int] NOT NULL,
	[CompanyId] [int] NULL,
	[UserId] [int] NULL,
	[Message] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ChatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblIssueTicketImages]    Script Date: 3/5/2025 6:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblIssueTicketImages](
	[ImageId] [int] IDENTITY(1,1) NOT NULL,
	[TicketId] [nvarchar](255) NULL,
	[ImageName] [nvarchar](255) NULL,
	[ImagePath] [nvarchar](255) NULL,
 CONSTRAINT [PK__tblIssue__7516F70C377082A2] PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblIssueTicketMaster]    Script Date: 3/5/2025 6:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblIssueTicketMaster](
	[TicketId] [int] IDENTITY(1,1) NOT NULL,
	[ReportingPerson] [varchar](255) NOT NULL,
	[ReportingDescription] [varchar](max) NULL,
	[OperationId] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[ToCraftmanType] [varchar](100) NULL,
	[Address] [varchar](100) NULL,
	[City] [varchar](100) NULL,
	[Pincode] [int] NULL,
	[CountyId] [int] NULL,
	[MunicipalityId] [int] NULL,
	[ReviewComment] [varchar](max) NULL,
	[ReviewStarRating] [int] NULL,
	[CompanyComment] [varchar](max) NULL,
	[ClosingOTP] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblIssueTicketWorkImages]    Script Date: 3/5/2025 6:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblIssueTicketWorkImages](
	[ImageId] [int] IDENTITY(1,1) NOT NULL,
	[TicketId] [nvarchar](255) NULL,
	[ImageName] [nvarchar](255) NULL,
	[ImagePath] [nvarchar](255) NULL,
 CONSTRAINT [PK__tblIssueWork__7516F70C377082A2] PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblIssueTransaction]    Script Date: 3/5/2025 6:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblIssueTransaction](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[TicketId] [int] NOT NULL,
	[Craftid] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblJobTypeMaster]    Script Date: 3/5/2025 6:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblJobTypeMaster](
	[JobTypeId] [int] IDENTITY(1,1) NOT NULL,
	[JobType] [varchar](255) NOT NULL,
	[JobTypeDescription] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[JobTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblLocationMaster]    Script Date: 3/5/2025 6:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLocationMaster](
	[LocationId] [int] IDENTITY(1,1) NOT NULL,
	[Area] [varchar](255) NULL,
	[Address] [text] NULL,
	[Country] [varchar](100) NULL,
	[City] [varchar](100) NULL,
	[State] [varchar](100) NULL,
	[LocationDescription] [varchar](150) NULL,
	[PostalCode] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblMenuItemMaster]    Script Date: 3/5/2025 6:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblMenuItemMaster](
	[MenuItemId] [int] IDENTITY(1,1) NOT NULL,
	[MenuId] [int] NOT NULL,
	[MenuItemDescription] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MenuItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblMenuMaster]    Script Date: 3/5/2025 6:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblMenuMaster](
	[MenuId] [int] IDENTITY(1,1) NOT NULL,
	[MenuType] [varchar](255) NOT NULL,
	[jobtypeid] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblMunicipalityMaster]    Script Date: 3/5/2025 6:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblMunicipalityMaster](
	[MunicipalityId] [int] IDENTITY(1,1) NOT NULL,
	[MunicipalityName] [varchar](255) NULL,
	[CountyId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MunicipalityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblServiceMaster]    Script Date: 3/5/2025 6:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblServiceMaster](
	[ServiceId] [int] IDENTITY(1,1) NOT NULL,
	[ServiceName] [varchar](255) NULL,
	[ImageName] [nvarchar](255) NULL,
	[ImagePath] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUserMaster]    Script Date: 3/5/2025 6:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUserMaster](
	[Username] [varchar](255) NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[Active] [varchar](50) NOT NULL,
	[UserType] [int] NULL,
	[pkey_UId] [int] IDENTITY(1,1) NOT NULL,
	[LocationId] [int] NULL,
	[MobileNumber] [varchar](15) NULL,
	[ContactPerson] [varchar](255) NULL,
	[EmailId] [varchar](255) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[CompanyName] [varchar](255) NULL,
	[CompanyRegistrationNumber] [varchar](255) NULL,
	[CompanyPresentation] [varchar](255) NULL,
	[Logotype] [varchar](50) NULL,
	[CompetenceDescription] [varchar](255) NULL,
	[CompanyReferences] [varchar](255) NULL,
	[CountyId] [int] NULL,
	[MunicipalityId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[pkey_UId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/5/2025 6:05:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Role] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblErrorLogs] ADD  DEFAULT (getdate()) FOR [LogDate]
GO
ALTER TABLE [dbo].[tblIssueTicketChat] ADD  DEFAULT (getdate()) FOR [ChatDateTime]
GO
ALTER TABLE [dbo].[tblIssueTransaction]  WITH CHECK ADD FOREIGN KEY([Craftid])
REFERENCES [dbo].[tblCraftMaster] ([CraftId])
GO
ALTER TABLE [dbo].[tblIssueTransaction]  WITH CHECK ADD FOREIGN KEY([TicketId])
REFERENCES [dbo].[tblIssueTicketMaster] ([TicketId])
GO
ALTER TABLE [dbo].[tblMenuItemMaster]  WITH CHECK ADD FOREIGN KEY([MenuId])
REFERENCES [dbo].[tblMenuMaster] ([MenuId])
GO
ALTER TABLE [dbo].[tblMenuMaster]  WITH CHECK ADD FOREIGN KEY([jobtypeid])
REFERENCES [dbo].[tblJobTypeMaster] ([JobTypeId])
GO
USE [master]
GO
ALTER DATABASE [Craftman_old] SET  READ_WRITE 
GO
