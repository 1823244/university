CREATE TABLE [Book] (
  [ID] int NOT NULL IDENTITY (1, 1),
  [Title] nvarchar(200) NOT NULL,
  [Publisher] nvarchar(50) NOT NULL,
  [Year] smallint NOT NULL,
  [Amount] tinyint NOT NULL,
  [Pages] smallint NOT NULL,
  [ISBN] bigint NOT NULL,
  [Language] nchar(5) NOT NULL,
  [Popularity] smallint NOT NULL,
  [SubjectID] smallint NOT NULL, 
 PRIMARY KEY ([ID])
) ON [PRIMARY]
GO

CREATE TABLE [Author] (
  [ID] int NOT NULL IDENTITY (1, 1),
  [FirstName] nvarchar(50) NOT NULL,
  [MiddleName] nvarchar(100),
  [LastName] nvarchar(50) NOT NULL,
  [Birth] datetime, 
 PRIMARY KEY ([ID])
) ON [PRIMARY]
GO

CREATE TABLE [AuthorBook] (
  [AuthorID] int NOT NULL,
  [BookID] int NOT NULL, 
 PRIMARY KEY ([AuthorID], [BookID])
) ON [PRIMARY]
GO

CREATE TABLE [Instance] (
  [ID] int NOT NULL IDENTITY (1, 1),
  [ReaderID] int,
  [BookID] int NOT NULL,
  [Issue] datetime,
  [Return] datetime,
  [Available] bit NOT NULL, 
 PRIMARY KEY ([ID])
) ON [PRIMARY]
GO

CREATE TABLE [Reader] (
  [ID] int NOT NULL IDENTITY (1, 1),
  [FirstName] nvarchar(50) NOT NULL,
  [MiddleName] nvarchar(100),
  [LastName] nvarchar(50) NOT NULL,
  [Birth] datetime,
  [Phone] bigint NOT NULL,
  [Passport] int,
  [Group] nchar(5) NOT NULL, 
 PRIMARY KEY ([ID])
) ON [PRIMARY]
GO

CREATE TABLE [SectionReader] (
  [ReaderID] int NOT NULL,
  [SectionID] tinyint NOT NULL, 
 PRIMARY KEY ([SectionID], [ReaderID])
) ON [PRIMARY]
GO

CREATE TABLE [Section] (
  [ID] tinyint NOT NULL IDENTITY (1, 1),
  [Title] nvarchar(50) NOT NULL,
  [Location] nvarchar(100) NOT NULL, 
 PRIMARY KEY ([ID])
) ON [PRIMARY]
GO

CREATE TABLE [Subject] (
  [ID] smallint NOT NULL IDENTITY (1, 1),
  [Title] nvarchar(50) NOT NULL,
  [SectionID] tinyint NOT NULL, 
 PRIMARY KEY ([ID])
) ON [PRIMARY]
GO

ALTER TABLE [AuthorBook] ADD FOREIGN KEY (BookID) REFERENCES [Book] ([ID]);

ALTER TABLE [AuthorBook] ADD FOREIGN KEY (AuthorID) REFERENCES [Author] ([ID]);

ALTER TABLE [Book] ADD FOREIGN KEY (SubjectID) REFERENCES [Subject] ([ID]);
				
ALTER TABLE [Instance] ADD FOREIGN KEY (ReaderID) REFERENCES [Reader] ([ID]);
				
ALTER TABLE [Instance] ADD FOREIGN KEY (BookID) REFERENCES [Book] ([ID]);
				
ALTER TABLE [SectionReader] ADD FOREIGN KEY (ReaderID) REFERENCES [Reader] ([ID]);
				
ALTER TABLE [SectionReader] ADD FOREIGN KEY (SectionID) REFERENCES [Section] ([ID]);
				
ALTER TABLE [Subject] ADD FOREIGN KEY (SectionID) REFERENCES [Section] ([ID]);