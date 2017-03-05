USE library
GO
CREATE PROCEDURE GiveBook
	@id_instance int,
	@id_reader int
AS BEGIN
	UPDATE Instance
	SET Available = 0, ReaderID = @id_reader, Issue = CONVERT (date, GETDATE())
	WHERE ID = @id_instance
END;

USE library
GO
EXEC GiveBook <ID>, <ID Reader> 
GO