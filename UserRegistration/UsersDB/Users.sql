﻿CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[FirstName] NVARCHAR(MAX) NOT NULL,
	[LastName] NVARCHAR(MAX) NOT NULL,
	[Login] NVARCHAR(MAX) NOT NULL,
	[Password] NVARCHAR(MAX) NOT NULL
)
