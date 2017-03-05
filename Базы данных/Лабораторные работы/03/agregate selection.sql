USE [library]
GO

SELECT MIN([Amount]) AS 'Minimum'
	  ,MAX([Amount]) AS 'Maximum'
	  ,AVG([Pages]) AS 'Average of pages'
	FROM [dbo].[Book]
GO


USE [library]
GO

SELECT COUNT(*) AS 'Number'
	FROM [dbo].[Section]
	WHERE [Location] LIKE 'А1%'
GO


USE [library]
GO

SELECT SUM([Amount]) AS 'Number'
	FROM [dbo].[Book]
	WHERE [Language] = 'en_GB'
GO