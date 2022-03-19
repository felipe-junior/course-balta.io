SELECT * FROM [Course]
SELECT * FROM [Student]
SELECT * FROM [StudentCourse]


DECLARE @StudentId UNIQUEIDENTIFIER = '79b82071-80a8-4e78-a79c-92c8cd1fd052'
SELECT 
    [Student].[Id],
    [Student].[Name],
    [Course].[Title],
    [StudentCourse].[Progress],
    [StudentCourse].[LastUpdateDate]
FROM 
    [StudentCourse]
    INNER JOIN  [Student] ON [Student].[Id] = [StudentCourse].[StudentId]
    INNER JOIn [Course] ON [Course].[Id] = [StudentCourse].[CourseId]
WHERE 
    [StudentCourse].[StudentId] = @StudentId
    AND [StudentCourse].[Progress] < 100
ORDER BY [LastUpdateDate]

INSERT INTO
    [Student]
VALUES (
    '79b82071-80a8-4e78-a79c-92c8cd1fd052',
    'André Baltieri',
    'hello@balta.io',
    '12345678901',
    '12345678',
    NULL,
    GETDATE()
)

INSERT INTO [StudentCourse] (
    [StudentId],
    [CourseId],
    [Progress],
    [Favorite],
    [StartDate],
    [LastUpdateDate]
) VALUES (
     '79b82071-80a8-4e78-a79c-92c8cd1fd052','5d8cf396-e717-9a02-2443-021b00000000', 60, 1, GETDATE(), GETDATE()
)
