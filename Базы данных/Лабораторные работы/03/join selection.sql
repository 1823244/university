USE [library]
GO

SELECT [BookID] as 'Book ID'
	  ,[LastName] as 'Reader last name'
	FROM [dbo].[Instance] INNER JOIN [dbo].[Reader]
	ON [ReaderID] = [dbo].[Reader].[ID]
GO

USE [library]
GO

SELECT [dbo].[Book].[Title] as 'Book title'
	  ,[dbo].[Subject].[Title] as 'Subject title'
	FROM [dbo].[Book] INNER JOIN [dbo].[Subject]
	ON [SubjectID] = [dbo].[Subject].[ID]
GO


USE [library]
GO

SELECT R.[LastName] as 'Reader last name',
	   A.[LastName] as 'Known author'
	FROM [dbo].[Reader] R INNER JOIN [dbo].[Instance] ON R.[ID] = [ReaderID]
						  INNER JOIN [dbo].[Book] B ON [BookID] = B.[ID]
						  INNER JOIN [dbo].[AuthorBook] AB ON B.[ID] = AB.BookID
						  INNER JOIN [dbo].Author A ON AB.[AuthorID] = A.[ID]
GO