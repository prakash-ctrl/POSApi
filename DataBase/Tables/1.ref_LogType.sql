USE [POSApi]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS(SELECT TOP 1 'x' FROM sys.tables WHERE [name]='ref_LogType')
BEGIN


	CREATE TABLE [dbo].[ref_LogType](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[TypeId] [int] NOT NULL PRIMARY KEY,
		[TypeDescription] [varchar](500) NULL,
		[CreatedBy] [varchar](200) NOT NULL,
		[CreatedDateTime] [datetime] NOT NULL,
		[UpdatedBy] [varchar](200) NOT NULL,
		[UpdateDateTime] [datetime] NOT NULL,
		[LastModified] [timestamp] NOT NULL
	)

END

GO