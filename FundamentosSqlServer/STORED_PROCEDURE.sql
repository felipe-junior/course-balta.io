CREATE OR ALTER PROCEDURE [spListCourse] AS
    BEGIN TRANSACTION
        DECLARE @CategoryId INT
        -- SET @CategoryId = 9
        SET @CategoryId = (SELECT TOP 1 [Id] FROM [Curso] WHERE [Nome] = 'Backend')
        SELECT [Nome] FROM [Curso] WHERE [CategoriaId] = @CategoryId 
    COMMIT
DROP PROCEDURE [spListCourse]
GO

--Com parametros
CREATE OR ALTER PROCEDURE [spListCourse2] 
    @CategoryName NVARCHAR(60)
    AS
    BEGIN TRANSACTION
        DECLARE @CategoryId INT
        -- SET @CategoryId = 9
        SET @CategoryId = ( SELECT TOP 1 [Id] FROM [Curso] WHERE [Nome] = @CategoryName )
    COMMIT

DROP PROCEDURE [spListCourse2]


EXEC [spListCourse]
EXEC [spListCourse2] 'Backend'