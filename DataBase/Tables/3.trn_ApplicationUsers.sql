USE [POSApi]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS(SELECT TOP 1 'x' FROM sys.tables WHERE [name]='trn_ApplicationUsers')
BEGIN
CREATE TABLE [dbo].[trn_ApplicationUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [varchar](100) NOT NULL PRIMARY KEY,
	[EmailId] [varchar](500) NOT NULL UNIQUE,
	[Password] [varchar](500) NOT NULL,
	[PasswordHash] [varchar](500) NOT NULL,
	[CountryCode] [varchar](100) NULL,
	[MobileNo] [varchar](100) NULL,
	[ProfileImageUrl] [varchar](max) NULL,
	[CreatedBy] [varchar](100) NULL,
	[CreatedDateTime] [datetime] NULL,
	[UpdatedBy] [varchar](100) NULL,
	[UpdatedDateTime] [datetime] NULL,
	[LastModified] [timestamp] NOT NULL
)
END
GO


