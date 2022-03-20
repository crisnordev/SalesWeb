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

CREATE TABLE [Customer] (
    [Id] uniqueidentifier NOT NULL,
    [Name] NVARCHAR(160) NOT NULL,
    [Email] VARCHAR(160) NOT NULL,
    [DocumentId] VARCHAR(11) NOT NULL,
    [BirthDate] SMALLDATETIME NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT [PK_Customer] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Product] (
    [Id] uniqueidentifier NOT NULL,
    [Name] NVARCHAR(160) NOT NULL,
    [Price] DECIMAL(18,2) NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Seller] (
    [Id] uniqueidentifier NOT NULL,
    [Name] NVARCHAR(160) NOT NULL,
    [Email] VARCHAR(160) NOT NULL,
    [DocumentId] VARCHAR(11) NOT NULL,
    [Password] VARCHAR(255) NOT NULL,
    [BirthDate] SMALLDATETIME NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT [PK_Seller] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SoldProduct] (
    [Id] uniqueidentifier NOT NULL,
    [Name] VARCHAR(160) NOT NULL,
    [Quantity] INT NOT NULL,
    [Price] DECIMAL(18,2) NOT NULL,
    CONSTRAINT [PK_SoldProduct] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Sale] (
    [Id] uniqueidentifier NOT NULL,
    [SellerId] uniqueidentifier NOT NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    [TotalAmount] DECIMAL(18,2) NOT NULL,
    CONSTRAINT [PK_Sale] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Sale_CustomerID] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Sale_SellerID] FOREIGN KEY ([SellerId]) REFERENCES [Seller] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [SaleSoldProduct] (
    [SaleId] uniqueidentifier NOT NULL,
    [SoldProductId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_SaleSoldProduct] PRIMARY KEY ([SaleId], [SoldProductId]),
    CONSTRAINT [FK_SaleSoldProduct_Sale_SoldProductId] FOREIGN KEY ([SoldProductId]) REFERENCES [SoldProduct] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SaleSoldProduct_SoldProduct_SaleId] FOREIGN KEY ([SaleId]) REFERENCES [Sale] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Sale_CustomerId] ON [Sale] ([CustomerId]);
GO

CREATE INDEX [IX_Sale_SellerId] ON [Sale] ([SellerId]);
GO

CREATE INDEX [IX_SaleSoldProduct_SoldProductId] ON [SaleSoldProduct] ([SoldProductId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220320173532_FirstMigration', N'6.0.3');
GO

COMMIT;
GO

