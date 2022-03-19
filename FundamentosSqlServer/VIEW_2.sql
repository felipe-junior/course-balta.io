-- SELECT * FROM [vwCareers]

CREATE OR ALTER VIEW vwCareers AS
    SELECT
        [Career].[Id],
        [Career].[Title],
        Count([CourseId]) AS total
    FROM
        [CareerItem]
        INNER JOIN [Career] ON  [CareerItem].[CareerId] = [Career].[Id]
        INNER JOIN [Course] ON  [CareerItem].[CourseId] = [Course].[Id] 
    GROUP BY
        [Career].Id,
        [Career].[Title]
