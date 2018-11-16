CREATE PROCEDURE [dbo].[DeleteUser]
		@id INT
AS
	Delete From Users 
	Where Id = @id
RETURN 0
