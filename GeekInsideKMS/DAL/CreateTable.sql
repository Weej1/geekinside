USE [geekinsidekms]
GO
/****** Object:  Table [dbo].[UserAdmin]    Script Date: 06/30/2012 12:15:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAdmin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeNumber] [int] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[LastLoginTime] [datetime] NULL,
 CONSTRAINT [PK_UserAdmins] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tag]    Script Date: 06/30/2012 12:15:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tag](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TagName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SiteNews]    Script Date: 06/30/2012 12:15:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiteNews](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[NewsContent] [ntext] NOT NULL,
	[PubTime] [datetime] NOT NULL,
	[IsOnTop] [bit] NOT NULL,
 CONSTRAINT [PK_SiteNews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SiteConfig]    Script Date: 06/30/2012 12:15:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiteConfig](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PropertyName] [nvarchar](50) NOT NULL,
	[PropertyValue] [nvarchar](200) NULL,
	[PropertyDescription] [nvarchar](200) NULL,
 CONSTRAINT [PK_SiteConfig] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Folder]    Script Date: 06/30/2012 12:15:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Folder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FolderName] [nvarchar](50) NOT NULL,
	[ParentId] [int] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[PhysicalPath] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FileType]    Script Date: 06/30/2012 12:15:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NULL,
 CONSTRAINT [PK_FileType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Document]    Script Date: 06/30/2012 12:15:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Document](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileDisplayName] [nvarchar](50) NOT NULL,
	[FileDiskName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[FolderId] [int] NOT NULL,
	[FileTypeId] [int] NOT NULL,
	[PublisherNumber] [int] NOT NULL,
	[PublisherName] [nvarchar](50) NOT NULL,
	[PubTime] [datetime] NOT NULL,
	[CheckerNumber] [int] NOT NULL,
	[CheckerName] [nvarchar](50) NOT NULL,
	[Size] [nvarchar](50) NOT NULL,
	[ViewNumber] [int] NOT NULL,
	[DownloadNumber] [int] NOT NULL,
	[IsChecked] [bit] NOT NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'带后缀名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Document', @level2type=N'COLUMN',@level2name=N'FileDisplayName'
GO
/****** Object:  Table [dbo].[Department]    Script Date: 06/30/2012 12:15:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](50) NOT NULL,
	[FolderId] [int] NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserEmployee]    Script Date: 06/30/2012 12:15:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserEmployee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeNumber] [int] NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[IsManager] [bit] NOT NULL,
	[IsChecker] [bit] NOT NULL,
	[IsAvailable] [bit] NOT NULL,
	[LastLoginTime] [datetime] NULL,
 CONSTRAINT [PK_UserEmployee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserEmployee] ON [dbo].[UserEmployee] 
(
	[EmployeeNumber] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentTag]    Script Date: 06/30/2012 12:15:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentTag](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
 CONSTRAINT [PK_DocumentTag] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserEmployeeDetail]    Script Date: 06/30/2012 12:15:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserEmployeeDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeNumber] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserEmployeeDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Favorite]    Script Date: 06/30/2012 12:15:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favorite](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeNumber] [int] NOT NULL,
	[DocumentId] [int] NOT NULL,
 CONSTRAINT [PK_Favorite] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_Document_CheckerNumber]    Script Date: 06/30/2012 12:15:08 ******/
