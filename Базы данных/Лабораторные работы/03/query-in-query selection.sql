USE [library]
GO

SELECT [Title], [Publisher], [Popularity]
	FROM [Book]
	WHERE [Title] IN (SELECT [Title] FROM [Book] WHERE [Amount] > 1)
GO