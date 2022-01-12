USE [POSApi]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT TOP 1 'x' FROM sys.procedures WHERE [name]='Save_ApplicationUsers')
BEGIN

	DROP PROCEDURE [dbo].[Save_ApplicationUsers]

END
GO

--======================================================================  
--Author		: Prakash S  
--CreatedBy		: 2022-01-08 
--Description	: To Save Application Users  
--======================================================================  
CREATE PROCEDURE [dbo].[Save_ApplicationUsers]
(
@EmailId VARCHAR(500),
@Password VARCHAR(500),
@PasswordHash VARCHAR(500)
)
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @CreatedDateTime DATETIME=GETDATE();
	DECLARE @ErrorMessage VARCHAR(MAX);
	DECLARE @ReturnVal INT=-1;
	DECLARE @CurrentId INT=0;
	DECLARE @UserId VARCHAR(100);
	DECLARE @ScopeId INT;
	DECLARE @CreatedBy VARCHAR(100)='SYSTEM'

	BEGIN TRY

		BEGIN TRANSACTION SAVE_USER;

			SELECT @CurrentId=ISNULL(MAX(CurrentId),0)+1 FROM [dbo].[trn_UserIdGenerator]

			SELECT @UserId='USR'+RIGHT(REPLICATE('0',3)+CAST(@CurrentId AS VARCHAR(10)),3)

			INSERT INTO [dbo].[trn_UserIdGenerator]
			(
				 CurrentId
				,CreatedDateTime
			)
			VALUES
			(
				 @CurrentId
				,@CreatedDateTime
			)

			SELECT @ScopeId=SCOPE_IDENTITY();

			INSERT INTO [dbo].[trn_ApplicationUsers]
			(
				 UserId
				,EmailId
				,[Password]
				,[PasswordHash]
				,CreatedBy
				,CreatedDateTime
				,UpdatedBy
				,UpdatedDateTime
			)
			VALUES 
			(
				 @UserId
				,@EmailId
				,@Password
				,@PasswordHash
				,@CreatedBy
				,@CreatedDateTime
				,@CreatedBy
				,@CreatedDateTime
			)

			UPDATE 
				[dbo].[trn_UserIdGenerator]
			SET 
				UserId=@UserId
			WHERE(1=1)
			AND Id=@ScopeId 
			AND CurrentId=@CurrentId

		COMMIT TRANSACTION SAVE_USER;

		SELECT @ReturnVal=1;

		GOTO SUCCESS;

	END TRY
	BEGIN CATCH
		
		IF(@@TRANCOUNT>0)
		BEGIN

			ROLLBACK TRANSACTION SAVE_USER;

		END

		SET @ErrorMessage='Error Procedure-#'+CAST(ERROR_PROCEDURE() AS VARCHAR(MAX))+
							', Error Line-#'+CAST(ERROR_LINE() AS VARCHAR(5))+', Error Message-#'+CAST(ERROR_MESSAGE() AS VARCHAR(MAX));

		RAISERROR(@ErrorMessage,16,1);

		SET @ReturnVal=0;

		GOTO ERROR;

	END CATCH

	SUCCESS:
		RETURN(@ReturnVal);
	ERROR:
		RETURN(@ReturnVal);

	SET NOCOUNT OFF;

END
GO


