USE [master]
GO
/****** Object:  Database [WebAPICoreDB]    Script Date: 2017/11/19 21:41:23 ******/
CREATE DATABASE [WebAPICoreDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WebAPICoreDB', FILENAME = N'G:\Database\WebAPICoreDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'WebAPICoreDB_log', FILENAME = N'G:\Database\WebAPICoreDB_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [WebAPICoreDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WebAPICoreDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WebAPICoreDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WebAPICoreDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WebAPICoreDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WebAPICoreDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WebAPICoreDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [WebAPICoreDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WebAPICoreDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WebAPICoreDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WebAPICoreDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WebAPICoreDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WebAPICoreDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WebAPICoreDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WebAPICoreDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WebAPICoreDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WebAPICoreDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WebAPICoreDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WebAPICoreDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WebAPICoreDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WebAPICoreDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WebAPICoreDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WebAPICoreDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WebAPICoreDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WebAPICoreDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WebAPICoreDB] SET  MULTI_USER 
GO
ALTER DATABASE [WebAPICoreDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WebAPICoreDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WebAPICoreDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WebAPICoreDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [WebAPICoreDB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [WebAPICoreDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2017/11/19 21:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AutoHistory]    Script Date: 2017/11/19 21:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AutoHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Changed] [nvarchar](2048) NULL,
	[Created] [datetime2](7) NOT NULL,
	[Kind] [int] NOT NULL,
	[RowId] [nvarchar](50) NOT NULL,
	[TableName] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_AutoHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LogSet]    Script Date: 2017/11/19 21:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogSet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Application] [nvarchar](50) NOT NULL,
	[Logged] [datetime] NOT NULL,
	[Level] [nvarchar](50) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Logger] [nvarchar](250) NULL,
	[Callsite] [nvarchar](max) NULL,
	[Exception] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_dbo.Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MenuCategorySet]    Script Date: 2017/11/19 21:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuCategorySet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Importance] [int] NOT NULL,
	[IsSys] [bit] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_MenuCategorySet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MenuSet]    Script Date: 2017/11/19 21:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuSet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Action] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL,
	[Area] [nvarchar](max) NULL,
	[CategoryId] [int] NOT NULL,
	[Controller] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Iconfont] [nvarchar](max) NULL,
	[Importance] [int] NOT NULL,
	[IsExpand] [bit] NOT NULL,
	[LayoutLevel] [int] NULL,
	[MenuType] [smallint] NOT NULL,
	[ParentId] [int] NULL,
	[Title] [nvarchar](50) NOT NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[Url] [nvarchar](max) NULL,
 CONSTRAINT [PK_MenuSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoleMenuSet]    Script Date: 2017/11/19 21:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleMenuSet](
	[RoleId] [int] NOT NULL,
	[MenuId] [int] NOT NULL,
 CONSTRAINT [PK_RoleMenuSet] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoleSet]    Script Date: 2017/11/19 21:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleSet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsSys] [bit] NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_RoleSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRoleSet]    Script Date: 2017/11/19 21:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoleSet](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRoleSet] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserSet]    Script Date: 2017/11/19 21:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSet](
	[Id] [uniqueidentifier] NOT NULL,
	[Birthday] [datetime2](7) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Gender] [smallint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[LastActivityDate] [datetime2](7) NULL,
	[Mobile] [nvarchar](50) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhotoUrl] [nvarchar](150) NULL,
	[QQ] [nvarchar](50) NULL,
	[RealName] [nvarchar](50) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[UserName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171028094415_sigcms', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171029091259_sigcms001', N'2.0.0-rtm-26452')
SET IDENTITY_INSERT [dbo].[LogSet] ON 

