USE [POSApi]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT TOP 1 'x' FROM sys.procedures WHERE [name]='Get_UserInfo')
BEGIN

	DROP PROCEDURE [dbo].[Get_UserInfo]  

END
GO
--=====================================================================  
--Author   : Prakash S  
--CreatedDate  : 2022-01-08  
--Description  : To Get User Details by UserId or EmailId  
--=====================================================================  
CREATE PROCEDURE [dbo].[Get_UserInfo]  
(  
@UserId VARCHAR(200)=NULL,  
@EmailId VARCHAR(200)=NULL  
)  
AS  
BEGIN  
   
 DECLARE @ErrorMessage NVARCHAR(MAX)=NULL;  
 DECLARE @ReturnVal INT=-1;  
  
 BEGIN TRY  
  
  IF @UserId IS NULL AND @EmailId IS NULL  
  BEGIN  
  
   RAISERROR('Both @UserId or @EmailId cannot be empty.',16,1);  
  
  END  
  
  SELECT   
    UserId  
   ,EmailId  
   ,PasswordHash [Password]  
   ,CountryCode  
   ,MobileNo  
   ,ProfileImageUrl  
  FROM  
   [dbo].[trn_ApplicationUsers](NOLOCK)  
  WHERE(1=1)  
  AND (UserId=@UserId OR @UserId IS NULL)  
  AND (EmailId=@EmailId OR @EmailId IS NULL)  
  
  SET @ReturnVal=1;  
  
  GOTO SUCCESS;  
  
 END TRY  
 BEGIN CATCH  
  
  
  SET @ErrorMessage='Error Procedure-#'+CAST(ERROR_PROCEDURE() AS VARCHAR(MAX))+  
       ', Error Line-#'+CAST(ERROR_LINE() AS VARCHAR(5))+', Error Message-#'+CAST(ERROR_MESSAGE() AS VARCHAR(MAX));  
  RAISERROR(@ErrorMessage,16,1);  
  
  SET @ReturnVal=0;  
  
  GOTO ERROR;  
  
 END CATCH  
  
 SUCCESS:  
  RETURN(@ReturnVal);  
 ERROR:  
  RETURN(@returnVal);  
  
  
END  
GO


