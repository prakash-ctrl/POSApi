USE [POSApi]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT * FROM sys.procedures WHERE [name]='Get_ref_LogType')
BEGIN

	DROP PROCEDURE [dbo].[Get_ref_LogType]

END
GO
--================================================================
--Author			:	Prakash S
--CreatedBy			:	2022-01-08
--Description		:	To Get Log Type
--================================================================
CREATE PROCEDURE [dbo].[Get_ref_LogType]
AS
BEGIN

SET NOCOUNT ON;

	SELECT TypeId,TypeDescription FROM ref_LogType(NOLOCK)

SET NOCOUNT OFF;

END
GO


