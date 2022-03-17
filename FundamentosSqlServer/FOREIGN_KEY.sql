
DROP TABLE [Aluno]
DROP TABLE [Curso]
DROP TABLE [Categoria]
DROP TABLE [ProgressoCurso]

CREATE TABLE [Aluno] (
    [Id] INT NOT NULL IDENTITY(1,1), -- AutoIncrement, começa do 1 e vai somando 1
    -- [Id] INT UNIQUEIDENTIFIER IDENTITY(1,1), -- Gera um Guid
    [Nome] NVARCHAR(80) NOT NULL,
    [Email] NVARCHAR(180) NOT NULL UNIQUE,
    [Nascimento] DATETIME DEFAULT(GETDATE()),
    [Active] BIT DEFAULT(0),

    CONSTRAINT [PK_Aluno_Id] PRIMARY KEY([Id]),
    CONSTRAINT [PK_Aluno_Email] UNIQUE ([Email])
)
GO

CREATE INDEX [IX_Aluno_Email] ON [Aluno] ([Email])
-- DROP INDEX [IX_Aluno_Email] ON [Aluno]

CREATE TABLE [Curso] (
    [Id] INT NOT NULL,
    [Nome] NVARCHAR(80) NOT NULL,
    [CategoriaId] INT NOT NULL,
    
    CONSTRAINT [PK_Curso_Id] PRIMARY KEY ([Id]),
)
GO

CREATE TABLE [ProgressoCurso] (
    [AlunoId] INT NOT NULL,
    [CursoId] INT NOT NULL,
    [Progresso] INT NOT NULL,
    [UltimaAtualizacao] DATETIME NOT NULL DEFAULT (GETDATE()),

    CONSTRAINT PK_ProgressoCurso PRIMARY KEY ([AlunoId], [CursoId])
)
GO

CREATE TABLE [Categoria] (
    [Id] INT NOT NULL,
    [Nome] NVARCHAR(80) NOT NULL,
    CONSTRAINT PK_Categoria_Id PRIMARY KEY ([Id])
)
GO

ALTER TABLE [ProgressoCurso] 
    ADD CONSTRAINT FK_CursoId FOREIGN KEY([CursoId]) 
        REFERENCES [Curso] ([Id]),
    CONSTRAINT [FK_Curso_AlunoId] FOREIGN KEY ([AlunoId]) 
    REFERENCES [Aluno] ([Id])

ALTER TABLE [Curso] ADD 
    CONSTRAINT [FK_Curso_CategoriaId] FOREIGN KEY ([CategoriaId]) 
        REFERENCES [Categoria] ([Id])