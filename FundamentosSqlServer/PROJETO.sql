
CREATE DATABASE [Felipe]
GO

USE [Felipe]
GO
CREATE TABLE [Student]
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR(120) NOT NULL,
    [Email] NVARCHAR(120) NOT NULL,
    [Document] NVARCHAR(20) NULL,
    [Phone] NVARCHAR(20) NULL,
    [BirthDate] DATETIME NULl,
    [CreateDate] DATETIME NOT NULL DEFAULT ((GETUTCDATE())),
    CONSTRAINT [PK_Student] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_StudentEmail] ON [Student] ([Email])

CREATE TABLE [Author](
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR (80) NOT NULL,
    [Title] NVARCHAR (80) NOT NULL,
    [Image] NVARCHAR (1024) NOT NULL,
    [Bio] NVARCHAR (2000) NOT NULL,
    [Url] NVARCHAR (450) NULL,
    [Email] NVARCHAR (160) NOT NULL,
    [Type] TINYINT NOT NULL,

    CONSTRAINT [PK_Author] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Career] (
    
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Title] NVARCHAR(160) NOT NULL,
    [Summary] NVARCHAR (2000) NOT NULL,
    [Url] NVARCHAR(2000) NOT NULL,
    [DurationInMinutes] INT NOT NULL,
    [Active] BIT NOT NULL,
    [Featured] BIT NOT NULL,
    [Tags] NVARCHAR (160) NOT NULL,
    
    CONSTRAINT [PK_Career_Id] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Category] (
    [Id] UNIQUEIDENTIFIER NOT NULl,
    [Title] NVARCHAR(160) NOT NULL,
    [Url] NVARCHAR(1024) NOT NULL,
    [Summary] NVARCHAR (1024) NOT NULL,
    [Order] INT NOT NULl,
    [Description] TEXT NOT NULL,
    [Featured] BIT NOT NULL,

    CONSTRAINT [PK_Category_Id] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Course]
(
    [Id] uniqueidentifier NOT NULL,
    [Tag] NVARCHAR(20) NOT NULL,
    [Title] NVARCHAR(160) NOT NULL,
    [Summary] NVARCHAR(2000) NOT NULL,
    [Url] NVARCHAR(1024) NOT NULL,
    [Level] TINYINT NOT NULL,
    [DurationInMinutes] INT NOT NULL,
    [CreateDate] DATETIME NOT NULL,
    [LastUpdateDate] DATETIME NOT NULL,
    [Active] BIT NOT NULL,
    [Free] BIT NOT NULL,
    [Featured] BIT NOT NULL,
    [AuthorId] uniqueidentifier NOT NULL,
    [CategoryId] uniqueidentifier NOT NULL,
    [Tags] NVARCHAR(160) NOT NULL,
    CONSTRAINT [PK_Course] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Course_Author_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [Author] ([Id]) ON DELETE NO ACTION , -- ON DELETE CASCADE
    CONSTRAINT [FK_Course_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id])
);
GO

CREATE TABLE [CareerItem]
(
    [CareerId] uniqueidentifier NOT NULL,
    [CourseId] uniqueidentifier NOT NULL,
    [Title] NVARCHAR(160) NOT NULL,
    [Description] TEXT NOT NULL,
    [Order] TINYINT NOT NULL,
    
    CONSTRAINT [PK_CareerItem] PRIMARY KEY ([CourseId], [CareerId]),
    CONSTRAINT [FK_CareerItem_Career_CareerId] FOREIGN KEY ([CareerId]) REFERENCES [Career] ([Id]),
    CONSTRAINT [FK_CareerItem_Course_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Course] ([Id])
);
GO

CREATE TABLE [StudentCourse]
(
    [CourseId] uniqueidentifier NOT NULL,
    [StudentId] uniqueidentifier NOT NULL,
    [Progress] TINYINT NOT NULL, --0 até 255
    [Favorite] BIT NOT NULL,
    [StartDate] DATETIME NOT NULL,
    [LastUpdateDate] DATETIME NULL DEFAULT(GETDATE()),
    CONSTRAINT [PK_StudentCourse] PRIMARY KEY ([CourseId], [StudentId]),
    CONSTRAINT [FK_StudentCourse_Course_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Course] ([Id]),
    CONSTRAINT [FK_StudentCourse_Student_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Student] ([Id])
);
GO