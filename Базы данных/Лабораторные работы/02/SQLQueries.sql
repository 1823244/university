CREATE TABLE [Section] (
	[ID] tinyint NOT NULL IDENTITY (1, 1) PRIMARY KEY,
	[Title] nvarchar(50) NOT NULL CHECK ([Title] <> ''),
	[Location] nvarchar(100) NOT NULL CHECK ([Location] <> '')
)
GO

CREATE TABLE [Reader] (
	[ID] int NOT NULL IDENTITY (1, 1) PRIMARY KEY,
	[FirstName] nvarchar(50) NOT NULL CHECK ([FirstName] <> ''),
	[MiddleName] nvarchar(100),
	[LastName] nvarchar(50) NOT NULL CHECK ([LastName] <> ''),
	[Birth] datetime,
	[Phone] bigint NOT NULL CHECK ([Phone] > 0),
	[Passport] int,
	[Group] nchar(5) NOT NULL CHECK ([Group] <> '')
)
GO

CREATE TABLE [SectionReader] (
	[ReaderID] int NOT NULL	REFERENCES [Reader]
							ON DELETE CASCADE
							ON UPDATE CASCADE,
	[SectionID] tinyint NOT NULL	REFERENCES [Section]
									ON DELETE CASCADE
									ON UPDATE CASCADE, 
	PRIMARY KEY ([SectionID], [ReaderID])
)
GO

CREATE TABLE [Subject] (
	[ID] smallint NOT NULL IDENTITY (1, 1) PRIMARY KEY,
	[Title] nvarchar(50) NOT NULL CHECK ([Title] <> ''),
	[SectionID] tinyint	REFERENCES [Section]
						ON DELETE SET NULL
						ON UPDATE CASCADE			
)
GO

CREATE TABLE [Book] (
	[ID] int NOT NULL IDENTITY (1, 1) PRIMARY KEY,
	[Title] nvarchar(200) NOT NULL CHECK ([Title] <> ''),
	[Publisher] nvarchar(50) NOT NULL CHECK ([Publisher] <> ''),
	[Year] smallint NOT NULL,
	[Amount] tinyint NOT NULL,
	[Pages] smallint NOT NULL CHECK ([Pages] > 0),
	[ISBN] bigint NOT NULL CHECK ([ISBN] > 0),
	[Language] nchar(5) NOT NULL CHECK ([Language] <> ''),
	[Popularity] smallint NOT NULL CHECK ([Popularity] >= 0),
	[SubjectID] smallint	REFERENCES [Subject]
							ON DELETE SET NULL
							ON UPDATE CASCADE
)
GO

CREATE TABLE [Author] (
	[ID] int NOT NULL IDENTITY (1, 1) PRIMARY KEY,
	[FirstName] nvarchar(50) NOT NULL CHECK ([FirstName] <> ''),
	[MiddleName] nvarchar(100),
	[LastName] nvarchar(50) NOT NULL CHECK ([LastName] <> ''),
	[Birth] datetime
)
GO

CREATE TABLE [AuthorBook] (
	[AuthorID] int NOT NULL	REFERENCES [Author]
							ON DELETE CASCADE
							ON UPDATE CASCADE,
	[BookID] int NOT NULL	REFERENCES [Book]
							ON DELETE CASCADE
							ON UPDATE CASCADE, 
	PRIMARY KEY ([AuthorID], [BookID])
)
GO

CREATE TABLE [Instance] (
	[ID] int NOT NULL IDENTITY (1, 1) PRIMARY KEY,
	[ReaderID] int 	REFERENCES [Reader]
					ON DELETE SET NULL
					ON UPDATE CASCADE,
	[BookID] int NOT NULL	REFERENCES [Book]
							ON DELETE CASCADE
							ON UPDATE CASCADE,
	[Issue] datetime,
	[Return] datetime,
	[Available] bit NOT NULL, 
)
GO