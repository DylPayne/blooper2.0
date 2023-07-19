CREATE PROCEDURE update_message
	@id int,
	@text varchar(255)
AS
BEGIN
	UPDATE messages
	SET text = @text
	WHERE id = @id
END