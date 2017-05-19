USE modul_2
GO

SELECT tAuthors.aName
FROM tAuthors
WHERE aName LIKE 'R%';

SELECT tAuthors.aName, tAuthors.aId
FROM tAuthors
WHERE tAuthors.aAddress IS NULL;

SELECT tBooks.bAmount, tAuthors.aName
FROM tBooks, tAuthors
WHERE tAuthors.aId = tBooks.bAuthor;

SELECT * 
FROM tAuthors, tBooks
WHERE tAuthors.aId = tBooks.bAuthor 
AND tBooks.bAmount = 100;

SELECT tAuthors.aName, tAuthors.aAddress
FROM tAuthors, tBooks
WHERE tBooks.bAuthor = tAuthors.aId
AND tBooks.bOrdered < tBooks.bAmount;

SELECT *
FROM tAuthors, tBooks
WHERE tAuthors.aId = tBooks.bAmount
AND tBooks.bAmount = 0
OR tBooks.bAmount IS NOT NULL
ORDER BY tAuthors.aName;

SELECT tAuthors.aId, tAuthors.aName, tBooks.bOrdered
FROM tAuthors, tBooks
WHERE tAuthors.aId = tBooks.bAuthor
ORDER BY tAuthors.aId;

/*ADD SOME BOOKS AND DO THE LAST QUERY*/