USE library
GO
CREATE PROCEDURE ReturnBook
	@id_instance int
AS BEGIN
	UPDATE Instance
	SET Available = 1, ReaderID = NULL, Issue = NULL
	WHERE ID = @id_instance
END;


USE library
GO
EXEC ReturnBook <ID> 
GO