INSERT [dbo].[LogSet] ([Id], [Application], [Logged], [Level], [Message], [Logger], [Callsite], [Exception], [CreatedBy]) VALUES (58, N'AspNetCoreNlog', CAST(N'2017-11-12 21:42:10.467' AS DateTime), N'Info', N'Inside my handler', N'SIG.SIGCMS.BadgeEntryHandler', N'SIG.SIGCMS.BadgeEntryHandler.HandleRequirementAsync(G:\OpenSourceProjects\SIGWebAPIBoilerplate\SIG.SIGCMS\SIGAuthorization.cs:25)', N'', N'')
INSERT [dbo].[LogSet] ([Id], [Application], [Logged], [Level], [Message], [Logger], [Callsite], [Exception], [CreatedBy]) VALUES (59, N'AspNetCoreNlog', CAST(N'2017-11-12 21:42:10.850' AS DateTime), N'Info', N'nlog is working from a controller', N'SIG.SIGCMS.Controllers.HomeController', N'SIG.SIGCMS.Controllers.HomeController.Index(G:\OpenSourceProjects\SIGWebAPIBoilerplate\SIG.SIGCMS\Controllers\HomeController.cs:24)', N'', N'')
INSERT [dbo].[LogSet] ([Id], [Application], [Logged], [Level], [Message], [Logger], [Callsite], [Exception], [CreatedBy]) VALUES (60, N'AspNetCoreNlog', CAST(N'2017-11-16 02:44:44.953' AS DateTime), N'Info', N'nlog is working from a controller', N'SIG.SIGCMS.Controllers.HomeController', N'SIG.SIGCMS.Controllers.HomeController.Index(G:\OpenSourceProjects\SIGWebAPIBoilerplate\SIG.SIGCMS\Controllers\HomeController.cs:24)', N'', N'')
INSERT [dbo].[LogSet] ([Id], [Application], [Logged], [Level], [Message], [Logger], [Callsite], [Exception], [CreatedBy]) VALUES (61, N'AspNetCoreNlog', CAST(N'2017-11-16 02:48:06.067' AS DateTime), N'Info', N'nlog is working from a controller', N'SIG.SIGCMS.Controllers.HomeController', N'SIG.SIGCMS.Controllers.HomeController.Index', N'', N'')
INSERT [dbo].[LogSet] ([Id], [Application], [Logged], [Level], [Message], [Logger], [Callsite], [Exception], [CreatedBy]) VALUES (62, N'AspNetCoreNlog', CAST(N'2017-11-16 03:05:09.757' AS DateTime), N'Info', N'nlog is working from a controller', N'SIG.SIGCMS.Controllers.HomeController', N'SIG.SIGCMS.Controllers.HomeController.Index(G:\OpenSourceProjects\SIGWebAPIBoilerplate\SIG.SIGCMS\Controllers\HomeController.cs:24)', N'', N'')
INSERT [dbo].[LogSet] ([Id], [Application], [Logged], [Level], [Message], [Logger], [Callsite], [Exception], [CreatedBy]) VALUES (63, N'AspNetCoreNlog', CAST(N'2017-11-16 04:06:52.677' AS DateTime), N'Info', N'nlog is working from a controller', N'SIG.SIGCMS.Controllers.HomeController', N'SIG.SIGCMS.Controllers.HomeController.Index(G:\OpenSourceProjects\SIGWebAPIBoilerplate\SIG.SIGCMS\Controllers\HomeController.cs:24)', N'', N'')
INSERT [dbo].[LogSet] ([Id], [Application], [Logged], [Level], [Message], [Logger], [Callsite], [Exception], [CreatedBy]) VALUES (64, N'AspNetCoreNlog', CAST(N'2017-11-16 04:07:04.343' AS DateTime), N'Info', N'nlog is working from a controller', N'SIG.SIGCMS.Controllers.HomeController', N'SIG.SIGCMS.Controllers.HomeController.Index(G:\OpenSourceProjects\SIGWebAPIBoilerplate\SIG.SIGCMS\Controllers\HomeController.cs:24)', N'', N'')
INSERT [dbo].[LogSet] ([Id], [Application], [Logged], [Level], [Message], [Logger], [Callsite], [Exception], [CreatedBy]) VALUES (65, N'AspNetCoreNlog', CAST(N'2017-11-16 04:10:35.657' AS DateTime), N'Info', N'nlog is working from a controller', N'SIG.SIGCMS.Controllers.HomeController', N'SIG.SIGCMS.Controllers.HomeController.Index(G:\OpenSourceProjects\SIGWebAPIBoilerplate\SIG.SIGCMS\Controllers\HomeController.cs:24)', N'', N'')
INSERT [dbo].[LogSet] ([Id], [Application], [Logged], [Level], [Message], [Logger], [Callsite], [Exception], [CreatedBy]) VALUES (66, N'AspNetCoreNlog', CAST(N'2017-11-16 04:13:14.453' AS DateTime), N'Info', N'nlog is working from a controller', N'SIG.SIGCMS.Controllers.HomeController', N'SIG.SIGCMS.Controllers.HomeController.Index(G:\OpenSourceProjects\SIGWebAPIBoilerplate\SIG.SIGCMS\Controllers\HomeController.cs:24)', N'', N'')
INSERT [dbo].[LogSet] ([Id], [Application], [Logged], [Level], [Message], [Logger], [Callsite], [Exception], [CreatedBy]) VALUES (67, N'AspNetCoreNlog', CAST(N'2017-11-16 04:49:46.537' AS DateTime), N'Info', N'nlog is working from a controller', N'SIG.SIGCMS.Controllers.HomeController', N'SIG.SIGCMS.Controllers.HomeController.Index(G:\OpenSourceProjects\SIGWebAPIBoilerplate\SIG.SIGCMS\Controllers\HomeController.cs:24)', N'', N'')
INSERT [dbo].[LogSet] ([Id], [Application], [Logged], [Level], [Message], [Logger], [Callsite], [Exception], [CreatedBy]) VALUES (68, N'AspNetCoreNlog', CAST(N'2017-11-16 11:31:15.143' AS DateTime), N'Info', N'nlog is working from a controller', N'SIG.SIGCMS.Controllers.HomeController', N'SIG.SIGCMS.Controllers.HomeController.Index(G:\OpenSourceProjects\SIGWebAPIBoilerplate\SIG.SIGCMS\Controllers\HomeController.cs:24)', N'', N'')
INSERT [dbo].[LogSet] ([Id], [Application], [Logged], [Level], [Message], [Logger], [Callsite], [Exception], [CreatedBy]) VALUES (69, N'AspNetCoreNlog', CAST(N'2017-11-16 11:31:41.857' AS DateTime), N'Info', N'nlog is working from a controller', N'SIG.SIGCMS.Controllers.HomeController', N'SIG.SIGCMS.Controllers.HomeController.Index(G:\OpenSourceProjects\SIGWebAPIBoilerplate\SIG.SIGCMS\Controllers\HomeController.cs:24)', N'', N'')
INSERT [dbo].[LogSet] ([Id], [Application], [Logged], [Level], [Message], [Logger], [Callsite], [Exception], [CreatedBy]) VALUES (70, N'AspNetCoreNlog', CAST(N'2017-11-16 12:47:20.877' AS DateTime), N'Info', N'nlog is working from a controller', N'SIG.SIGCMS.Controllers.HomeController', N'SIG.SIGCMS.Controllers.HomeController.Index(G:\OpenSourceProjects\SIGWebAPIBoilerplate\SIG.SIGCMS\Controllers\HomeController.cs:25)', N'', N'')
INSERT [dbo].[LogSet] ([Id], [Application], [Logged], [Level], [Message], [Logger], [Callsite], [Exception], [CreatedBy]) VALUES (71, N'AspNetCoreNlog', CAST(N'2017-11-16 12:47:25.383' AS DateTime), N'Info', N'nlog is working from a controller', N'SIG.SIGCMS.Controllers.HomeController', N'SIG.SIGCMS.Controllers.HomeController.Index(G:\OpenSourceProjects\SIGWebAPIBoilerplate\SIG.SIGCMS\Controllers\HomeController.cs:25)', N'', N'')
INSERT [dbo].[LogSet] ([Id], [Application], [Logged], [Level], [Message], [Logger], [Callsite], [Exception], [CreatedBy]) VALUES (72, N'AspNetCoreNlog', CAST(N'2017-11-16 12:47:29.437' AS DateTime), N'Info', N'nlog is working from a controller', N'SIG.SIGCMS.Controllers.HomeController', N'SIG.SIGCMS.Controllers.HomeController.Index(G:\OpenSourceProjects\SIGWebAPIBoilerplate\SIG.SIGCMS\Controllers\HomeController.cs:25)', N'', N'')
INSERT [dbo].[LogSet] ([Id], [Application], [Logged], [Level], [Message], [Logger], [Callsite], [Exception], [CreatedBy]) VALUES (73, N'AspNetCoreNlog', CAST(N'2017-11-16 12:47:30.140' AS DateTime), N'Info', N'nlog is working from a controller', N'SIG.SIGCMS.Controllers.HomeController', N'SIG.SIGCMS.Controllers.HomeController.Index(G:\OpenSourceProjects\SIGWebAPIBoilerplate\SIG.SIGCMS\Controllers\HomeController.cs:25)', N'', N'')
INSERT [dbo].[LogSet] ([Id], [Application], [Logged], [Level], [Message], [Logger], [Callsite], [Exception], [CreatedBy]) VALUES (74, N'AspNetCoreNlog', CAST(N'2017-11-16 12:47:46.483' AS DateTime), N'Info', N'nlog is working from a controller', N'SIG.SIGCMS.Controllers.HomeController', N'SIG.SIGCMS.Controllers.HomeController.Index(G:\OpenSourceProjects\SIGWebAPIBoilerplate\SIG.SIGCMS\Controllers\HomeController.cs:25)', N'', N'')
SET IDENTITY_INSERT [dbo].[LogSet] OFF
SET IDENTITY_INSERT [dbo].[MenuCategorySet] ON 

