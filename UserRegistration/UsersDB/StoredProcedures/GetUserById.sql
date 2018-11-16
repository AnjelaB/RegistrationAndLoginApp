CREATE PROCEDURE [dbo].[GetUserById]
	@userId INT
AS
	SELECT *
	From Users
	Where Id = @userId
RETURN 0
