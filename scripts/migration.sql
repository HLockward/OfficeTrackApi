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
CREATE TABLE [EquipmentTypes] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_EquipmentTypes] PRIMARY KEY ([Id])
);

CREATE TABLE [MaintenanceTasks] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(200) NOT NULL,
    CONSTRAINT [PK_MaintenanceTasks] PRIMARY KEY ([Id])
);

CREATE TABLE [Equipment] (
    [Id] int NOT NULL IDENTITY,
    [Brand] nvarchar(100) NOT NULL,
    [Model] nvarchar(100) NOT NULL,
    [PurchaseDate] datetime2 NOT NULL,
    [SerialNumber] nvarchar(100) NULL,
    [EquipmentTypeId] int NOT NULL,
    CONSTRAINT [PK_Equipment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Equipment_EquipmentTypes_EquipmentTypeId] FOREIGN KEY ([EquipmentTypeId]) REFERENCES [EquipmentTypes] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [EquipmentMaintenances] (
    [EquipmentId] int NOT NULL,
    [MaintenanceTaskId] int NOT NULL,
    CONSTRAINT [PK_EquipmentMaintenances] PRIMARY KEY ([EquipmentId], [MaintenanceTaskId]),
    CONSTRAINT [FK_EquipmentMaintenances_Equipment_EquipmentId] FOREIGN KEY ([EquipmentId]) REFERENCES [Equipment] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_EquipmentMaintenances_MaintenanceTasks_MaintenanceTaskId] FOREIGN KEY ([MaintenanceTaskId]) REFERENCES [MaintenanceTasks] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Equipment_EquipmentTypeId] ON [Equipment] ([EquipmentTypeId]);

CREATE INDEX [IX_EquipmentMaintenances_MaintenanceTaskId] ON [EquipmentMaintenances] ([MaintenanceTaskId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250510203900_InitialCreate', N'9.0.4');

COMMIT;
GO