INSERT [dbo].[MenuCategorySet] ([Id], [Active], [CreatedBy], [CreatedDate], [Importance], [IsSys], [Title], [UpdatedBy], [UpdatedDate]) VALUES (1, 1, N'admin', CAST(N'2017-11-05 00:00:00.000' AS DateTime), 100, 1, N'后台菜单', NULL, NULL)
SET IDENTITY_INSERT [dbo].[MenuCategorySet] OFF
SET IDENTITY_INSERT [dbo].[MenuSet] ON 

INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (3, N'Index', 1, N'Admin', 1, N'Home', N'admin', CAST(N'2017-11-05 00:00:00.000' AS DateTime), N'fa fa-dashboard', 0, 1, 0, 1, NULL, N'控制面版', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), N'/admin')
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (4, NULL, 1, NULL, 1, NULL, N'admin', CAST(N'2017-11-06 03:21:10.660' AS DateTime), N'fa fa-cog', 10, 1, 1, 3, 3, N'系统', N'admin', CAST(N'2017-11-15 17:13:22.7692780' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (5, NULL, 1, NULL, 1, NULL, N'admin', CAST(N'2017-11-06 03:32:14.283' AS DateTime), NULL, 3, 0, 1, 3, 3, N'FQA', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (6, NULL, 1, NULL, 1, NULL, N'admin', CAST(N'2017-11-06 03:50:44.857' AS DateTime), NULL, 8, 0, 1, 3, 3, N'下载', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (7, NULL, 1, NULL, 1, NULL, N'admin', CAST(N'2017-11-06 04:06:24.273' AS DateTime), NULL, 9, 0, 1, 3, 3, N'图片', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (8, NULL, 1, NULL, 1, NULL, N'admin', CAST(N'2017-11-06 04:10:20.837' AS DateTime), NULL, 1, 0, 1, 3, 3, N'订单', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (9, NULL, 1, NULL, 1, NULL, N'admin', CAST(N'2017-11-06 04:15:34.983' AS DateTime), NULL, 2, 0, 1, 3, 3, N'视频', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (10, NULL, 1, NULL, 1, NULL, N'admin', CAST(N'2017-11-06 04:16:58.167' AS DateTime), N'fa fa-plug', 4, 0, 1, 3, 3, N'组件', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (11, NULL, 1, NULL, 1, NULL, N'admin', CAST(N'2017-11-06 05:11:42.453' AS DateTime), NULL, 5, 0, 1, 3, 3, N'产品', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (12, NULL, 1, NULL, 1, NULL, N'admin', CAST(N'2017-11-06 05:12:08.447' AS DateTime), N'fa fa-file-text', 6, 0, 1, 3, 3, N'文章', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (13, NULL, 1, NULL, 1, NULL, N'admin', CAST(N'2017-11-06 05:12:35.367' AS DateTime), N'fa fa-file', 7, 0, 1, 3, 3, N'页面', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (14, N'Index', 1, N'Admin', 1, N'Menu', N'admin', CAST(N'2017-11-06 05:37:16.487' AS DateTime), NULL, 23, 0, 2, 1, 4, N'后台菜单', N'admin', CAST(N'2017-11-15 16:09:34.4437191' AS DateTime2), N'/admin/menu/index')
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (15, N'Add', 0, N'Admin', 1, N'Menu', N'admin', CAST(N'2017-11-06 05:37:16.487' AS DateTime), NULL, 24, 0, 3, 2, 14, N'添加', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (16, N'Edit', 0, N'Admin', 1, N'Menu', N'admin', CAST(N'2017-11-06 05:37:16.487' AS DateTime), NULL, 25, 0, 3, 2, 14, N'编辑', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (17, N'IsActive', 0, N'Admin', 1, N'Menu', N'admin', CAST(N'2017-11-06 05:37:16.487' AS DateTime), NULL, 26, 0, 3, 2, 14, N'显示/隐藏', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (18, N'Delete', 0, N'Admin', 1, N'Menu', N'admin', CAST(N'2017-11-06 05:37:16.487' AS DateTime), NULL, 27, 0, 3, 2, 14, N'删除', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (19, N'PageSizeSet', 0, N'Admin', 1, N'Menu', N'admin', CAST(N'2017-11-06 05:37:16.487' AS DateTime), NULL, 28, 0, 3, 2, 14, N'分页设置', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (20, N'Index', 1, N'Admin', 1, N'Log', N'admin', CAST(N'2017-11-06 17:07:19.423' AS DateTime), NULL, 29, 0, 2, 1, 4, N'日志', N'admin', CAST(N'2017-11-15 16:09:35.6369714' AS DateTime2), N'/admin/log')
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (24, N'Delete', 0, N'Admin', 1, N'Log', N'admin', CAST(N'2017-11-06 17:07:19.423' AS DateTime), NULL, 30, 0, 3, 2, 20, N'删除', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (25, N'PageSizeSet', 0, N'Admin', 1, N'Log', N'admin', CAST(N'2017-11-06 17:07:19.423' AS DateTime), NULL, 31, 0, 3, 2, 20, N'分页设置', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (26, N'Index', 1, N'Admin', 1, N'Role', N'admin', CAST(N'2017-11-06 17:36:26.300' AS DateTime), NULL, 17, 1, 2, 1, 4, N'角色', N'admin', CAST(N'2017-11-15 17:13:30.6188669' AS DateTime2), N'/Admin/Role')
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (27, N'Add', 0, N'Admin', 1, N'Role', N'admin', CAST(N'2017-11-06 17:36:26.300' AS DateTime), NULL, 18, 0, 3, 2, 26, N'添加', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (28, N'Edit', 0, N'Admin', 1, N'Role', N'admin', CAST(N'2017-11-06 17:36:26.300' AS DateTime), NULL, 19, 0, 3, 2, 26, N'编辑', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (29, N'IsActive', 0, N'Admin', 1, N'Role', N'admin', CAST(N'2017-11-06 17:36:26.300' AS DateTime), NULL, 20, 0, 3, 2, 26, N'显示/隐藏', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (30, N'Delete', 0, N'Admin', 1, N'Role', N'admin', CAST(N'2017-11-06 17:36:26.300' AS DateTime), NULL, 21, 0, 3, 2, 26, N'删除', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (31, N'PageSizeSet', 0, N'Admin', 1, N'Role', N'admin', CAST(N'2017-11-06 17:36:26.300' AS DateTime), NULL, 22, 0, 3, 2, 26, N'分页设置', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (32, N'Index', 1, N'admin', 1, N'user', N'admin', CAST(N'2017-11-06 17:40:21.377' AS DateTime), NULL, 11, 1, 2, 1, 4, N'用户', N'admin', CAST(N'2017-11-15 20:58:40.5184617' AS DateTime2), N'/Admin/User')
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (33, N'Add', 0, N'Admin', 1, N'User', N'admin', CAST(N'2017-11-06 17:40:21.377' AS DateTime), NULL, 12, 0, 3, 2, 32, N'添加', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (34, N'Edit', 0, N'Admin', 1, N'User', N'admin', CAST(N'2017-11-06 17:40:21.377' AS DateTime), NULL, 13, 0, 3, 2, 32, N'编辑', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (35, N'IsActive', 0, N'Admin', 1, N'User', N'admin', CAST(N'2017-11-06 17:40:21.377' AS DateTime), NULL, 14, 0, 3, 2, 32, N'显示/隐藏', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (36, N'Delete', 0, N'Admin', 1, N'User', N'admin', CAST(N'2017-11-06 17:40:21.377' AS DateTime), NULL, 15, 0, 3, 2, 32, N'删除', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
INSERT [dbo].[MenuSet] ([Id], [Action], [Active], [Area], [CategoryId], [Controller], [CreatedBy], [CreatedDate], [Iconfont], [Importance], [IsExpand], [LayoutLevel], [MenuType], [ParentId], [Title], [UpdatedBy], [UpdatedDate], [Url]) VALUES (37, N'PageSizeSet', 0, N'Admin', 1, N'User', N'admin', CAST(N'2017-11-06 17:40:21.377' AS DateTime), NULL, 16, 0, 3, 2, 32, N'分页设置', N'admin', CAST(N'2017-11-15 16:08:48.5799956' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[MenuSet] OFF
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 3)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 3)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 4)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 4)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 5)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 5)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 6)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 6)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 7)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 7)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 8)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 8)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 9)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 9)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 10)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 10)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 11)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 11)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 12)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 12)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 13)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 13)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 14)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 14)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 15)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 15)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 16)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 16)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 17)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 17)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 18)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 18)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 19)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 19)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 20)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 20)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 24)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 24)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 25)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 25)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 26)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 26)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 27)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 27)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 28)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 28)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 29)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 29)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 30)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 30)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 31)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 31)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 32)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 32)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 33)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 33)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 34)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 34)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 35)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 35)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 36)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 36)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (1, 37)
INSERT [dbo].[RoleMenuSet] ([RoleId], [MenuId]) VALUES (5, 37)
SET IDENTITY_INSERT [dbo].[RoleSet] ON 

