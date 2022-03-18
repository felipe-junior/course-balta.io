SELECT TOP (10) [Id]
      ,[Nome]
  FROM [Curso].[dbo].[Curso]

SELECT 
    [Nome] 
FROM 
    [Curso] 
ORDER BY
    [Nome] DESC
    

BEGIN TRANSACTION
    UPDATE 
        [Curso]
    SET
        [Nome] = 'Test'
    WHERE [id] = 1
--ROLLBACK
COMMIT

BEGIN TRANSACTION
    DELETE FROM
        [Curso]
    WHERE [Id] = 4
COMMIT

BEGIN TRANSACTION 
    DELETE FROM 
        [Categoria]
    WHERE 
        [Id] = 1
COMMIT
