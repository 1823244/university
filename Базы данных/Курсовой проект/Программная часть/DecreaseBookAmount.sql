USE library
GO
CREATE TRIGGER DecreaseBookAmount
ON [Instance] AFTER DELETE
AS BEGIN
	UPDATE Book
	SET Amount = Amount - 1
	WHERE ID in (SELECT BookID FROM inserted)
END;