USE library
GO
CREATE TRIGGER IncreaseBookAmount
ON [Instance] AFTER INSERT
AS BEGIN
	UPDATE Book
	SET Amount = Amount + 1
	WHERE ID in (SELECT BookID FROM inserted)
END;