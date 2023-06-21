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
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510192656_Init')
BEGIN
    CREATE TABLE [Difficulties] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Difficulties] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510192656_Init')
BEGIN
    CREATE TABLE [Regions] (
        [Id] uniqueidentifier NOT NULL,
        [Code] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [RegionImageUrl] nvarchar(max) NULL,
        CONSTRAINT [PK_Regions] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510192656_Init')
BEGIN
    CREATE TABLE [Walks] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [LengthInKm] float NOT NULL,
        [WalkImageUrl] nvarchar(max) NULL,
        [DifficultyId] uniqueidentifier NOT NULL,
        [RegionId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Walks] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Walks_Difficulties_DifficultyId] FOREIGN KEY ([DifficultyId]) REFERENCES [Difficulties] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Walks_Regions_RegionId] FOREIGN KEY ([RegionId]) REFERENCES [Regions] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510192656_Init')
BEGIN
    CREATE INDEX [IX_Walks_DifficultyId] ON [Walks] ([DifficultyId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510192656_Init')
BEGIN
    CREATE INDEX [IX_Walks_RegionId] ON [Walks] ([RegionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510192656_Init')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230510192656_Init', N'7.0.7');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230619230218_AddImageTableToDb')
BEGIN
    EXEC(N'DELETE FROM [Difficulties]
    WHERE [Id] = ''54466f17-02af-48e7-8ed3-5a4a8bfacf6f'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230619230218_AddImageTableToDb')
BEGIN
    EXEC(N'DELETE FROM [Difficulties]
    WHERE [Id] = ''ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230619230218_AddImageTableToDb')
BEGIN
    EXEC(N'DELETE FROM [Difficulties]
    WHERE [Id] = ''f808ddcd-b5e5-4d80-b732-1ca523e48434'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230619230218_AddImageTableToDb')
BEGIN
    EXEC(N'DELETE FROM [Regions]
    WHERE [Id] = ''14ceba71-4b51-4777-9b17-46602cf66153'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230619230218_AddImageTableToDb')
BEGIN
    EXEC(N'DELETE FROM [Regions]
    WHERE [Id] = ''6884f7d7-ad1f-4101-8df3-7a6fa7387d81'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230619230218_AddImageTableToDb')
BEGIN
    EXEC(N'DELETE FROM [Regions]
    WHERE [Id] = ''906cb139-415a-4bbb-a174-1a1faf9fb1f6'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230619230218_AddImageTableToDb')
BEGIN
    EXEC(N'DELETE FROM [Regions]
    WHERE [Id] = ''cfa06ed2-bf65-4b65-93ed-c9d286ddb0de'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230619230218_AddImageTableToDb')
BEGIN
    EXEC(N'DELETE FROM [Regions]
    WHERE [Id] = ''f077a22e-4248-4bf6-b564-c7cf4e250263'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230619230218_AddImageTableToDb')
BEGIN
    EXEC(N'DELETE FROM [Regions]
    WHERE [Id] = ''f7248fc3-2585-4efb-8d1d-1c555f4087f6'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230619230218_AddImageTableToDb')
BEGIN
    CREATE TABLE [Images] (
        [Id] uniqueidentifier NOT NULL,
        [FileName] nvarchar(max) NOT NULL,
        [FileDescription] nvarchar(max) NULL,
        [FileExtension] nvarchar(max) NOT NULL,
        [FileSizeInBytes] bigint NOT NULL,
        [FilePath] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Images] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230619230218_AddImageTableToDb')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230619230218_AddImageTableToDb', N'7.0.7');
END;
GO

COMMIT;
GO

