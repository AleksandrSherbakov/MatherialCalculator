USE [MaterialsCalculator]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 14.02.2024 9:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[GenderID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[GenderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manufacturer]    Script Date: 14.02.2024 9:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manufacturer](
	[ManufacturerID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[Email] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ManufacturerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Material]    Script Date: 14.02.2024 9:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Material](
	[MaterialID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[Image] [nvarchar](20) NULL,
	[Square] [nvarchar](20) NULL,
	[Price] [nvarchar](20) NULL,
	[Count] [nvarchar](20) NULL,
	[MaterialTypeID] [int] NULL,
	[ManufacturerID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaterialID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaterialType]    Script Date: 14.02.2024 9:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialType](
	[MaterialTypeID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[Picture] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaterialTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 14.02.2024 9:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 14.02.2024 9:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[GenderID] [int] NULL,
	[RoleID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Gender] ([GenderID], [Name], [Description]) VALUES (1, N'Male', N'Male gender')
INSERT [dbo].[Gender] ([GenderID], [Name], [Description]) VALUES (2, N'Female', N'Female gender')
GO
INSERT [dbo].[Manufacturer] ([ManufacturerID], [Name], [Description], [Email]) VALUES (1, N'TileCrafters', N'Specializing in Tile', N'info@tilecrafters.com')
INSERT [dbo].[Manufacturer] ([ManufacturerID], [Name], [Description], [Email]) VALUES (2, N'RoofWorks', N'Experts in Roof', N'contact@roof.com')
INSERT [dbo].[Manufacturer] ([ManufacturerID], [Name], [Description], [Email]) VALUES (3, N'LogsMasters', N'Innovators in Logs manufacturing', N'info@logsmasters.com')
GO
SET IDENTITY_INSERT [dbo].[Material] ON 

INSERT [dbo].[Material] ([MaterialID], [Name], [Description], [Image], [Square], [Price], [Count], [MaterialTypeID], [ManufacturerID]) VALUES (1, N'BlackTile', N'Like batman', N'batman.png', N'25', N'10', N'1000', 1, 1)
SET IDENTITY_INSERT [dbo].[Material] OFF
GO
INSERT [dbo].[MaterialType] ([MaterialTypeID], [Name], [Description], [Picture]) VALUES (1, N'Tile', N'Tile materials', N'Tile.png')
INSERT [dbo].[MaterialType] ([MaterialTypeID], [Name], [Description], [Picture]) VALUES (2, N'Roof', N'Roof materials', N'Roof.png')
INSERT [dbo].[MaterialType] ([MaterialTypeID], [Name], [Description], [Picture]) VALUES (3, N'Logs', N'Logs materials', N'Logs.png')
GO
INSERT [dbo].[Role] ([RoleID], [Name], [Description]) VALUES (1, N'Admin', N'Administrator role')
INSERT [dbo].[Role] ([RoleID], [Name], [Description]) VALUES (2, N'User', N'Regular user role')
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [LastName], [FirstName], [MiddleName], [Email], [Password], [GenderID], [RoleID]) VALUES (1, N'admin', N'admin', N'admin', N'admin@admin.com', N'Admin!1', 1, 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Material]  WITH CHECK ADD FOREIGN KEY([ManufacturerID])
REFERENCES [dbo].[Manufacturer] ([ManufacturerID])
GO
ALTER TABLE [dbo].[Material]  WITH CHECK ADD FOREIGN KEY([MaterialTypeID])
REFERENCES [dbo].[MaterialType] ([MaterialTypeID])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([GenderID])
REFERENCES [dbo].[Gender] ([GenderID])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
