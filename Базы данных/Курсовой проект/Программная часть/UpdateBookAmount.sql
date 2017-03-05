USE library
GO
CREATE TRIGGER UpdateBookAmount
ON [Instance] AFTER UPDATE
AS BEGIN
	DECLARE @available_old bit,
			@available_new bit;
	SET @available_old = (SELECT Available FROM deleted);
	SET @available_new = (SELECT Available FROM inserted);

	IF @available_old != @available_new
	BEGIN
		IF @available_new = 1
			BEGIN
				UPDATE Book
				SET Amount = Amount + 1
				WHERE ID in (SELECT BookID FROM inserted)
			END
		ELSE
			BEGIN
				UPDATE Book
				SET Amount = Amount - 1
				WHERE ID in (SELECT BookID FROM inserted)
			END
	END
END;