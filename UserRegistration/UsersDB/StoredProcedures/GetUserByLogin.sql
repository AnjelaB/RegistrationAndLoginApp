CREATE PROCEDURE [dbo].[GetUserByLogin]
	@login NVARCHAR(MAX)
AS
	SELECT *
	From Users
	Where [Login] = @login
RETURN 0