ALTER TABLE [dbo].[Document] ADD  CONSTRAINT [DF_Document_CheckerNumber]  DEFAULT ((0)) FOR [CheckerNumber]
GO
/****** Object:  Default [DF_Document_ViewNumber]    Script Date: 06/30/2012 12:15:08 ******/
ALTER TABLE [dbo].[Document] ADD  CONSTRAINT [DF_Document_ViewNumber]  DEFAULT ((0)) FOR [ViewNumber]
GO
/****** Object:  Default [DF_Document_DownloadNumber]    Script Date: 06/30/2012 12:15:08 ******/
ALTER TABLE [dbo].[Document] ADD  CONSTRAINT [DF_Document_DownloadNumber]  DEFAULT ((0)) FOR [DownloadNumber]
GO
/****** Object:  Default [DF_Document_IsChecked]    Script Date: 06/30/2012 12:15:08 ******/
ALTER TABLE [dbo].[Document] ADD  CONSTRAINT [DF_Document_IsChecked]  DEFAULT ((0)) FOR [IsChecked]
GO
/****** Object:  Default [DF_Category_ParentId]    Script Date: 06/30/2012 12:15:08 ******/
ALTER TABLE [dbo].[Folder] ADD  CONSTRAINT [DF_Category_ParentId]  DEFAULT ((0)) FOR [ParentId]
GO
/****** Object:  Default [DF_SiteNews_IsOnTop]    Script Date: 06/30/2012 12:15:08 ******/
ALTER TABLE [dbo].[SiteNews] ADD  CONSTRAINT [DF_SiteNews_IsOnTop]  DEFAULT ((0)) FOR [IsOnTop]
GO
/****** Object:  Default [DF_UserEmployee_IsManager]    Script Date: 06/30/2012 12:15:08 ******/
ALTER TABLE [dbo].[UserEmployee] ADD  CONSTRAINT [DF_UserEmployee_IsManager]  DEFAULT ((0)) FOR [IsManager]
GO
/****** Object:  Default [DF_UserEmployee_IsChecker]    Script Date: 06/30/2012 12:15:08 ******/
ALTER TABLE [dbo].[UserEmployee] ADD  CONSTRAINT [DF_UserEmployee_IsChecker]  DEFAULT ((0)) FOR [IsChecker]
GO
/****** Object:  Default [DF_UserEmployee_IsAvailable]    Script Date: 06/30/2012 12:15:08 ******/
ALTER TABLE [dbo].[UserEmployee] ADD  CONSTRAINT [DF_UserEmployee_IsAvailable]  DEFAULT ((1)) FOR [IsAvailable]
GO
/****** Object:  ForeignKey [FK_Department_Folder]    Script Date: 06/30/2012 12:15:08 ******/
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Folder] FOREIGN KEY([FolderId])
REFERENCES [dbo].[Folder] ([Id])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Folder]
GO
/****** Object:  ForeignKey [FK_Document_FileType]    Script Date: 06/30/2012 12:15:08 ******/
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_Document_FileType] FOREIGN KEY([FileTypeId])
REFERENCES [dbo].[FileType] ([Id])
GO
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_Document_FileType]
GO
/****** Object:  ForeignKey [FK_Document_Folder]    Script Date: 06/30/2012 12:15:08 ******/
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_Document_Folder] FOREIGN KEY([FolderId])
REFERENCES [dbo].[Folder] ([Id])
GO
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_Document_Folder]
GO
/****** Object:  ForeignKey [FK_DocumentTag_Document]    Script Date: 06/30/2012 12:15:08 ******/
ALTER TABLE [dbo].[DocumentTag]  WITH CHECK ADD  CONSTRAINT [FK_DocumentTag_Document] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Document] ([Id])
GO
ALTER TABLE [dbo].[DocumentTag] CHECK CONSTRAINT [FK_DocumentTag_Document]
GO
/****** Object:  ForeignKey [FK_DocumentTag_Tag]    Script Date: 06/30/2012 12:15:08 ******/
ALTER TABLE [dbo].[DocumentTag]  WITH CHECK ADD  CONSTRAINT [FK_DocumentTag_Tag] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tag] ([Id])
GO
ALTER TABLE [dbo].[DocumentTag] CHECK CONSTRAINT [FK_DocumentTag_Tag]
GO
/****** Object:  ForeignKey [FK_Favorite_Document]    Script Date: 06/30/2012 12:15:08 ******/
ALTER TABLE [dbo].[Favorite]  WITH CHECK ADD  CONSTRAINT [FK_Favorite_Document] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Document] ([Id])
GO
ALTER TABLE [dbo].[Favorite] CHECK CONSTRAINT [FK_Favorite_Document]
GO
/****** Object:  ForeignKey [FK_Favorite_UserEmployee]    Script Date: 06/30/2012 12:15:08 ******/
ALTER TABLE [dbo].[Favorite]  WITH CHECK ADD  CONSTRAINT [FK_Favorite_UserEmployee] FOREIGN KEY([EmployeeNumber])
REFERENCES [dbo].[UserEmployee] ([EmployeeNumber])
GO
ALTER TABLE [dbo].[Favorite] CHECK CONSTRAINT [FK_Favorite_UserEmployee]
GO
/****** Object:  ForeignKey [FK_UserEmployee_Department]    Script Date: 06/30/2012 12:15:08 ******/
ALTER TABLE [dbo].[UserEmployee]  WITH CHECK ADD  CONSTRAINT [FK_UserEmployee_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[UserEmployee] CHECK CONSTRAINT [FK_UserEmployee_Department]
GO
/****** Object:  ForeignKey [FK_UserEmployeeDetail_UserEmployee]    Script Date: 06/30/2012 12:15:08 ******/
ALTER TABLE [dbo].[UserEmployeeDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserEmployeeDetail_UserEmployee] FOREIGN KEY([EmployeeNumber])
REFERENCES [dbo].[UserEmployee] ([EmployeeNumber])
GO
ALTER TABLE [dbo].[UserEmployeeDetail] CHECK CONSTRAINT [FK_UserEmployeeDetail_UserEmployee]
GO
