USE [BlogDB]
GO
/****** Object:  Table [dbo].[BlogPost]    Script Date: 30/01/2021 02:11:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogPost](
	[Id] [uniqueidentifier] NOT NULL,
	[authorId] [uniqueidentifier] NULL,
	[title] [varchar](100) NULL,
	[metaTitle] [varchar](100) NULL,
	[summary] [varchar](500) NULL,
	[published] [bit] NULL,
	[content] [varchar](max) NULL,
	[Disabled] [bit] NULL,
	[Created] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Registration]    Script Date: 30/01/2021 02:11:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Registration](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NULL,
	[PhoneNumber] [varchar](50) NULL,
	[EmailAddress] [varchar](50) NULL,
	[City] [varchar](100) NULL,
	[State] [varchar](100) NULL,
	[Address] [varchar](255) NULL,
	[PostalCode] [varchar](100) NULL,
	[Disabled] [bit] NULL,
	[Created] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 30/01/2021 02:11:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NULL,
	[UserName] [varchar](100) NULL,
	[UserPassword] [varchar](100) NULL,
	[PhoneNumber] [varchar](50) NULL,
	[EmailAddress] [varchar](50) NULL,
	[City] [varchar](100) NULL,
	[State] [varchar](100) NULL,
	[Address] [varchar](255) NULL,
	[PostalCode] [varchar](100) NULL,
	[Disabled] [bit] NULL,
	[Created] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[Modified] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BlogPost] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Registration] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (newid()) FOR [Id]
GO
/****** Object:  StoredProcedure [dbo].[DeleteBlogPost]    Script Date: 30/01/2021 02:11:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteBlogPost]    
  @Id [uniqueidentifier]    
AS    
BEGIN    
  DELETE FROM [dbo].[BlogPost]    
  WHERE [Id] = @Id    
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllBlogPost]    Script Date: 30/01/2021 02:11:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllBlogPost]    
AS    
BEGIN    
 SELECT [Id], [authorId], [title], [metaTitle], [summary], [published], [content], [Disabled]  ,[Created], [CreatedBy], [Modified], [ModifiedBy]   
 FROM [dbo].[BlogPost]  
END 
GO
/****** Object:  StoredProcedure [dbo].[GetBlogPost]    Script Date: 30/01/2021 02:11:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBlogPost]    
  @Id [uniqueidentifier]    
AS    
BEGIN    
 SELECT [Id], [authorId], [title], [metaTitle], [summary], [published], [content], [Disabled]  ,[Created], [CreatedBy], [Modified], [ModifiedBy]   
 FROM [dbo].[BlogPost]    
 WHERE [Id] = @Id    
END 
GO
/****** Object:  StoredProcedure [dbo].[InsertBlogPost]    Script Date: 30/01/2021 02:11:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertBlogPost]    
 (    
  @authorId [UNIQUEIDENTIFIER],   
  @title [nvarchar](100),
  @metaTitle [nvarchar](100),       
  @summary [nvarchar](500),   
  @published [bit]  ,   
  @content [nvarchar](max),     
  @Disabled [bit]  ,  
  
  @Created [datetime],  
  @CreatedBy [nvarchar](50),   
  @Modified [datetime],  
  @ModifiedBy [nvarchar](50)   
 )    
AS    
SET NOCOUNT ON    
BEGIN    
 DECLARE @NewID uniqueidentifier = NEWID()    
 INSERT INTO [dbo].[BlogPost]    
 (    
  [Id], [authorId], [title], [metaTitle], [summary], [published], [content], [Disabled]  ,[Created], [CreatedBy], [Modified], [ModifiedBy]  
 )    
 VALUES    
 (    
  @NewID,    
  @authorId, 
  @title,
  @metaTitle ,      
  @summary,   
  @published,    
  @content,        
  @Disabled,  
  @Created,  
  @CreatedBy,  
  @Modified,  
  @ModifiedBy  
 )     
 SELECT @NewID    
END 
GO
/****** Object:  StoredProcedure [dbo].[InsertRegistration]    Script Date: 30/01/2021 02:11:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertRegistration]    
 (    
  @Name [nvarchar](50),   
  @UserName [nvarchar](50),
  @UserPassword [nvarchar](50),       
  @EmailAddress [nvarchar](100),   
  @PhoneNumber [nvarchar](50),    
  @Address [nvarchar](100),     
  @City [nvarchar](100),    
  @State [nvarchar](50),    
  @PostalCode [nvarchar](20),   
  @Disabled [bit]  ,  
  
  @Created [datetime],  
  @CreatedBy [nvarchar](50),   
  @Modified [datetime],  
  @ModifiedBy [nvarchar](50)   
 )    
AS    
SET NOCOUNT ON    
BEGIN    
 DECLARE @NewID uniqueidentifier = NEWID()    
 INSERT INTO [dbo].[Users]    
 (    
  [Id], [Name], [UserName], [UserPassword], [EmailAddress], [PhoneNumber], [Address], [City], [State], [PostalCode], [Disabled]  ,[Created], [CreatedBy], [Modified], [ModifiedBy]  
 )    
 VALUES    
 (    
  @NewID,    
  @Name, 
  @UserName,
  @UserPassword ,      
  @EmailAddress,   
  @PhoneNumber,    
  @Address,     
  @City,    
  @State,    
  @PostalCode,     
  @Disabled,  
  @Created,  
  @CreatedBy,  
  @Modified,  
  @ModifiedBy  
 )     
 SELECT @NewID    
END 
GO
/****** Object:  StoredProcedure [dbo].[UpdateBlogPost]    Script Date: 30/01/2021 02:11:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateBlogPost]    
 (    
  @Id [uniqueidentifier],  
  @authorId [UNIQUEIDENTIFIER],   
  @title [nvarchar](100),
  @metaTitle [nvarchar](100),       
  @summary [nvarchar](500),   
  @published [bit]  ,   
  @content [nvarchar](max),     
  @Disabled [bit]  ,  
  
  @Created [datetime],  
  @CreatedBy [nvarchar](50),   
  @Modified [datetime],  
  @ModifiedBy [nvarchar](50)    
 )    
AS    
SET NOCOUNT ON    
BEGIN    
  
  
 UPDATE [dbo].[BlogPost]    
  SET [title] = @title, [metaTitle] = @metaTitle, [summary] = @summary, [published] = @published, [content] = @content,  [Disabled] = @Disabled, Modified = @Modified, ModifiedBy =  @ModifiedBy  
    
  WHERE [Id] = @Id   
  
  
  
    
    
END 
GO
/****** Object:  StoredProcedure [dbo].[UserLogin]    Script Date: 30/01/2021 02:11:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserLogin]    
 (    
  @UserName [nvarchar](50),   
  @Password [nvarchar](50)
 )    
AS    
SET NOCOUNT ON    
BEGIN    

	select [Id], [Name], [UserName], [UserPassword], [EmailAddress], [PhoneNumber], [Address], [City], [State], [PostalCode], [Disabled]  ,[Created], [CreatedBy], [Modified], [ModifiedBy] 
	from users where [UserName] = @UserName and [UserPassword] = @Password
 --DECLARE @NewID uniqueidentifier = NEWID()    
 --INSERT INTO [dbo].[Users]    
 --(    
 -- [Id], [Name], [UserName], [UserPassword], [EmailAddress], [PhoneNumber], [Address], [City], [State], [PostalCode], [Disabled]  ,[Created], [CreatedBy], [Modified], [ModifiedBy]  
 --)    
 --VALUES    
 --(    
 -- @NewID,    
 -- @Name, 
 -- @UserName,
 -- @UserPassword ,      
 -- @EmailAddress,   
 -- @PhoneNumber,    
 -- @Address,     
 -- @City,    
 -- @State,    
 -- @PostalCode,     
 -- @Disabled,  
 -- @Created,  
 -- @CreatedBy,  
 -- @Modified,  
 -- @ModifiedBy  
 --)     
 --SELECT @NewID    
END 
GO
