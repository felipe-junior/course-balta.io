USE [Curso]

DROP TABLE [Aluno]

CREATE TABLE [ALUNO] (
    -- atributo | tipo 
    [Id] INT NOT NULL,
    [Nome] NVARCHAR(80) NOT NULL, -- CHAR, VARCHAR, NVARCHAR
    [Email] NVARCHAR(180) NOT NULL UNIQUE,
    [Nascimento] DATETIME DEFAULT(GETDATE()),
    [Active] BIT DEFAULT(0),

    CONSTRAINT [PK_Aluno_Id] PRIMARY KEY([Id]),
    CONSTRAINT [PK_Aluno_Email] UNIQUE ([Email])
    -- PRIMARY KEY([Id], [Email]) Chaves primarias compostas
)
GO -- Vai executar o comando no database e espera concluir

ALTER TABLE [ALuno] ALTER COLUMN [Active] BIT NOT NULL 

ALTER TABLE [Aluno] DROP CONSTRAINT [PK_Aluno_Id]
    
ALTER TABLE [Aluno] ADD PRIMARY KEY([Id])