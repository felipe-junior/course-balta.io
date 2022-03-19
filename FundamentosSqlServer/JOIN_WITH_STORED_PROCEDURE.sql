CREATE OR ALTER PROCEDURE sp_ProgressCourseStudent 
    @StudentId UNIQUEIDENTIFIER
AS 
    SELECT 
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

EXEC [sp_ProgressCourseStudent] '79b82071-80a8-4e78-a79c-92c8cd1fd05'