USE [POSApi]
GO
SET IDENTITY_INSERT [dbo].[ref_LogType] ON 
GO
INSERT [dbo].[ref_LogType] ([Id], [TypeId], [TypeDescription], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdateDateTime]) VALUES (1, 1, N'INFO', N'SYSTEM', CAST(N'2022-01-08T19:27:23.800' AS DateTime), N'SYSTEM', CAST(N'2022-01-08T19:27:23.800' AS DateTime))
GO
INSERT [dbo].[ref_LogType] ([Id], [TypeId], [TypeDescription], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdateDateTime]) VALUES (2, 2, N'SUCCESS', N'SYSTEM', CAST(N'2022-01-08T19:27:23.800' AS DateTime), N'SYSTEM', CAST(N'2022-01-08T19:27:23.800' AS DateTime))
GO
INSERT [dbo].[ref_LogType] ([Id], [TypeId], [TypeDescription], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdateDateTime]) VALUES (3, 3, N'WARNING', N'SYSTEM', CAST(N'2022-01-08T19:27:23.800' AS DateTime), N'SYSTEM', CAST(N'2022-01-08T19:27:23.800' AS DateTime))
GO
INSERT [dbo].[ref_LogType] ([Id], [TypeId], [TypeDescription], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdateDateTime]) VALUES (4, 4, N'ERROR', N'SYSTEM', CAST(N'2022-01-08T19:27:23.800' AS DateTime), N'SYSTEM', CAST(N'2022-01-08T19:27:23.800' AS DateTime))
GO
INSERT [dbo].[ref_LogType] ([Id], [TypeId], [TypeDescription], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdateDateTime]) VALUES (5, 5, N'LOG', N'SYSTEM', CAST(N'2022-01-08T19:27:23.800' AS DateTime), N'SYSTEM', CAST(N'2022-01-08T19:27:23.800' AS DateTime))
GO
INSERT [dbo].[ref_LogType] ([Id], [TypeId], [TypeDescription], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdateDateTime]) VALUES (6, 6, N'SQL', N'SYSTEM', CAST(N'2022-01-08T19:27:23.800' AS DateTime), N'SYSTEM', CAST(N'2022-01-08T19:27:23.800' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[ref_LogType] OFF
GO
