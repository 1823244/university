USE library
GO
CREATE VIEW FreeBooks
AS 
SELECT B.Title as "Book title", B.Amount as "Amount", S.Title as "Section"
FROM Book B, Subject Sbj, Section S
WHERE B.SubjectID = Sbj.ID AND Sbj.SectionID = S.ID