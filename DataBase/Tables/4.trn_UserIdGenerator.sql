USE [POSApi]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS(SELECT TOP 1 'x' FROM sys.tables WHERE [name]='trn_UserIdGenerator')
BEGIN

CREATE TABLE [dbo].[trn_UserIdGenerator](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CurrentId] [int] NOT NULL,
	[UserId] [varchar](100) NULL UNIQUE,
	[CreatedDateTime] [datetime] NULL,
	[LastModified] [timestamp] NOT NULL
)

END
GO



