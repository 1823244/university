USE [library]
GO

SELECT [ID]
      ,[Title]
      ,[Amount]
	FROM [dbo].[Book]
	WHERE [Popularity] BETWEEN 5 AND 10 AND [Language] = 'ru_RU'
	ORDER BY [Year]
GO


USE [library]
GO

SELECT [ID]
      ,[FirstName]
      ,[MiddleName]
      ,[LastName]
	FROM [dbo].[Author]
	WHERE [MiddleName] LIKE '%вич'
GO


USE [library]
GO

SELECT [ID]
      ,[Title]
      ,[Location]
	FROM [dbo].[Section]
	WHERE [Location] LIKE '_[10]0[10]'
GO