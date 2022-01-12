USE [POSApi]
GO

ALTER TABLE [dbo].[trn_ApplicationLogs]  WITH CHECK ADD  CONSTRAINT [FK_trn_ApplicationLogs_ref_LogType] FOREIGN KEY([LogTypeId])
REFERENCES [dbo].[ref_LogType] ([TypeId])
GO

ALTER TABLE [dbo].[trn_ApplicationLogs] CHECK CONSTRAINT [FK_trn_ApplicationLogs_ref_LogType]
GO


