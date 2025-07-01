IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250701072635_Init'
)
BEGIN
    CREATE TABLE [Movie] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [Year] int NOT NULL,
        [Genre] nvarchar(max) NOT NULL,
        [Duration] int NOT NULL,
        CONSTRAINT [PK_Movie] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250701072635_Init'
)
BEGIN
    CREATE TABLE [MovieDetails] (
        [Id] int NOT NULL IDENTITY,
        [Synopsis] nvarchar(max) NOT NULL,
        [Language] nvarchar(max) NOT NULL,
        [Budget] int NOT NULL,
        [MovieId] int NOT NULL,
        CONSTRAINT [PK_MovieDetails] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_MovieDetails_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250701072635_Init'
)
BEGIN
    CREATE UNIQUE INDEX [IX_MovieDetails_MovieId] ON [MovieDetails] ([MovieId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250701072635_Init'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250701072635_Init', N'9.0.0');
END;

COMMIT;
GO