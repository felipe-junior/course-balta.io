USE [Curso]

ALTER TABLE [Aluno]
    ADD [Document] NVARCHAR(11)

ALTER TABLE [Aluno]
    DROP COLUMN [Document] --Cuidado ao usar drop porque todo o dado é perdido
