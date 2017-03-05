USE [library]
GO

SELECT [LastName]
     , [Birth]
	FROM [dbo].[Author]
UNION
SELECT [LastName]
      ,[Birth]
	FROM [dbo].[Reader]
	ORDER BY [Birth]
GO

USE [library]
GO

SELECT [FirstName] + ' ' + [MiddleName] + ' ' + [LastName] as 'Full name'
	FROM [dbo].[Author]
UNION
SELECT [FirstName] + ' ' + [MiddleName] + ' ' + [LastName]
	FROM [dbo].[Reader]
GO