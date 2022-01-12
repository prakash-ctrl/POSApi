USE [POSApi]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS(SELECT TOP 1 'x' FROM sys.tables WHERE [name]='trn_ApplicationLogs')
BEGIN

CREATE TABLE [dbo].[trn_ApplicationLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LogTypeId] [int] NOT NULL,
	[LogFrom] [varchar](500) NULL,
	[ClientIPAddress] [varchar](50) NULL,
	[ServerIPAddress] [varchar](50) NULL,
	[StackTrace] [varchar](max) NULL,
	[Exception] [varchar](max) NULL,
	[InnerException] [varchar](max) NULL,
	[Message] [varchar](max) NULL,
	[CreatedBy] [varchar](200) NULL,
	[CreatedDateTime] [datetime] NULL,
	[LastModified] [timestamp] NOT NULL
)

END

GO


