CREATE PROCEDURE [dbo].[InsertUser]
	@firstName NVARCHAR(MAX),
	@lastName NVARCHAR(MAX),
	@login NVARCHAR(MAX),
	@password NVARCHAR(MAX)
AS
	Insert into Users(FirstName,LastName,[Login],[Password])
	Values (@firstName,@lastName,@login,@password)
RETURN 0