INSERT [dbo].[RoleSet] ([Id], [Description], [IsSys], [RoleName]) VALUES (1, N'', 1, N'系统管理员')
INSERT [dbo].[RoleSet] ([Id], [Description], [IsSys], [RoleName]) VALUES (2, N'', 1, N'用户')
INSERT [dbo].[RoleSet] ([Id], [Description], [IsSys], [RoleName]) VALUES (3, NULL, 0, N'商家')
INSERT [dbo].[RoleSet] ([Id], [Description], [IsSys], [RoleName]) VALUES (5, NULL, 0, N'考核员')
INSERT [dbo].[RoleSet] ([Id], [Description], [IsSys], [RoleName]) VALUES (6, N'商家会员', 0, N'商家')
SET IDENTITY_INSERT [dbo].[RoleSet] OFF
INSERT [dbo].[UserRoleSet] ([UserId], [RoleId]) VALUES (N'f7c80667-73a3-c857-94a6-08d5221b963e', 1)
INSERT [dbo].[UserRoleSet] ([UserId], [RoleId]) VALUES (N'13d818fd-687c-c44c-8413-08d522565bcc', 1)
INSERT [dbo].[UserRoleSet] ([UserId], [RoleId]) VALUES (N'469ffcd7-60ba-c401-cdbd-08d529b53c23', 1)
INSERT [dbo].[UserRoleSet] ([UserId], [RoleId]) VALUES (N'f7c80667-73a3-c857-94a6-08d5221b963e', 2)
INSERT [dbo].[UserRoleSet] ([UserId], [RoleId]) VALUES (N'13d818fd-687c-c44c-8413-08d522565bcc', 2)
INSERT [dbo].[UserRoleSet] ([UserId], [RoleId]) VALUES (N'1c25fd90-6042-c3b4-b049-08d529b2f0a1', 2)
INSERT [dbo].[UserRoleSet] ([UserId], [RoleId]) VALUES (N'f7c80667-73a3-c857-94a6-08d5221b963e', 5)
INSERT [dbo].[UserRoleSet] ([UserId], [RoleId]) VALUES (N'13d818fd-687c-c44c-8413-08d522565bcc', 5)
INSERT [dbo].[UserRoleSet] ([UserId], [RoleId]) VALUES (N'f7c80667-73a3-c857-94a6-08d5221b963e', 6)
INSERT [dbo].[UserRoleSet] ([UserId], [RoleId]) VALUES (N'13d818fd-687c-c44c-8413-08d522565bcc', 6)
INSERT [dbo].[UserSet] ([Id], [Birthday], [CreateDate], [Email], [Gender], [IsActive], [LastActivityDate], [Mobile], [PasswordHash], [PhotoUrl], [QQ], [RealName], [SecurityStamp], [UserName]) VALUES (N'f7c80667-73a3-c857-94a6-08d5221b963e', NULL, CAST(N'2017-11-03 02:00:22.2946489' AS DateTime2), N'twotong@gmail.com', 1, 1, NULL, N'dfs', N'lNh3Mr/RO3LvoLMECqHLE1ZqIE/taIgMtMjrbpDVcq4=', NULL, NULL, N'童柱港', N'/gT4bi9WsTq2ufS9VGZ4sFqPLAv7q9duwKQqntLUOBE=', N'doubetong')
INSERT [dbo].[UserSet] ([Id], [Birthday], [CreateDate], [Email], [Gender], [IsActive], [LastActivityDate], [Mobile], [PasswordHash], [PhotoUrl], [QQ], [RealName], [SecurityStamp], [UserName]) VALUES (N'13d818fd-687c-c44c-8413-08d522565bcc', NULL, CAST(N'2017-11-03 09:01:04.5463077' AS DateTime2), N'13212847@qq.com', 0, 1, NULL, NULL, N'WXijN1fTUYUNNYfbVKuspLTg2ShkiqpBX27bJXNBcBw=', NULL, NULL, NULL, N'aHYq+8ksMCopQTD0smaxnV+ttMQo0blLbRVNiWsWVos=', N'admin')
INSERT [dbo].[UserSet] ([Id], [Birthday], [CreateDate], [Email], [Gender], [IsActive], [LastActivityDate], [Mobile], [PasswordHash], [PhotoUrl], [QQ], [RealName], [SecurityStamp], [UserName]) VALUES (N'1c25fd90-6042-c3b4-b049-08d529b2f0a1', NULL, CAST(N'2017-11-12 17:51:26.0840774' AS DateTime2), N'11@qq.com', 1, 1, NULL, N'15361828193', N's+00PJwx6Q5cRr79sw6XSMucsfopc8/FBAXY+elgxo4=', NULL, NULL, N'twot', N'qqxo/B7H8F4jX2CJREtaffzgIYcuSVbT1ZDxt3sw8VU=', N'xiao')
INSERT [dbo].[UserSet] ([Id], [Birthday], [CreateDate], [Email], [Gender], [IsActive], [LastActivityDate], [Mobile], [PasswordHash], [PhotoUrl], [QQ], [RealName], [SecurityStamp], [UserName]) VALUES (N'469ffcd7-60ba-c401-cdbd-08d529b53c23', NULL, CAST(N'2017-11-12 18:07:51.7594668' AS DateTime2), N'ytjang@qq.com', 0, 1, NULL, NULL, N'lEbJUIUGv6CG3fnPeTd3XNpP9pinKT0mQBZk12/f0Ak=', NULL, NULL, N'dfs', N'bYbgYzMcswXr0u2gnfKpY2QdyEYQgsd/wTFPGi+o/x8=', N'ytjang')
/****** Object:  Index [IX_MenuSet_CategoryId]    Script Date: 2017/11/19 21:41:24 ******/
CREATE NONCLUSTERED INDEX [IX_MenuSet_CategoryId] ON [dbo].[MenuSet]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MenuSet_ParentId]    Script Date: 2017/11/19 21:41:24 ******/
CREATE NONCLUSTERED INDEX [IX_MenuSet_ParentId] ON [dbo].[MenuSet]
(
	[ParentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoleMenuSet_MenuId]    Script Date: 2017/11/19 21:41:24 ******/
CREATE NONCLUSTERED INDEX [IX_RoleMenuSet_MenuId] ON [dbo].[RoleMenuSet]
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRoleSet_RoleId]    Script Date: 2017/11/19 21:41:24 ******/
CREATE NONCLUSTERED INDEX [IX_UserRoleSet_RoleId] ON [dbo].[UserRoleSet]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MenuSet]  WITH CHECK ADD  CONSTRAINT [FK_MenuSet_MenuCategorySet_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[MenuCategorySet] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MenuSet] CHECK CONSTRAINT [FK_MenuSet_MenuCategorySet_CategoryId]
GO
ALTER TABLE [dbo].[MenuSet]  WITH CHECK ADD  CONSTRAINT [FK_MenuSet_MenuSet_ParentId] FOREIGN KEY([ParentId])
REFERENCES [dbo].[MenuSet] ([Id])
GO
ALTER TABLE [dbo].[MenuSet] CHECK CONSTRAINT [FK_MenuSet_MenuSet_ParentId]
GO
ALTER TABLE [dbo].[RoleMenuSet]  WITH CHECK ADD  CONSTRAINT [FK_RoleMenuSet_MenuSet_MenuId] FOREIGN KEY([MenuId])
REFERENCES [dbo].[MenuSet] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleMenuSet] CHECK CONSTRAINT [FK_RoleMenuSet_MenuSet_MenuId]
GO
ALTER TABLE [dbo].[RoleMenuSet]  WITH CHECK ADD  CONSTRAINT [FK_RoleMenuSet_RoleSet_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[RoleSet] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleMenuSet] CHECK CONSTRAINT [FK_RoleMenuSet_RoleSet_RoleId]
GO
ALTER TABLE [dbo].[UserRoleSet]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleSet_RoleSet_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[RoleSet] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoleSet] CHECK CONSTRAINT [FK_UserRoleSet_RoleSet_RoleId]
GO
ALTER TABLE [dbo].[UserRoleSet]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleSet_UserSet_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserSet] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoleSet] CHECK CONSTRAINT [FK_UserRoleSet_UserSet_UserId]
GO
USE [master]
GO
ALTER DATABASE [WebAPICoreDB] SET  READ_WRITE 
GO
