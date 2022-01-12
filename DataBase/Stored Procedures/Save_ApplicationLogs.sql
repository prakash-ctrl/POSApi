USE [POSApi]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT TOP 1 'x' FROM sys.procedures WHERE [name]='Save_ApplicationLogs')
BEGIN

	DROP PROCEDURE [dbo].[Save_ApplicationLogs] 

END  

GO
  
--======================================================================  
--Author  : Prakash S  
--CreatedBy  : 2022-01-08  
--Description : To Save Application Logs  
--======================================================================  
CREATE PROCEDURE [dbo].[Save_ApplicationLogs]  
(  
 @LogTypeId   INT  
,@LogFrom   VARCHAR(MAX)=NULL  
,@ClientIPAddress VARCHAR(200)=NULL  
,@ServerIPAddress VARCHAR(200)=NULL  
,@StackTrace  VARCHAR(MAX)=NULL  
,@Exception   VARCHAR(MAX)=NULL  
,@InnerException VARCHAR(MAX)=NULL  
,@Message   VARCHAR(MAX)=NULL  
,@CreatedBy   VARCHAR(200)  
)  
AS  
BEGIN  
    
   
  
 DECLARE @CreatedDateTime DATETIME=GETDATE();  
 DECLARE @ErrorMessage NVARCHAR(MAX);  
 DECLARE @ReturnVal INT=-1;  
  
 BEGIN TRY  
    
  
  
  INSERT INTO [dbo].[trn_applicationlogs]  
  (  
    LogTypeId  
   ,LogFrom  
   ,ClientIPAddress  
   ,ServerIPAddress  
   ,StackTrace  
   ,Exception  
   ,InnerException  
   ,[Message]  
   ,CreatedBy  
   ,CreatedDateTime  
  )  
  VALUES  
  (  
    @LogTypeId     
   ,@LogFrom     
   ,@ClientIPAddress   
   ,@ServerIPAddress   
   ,@StackTrace    
   ,@Exception     
   ,@InnerException   
   ,@Message     
   ,@CreatedBy  
   ,@CreatedDateTime  
  )  
  
  
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
  RETURN(@ReturnVal);  
  
 SET NOCOUNT OFF;  
  
END  
GO


