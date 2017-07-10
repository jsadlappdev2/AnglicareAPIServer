USE [APIDB]
GO
/****** Object:  Table [dbo].[TokensManager]    Script Date: 04/19/2017 23:07:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TokensManager](
	[TokenID] [int] IDENTITY(1,1) NOT NULL,
	[TokenKey] [varchar](100) NULL,
	[IssuedOn] [datetime] NULL,
	[ExpiresOn] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[CompanyID] [int] NULL,
 CONSTRAINT [PK_TokensManager] PRIMARY KEY CLUSTERED 
(
	[TokenID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[TokensManager] ON
INSERT [dbo].[TokensManager] ([TokenID], [TokenKey], [IssuedOn], [ExpiresOn], [CreatedOn], [CompanyID]) VALUES (6, N'9/+BQ1T65e/BXT10WKpls82KBPyl7UpkmeGjYuoQM2zoWWkJ6Ze7AJkQ71o5H7YTBb+yB4ZhBe9w/BAC8TUSrw==', CAST(0x0000A74400B7DABA AS DateTime), CAST(0x0000A74400C01828 AS DateTime), CAST(0x0000A74400B7DAC8 AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[TokensManager] OFF
/****** Object:  Table [dbo].[RegisterUser]    Script Date: 04/19/2017 23:07:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegisterUser](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[EmailID] [nvarchar](50) NULL,
	[CreateOn] [datetime] NULL,
 CONSTRAINT [PK_RegisterUser] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[RegisterUser] ON
INSERT [dbo].[RegisterUser] ([UserID], [Username], [Password], [EmailID], [CreateOn]) VALUES (1, N'Saineshwar', N'w/yAycDAcBe8qrDhU8b50Q==', N'demo@demo.com', CAST(0x0000A739018A7DA8 AS DateTime))
SET IDENTITY_INSERT [dbo].[RegisterUser] OFF
/****** Object:  Table [dbo].[RegisterCompany]    Script Date: 04/19/2017 23:07:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RegisterCompany](
	[CompanyID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[Description] [varchar](100) NULL,
	[PersonInCharge] [varchar](100) NULL,
	[CreateOn] [datetime] NULL,
	[EmailID] [varchar](100) NULL,
	[Status] [bit] NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_RegsiterCompany] PRIMARY KEY CLUSTERED 
(
	[CompanyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[RegisterCompany] ON
INSERT [dbo].[RegisterCompany] ([CompanyID], [Name], [Description], [PersonInCharge], [CreateOn], [EmailID], [Status], [UserID]) VALUES (1, N'SAIMUSICCOMPANY', N'MUSIC COMPANY', N'Saineshwar', CAST(0x0000A73E009B6842 AS DateTime), N'saimm@demo.com', 1, 1)
SET IDENTITY_INSERT [dbo].[RegisterCompany] OFF
/****** Object:  Table [dbo].[MusicStore]    Script Date: 04/19/2017 23:07:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MusicStore](
	[MusicID] [int] IDENTITY(1,1) NOT NULL,
	[MusicName] [varchar](100) NULL,
	[MusicRelease] [datetime] NULL,
	[MovieName] [varchar](100) NULL,
 CONSTRAINT [PK_MusicStore] PRIMARY KEY CLUSTERED 
(
	[MusicID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[MusicStore] ON
INSERT [dbo].[MusicStore] ([MusicID], [MusicName], [MusicRelease], [MovieName]) VALUES (1, N'Laila Main Laila', CAST(0x0000A70500000000 AS DateTime), N'Raees')
INSERT [dbo].[MusicStore] ([MusicID], [MusicName], [MusicRelease], [MovieName]) VALUES (2, N'Zaalima', CAST(0x0000A70500000000 AS DateTime), N'Raees')
INSERT [dbo].[MusicStore] ([MusicID], [MusicName], [MusicRelease], [MovieName]) VALUES (3, N'Udi Udi Jaye', CAST(0x0000A70500000000 AS DateTime), N'Raees')
INSERT [dbo].[MusicStore] ([MusicID], [MusicName], [MusicRelease], [MovieName]) VALUES (4, N'Dhingana', CAST(0x0000A70500000000 AS DateTime), N'Raees')
INSERT [dbo].[MusicStore] ([MusicID], [MusicName], [MusicRelease], [MovieName]) VALUES (5, N'Go Pagal', CAST(0x0000A6FA00000000 AS DateTime), N'Jolly LLB 2')
INSERT [dbo].[MusicStore] ([MusicID], [MusicName], [MusicRelease], [MovieName]) VALUES (6, N'Bawara Mann', CAST(0x0000A6FA00000000 AS DateTime), N'Jolly LLB 2')
INSERT [dbo].[MusicStore] ([MusicID], [MusicName], [MusicRelease], [MovieName]) VALUES (7, N'O Re Rangreza (Qawaali)', CAST(0x0000A6FA00000000 AS DateTime), N'Jolly LLB 2')
INSERT [dbo].[MusicStore] ([MusicID], [MusicName], [MusicRelease], [MovieName]) VALUES (8, N'OK Jaanu', CAST(0x0000A6F100000000 AS DateTime), N'OK Jaanu')
INSERT [dbo].[MusicStore] ([MusicID], [MusicName], [MusicRelease], [MovieName]) VALUES (9, N'The Humma Song', CAST(0x0000A6F100000000 AS DateTime), N'OK Jaanu')
INSERT [dbo].[MusicStore] ([MusicID], [MusicName], [MusicRelease], [MovieName]) VALUES (10, N'Enna Sona', CAST(0x0000A6F100000000 AS DateTime), N'OK Jaanu')
INSERT [dbo].[MusicStore] ([MusicID], [MusicName], [MusicRelease], [MovieName]) VALUES (11, N'Jee Lein', CAST(0x0000A6F100000000 AS DateTime), N'OK Jaanu')
INSERT [dbo].[MusicStore] ([MusicID], [MusicName], [MusicRelease], [MovieName]) VALUES (12, N'Aashiq Surrender Hua', CAST(0x0000A71A00000000 AS DateTime), N'Badrinath Ki Dulhania')
INSERT [dbo].[MusicStore] ([MusicID], [MusicName], [MusicRelease], [MovieName]) VALUES (13, N'Roke Na Ruke Naina', CAST(0x0000A71A00000000 AS DateTime), N'Badrinath Ki Dulhania')
INSERT [dbo].[MusicStore] ([MusicID], [MusicName], [MusicRelease], [MovieName]) VALUES (14, N'Humsafar', CAST(0x0000A71A00000000 AS DateTime), N'Badrinath Ki Dulhania')
INSERT [dbo].[MusicStore] ([MusicID], [MusicName], [MusicRelease], [MovieName]) VALUES (15, N'Tamma Tamma Again', CAST(0x0000A71A00000000 AS DateTime), N'Badrinath Ki Dulhania')
SET IDENTITY_INSERT [dbo].[MusicStore] OFF
/****** Object:  Table [dbo].[ClientKey]    Script Date: 04/19/2017 23:07:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ClientKey](
	[ClientKeyID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyID] [int] NULL,
	[ClientID] [varchar](100) NULL,
	[ClientSecret] [varchar](100) NULL,
	[CreateOn] [datetime] NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_ClientKeys] PRIMARY KEY CLUSTERED 
(
	[ClientKeyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[ClientKey] ON
INSERT [dbo].[ClientKey] ([ClientKeyID], [CompanyID], [ClientID], [ClientSecret], [CreateOn], [UserID]) VALUES (1, 1, N'pGU6RJ8ELcVRZmN', N'tiIfdZ3vh5IwGVm', CAST(0x0000A73F009199A3 AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[ClientKey] OFF

--------------------------------

create table TodoItem
(
id int identity(1,1),
Description varchar(50),
DueDate varchar(20),
isDone bit);

insert into TodoItem values('test1','2016-08-09',0)
insert into TodoItem values('test2','2016-09-09',0)

select * from TodoItem






