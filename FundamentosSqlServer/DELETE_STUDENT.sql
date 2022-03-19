--APAGAR UM ESTUDANTE

CREATE OR ALTER PROCEDURE sp_DeleteStudent(
    @StudentId UNIQUEIDENTIFIER 
)
AS
    BEGIN TRANSACTION
        DELETE FROM [StudentCourse] WHERE [StudentId] = @StudentId
        DELETE FROM [Student] WHERE [Id] = @StudentId
    COMMIT

EXEC [sp_DeleteStudent] '79b82071-80a8-4e78-a79c-92c8cd1fd052'