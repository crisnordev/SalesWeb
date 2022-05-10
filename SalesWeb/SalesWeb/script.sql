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
    [CustomerId] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(60) NOT NULL,
    [LastName] nvarchar(120) NOT NULL,
    [CompleteName] nvarchar(180) NOT NULL,
    [Email] VARCHAR(160) NOT NULL,
    [EmailConfirmed] bit NOT NULL,
    [EmailVerificationCode] nvarchar(8) NULL,
    [EmailExpirationDate] datetime2 NULL,
    [CodeVerified] bit NULL,
    [DocumentIdentificationNumber] VARCHAR(14) NOT NULL,
    [BirthDate] SMALLDATETIME NOT NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY ([CustomerId])
);
GO

CREATE TABLE [Product] (
    [ProductId] uniqueidentifier NOT NULL,
    [ProductName] VARCHAR(160) NOT NULL,
    [Price] DECIMAL(18,2) NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([ProductId])
);
GO

CREATE TABLE [Seller] (
    [SellerId] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(60) NOT NULL,
    [LastName] nvarchar(120) NOT NULL,
    [CompleteName] nvarchar(180) NOT NULL,
    [Email] VARCHAR(160) NOT NULL,
    [EmailConfirmed] bit NOT NULL,
    [EmailVerificationCode] nvarchar(8) NULL,
    [EmailExpirationDate] datetime2 NULL,
    [CodeVerified] bit NULL,
    [DocumentIdentificationNumber] VARCHAR(14) NOT NULL,
    [Password] VARCHAR(255) NOT NULL,
    [BirthDate] SMALLDATETIME NOT NULL,
    CONSTRAINT [PK_Seller] PRIMARY KEY ([SellerId])
);
GO

CREATE TABLE [SoldProduct] (
    [SoldProductId] uniqueidentifier NOT NULL,
    [ProductId] VARCHAR(36) NOT NULL,
    [ProductName] VARCHAR(160) NOT NULL,
    [Quantity] INT NOT NULL,
    [Price] DECIMAL(18,2) NOT NULL,
    CONSTRAINT [PK_SoldProduct] PRIMARY KEY ([SoldProductId])
);
GO

CREATE TABLE [Sale] (
    [SaleId] uniqueidentifier NOT NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    [SellerId] uniqueidentifier NOT NULL,
    [TotalAmount] DECIMAL(18,2) NOT NULL,
    CONSTRAINT [PK_Sale] PRIMARY KEY ([SaleId]),
    CONSTRAINT [FK_Sale_CustomerID] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([CustomerId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Sale_SellerID] FOREIGN KEY ([SellerId]) REFERENCES [Seller] ([SellerId]) ON DELETE CASCADE
);
GO

CREATE TABLE [SaleSoldProduct] (
    [SaleId] uniqueidentifier NOT NULL,
    [SoldProductId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_SaleSoldProduct] PRIMARY KEY ([SaleId], [SoldProductId]),
    CONSTRAINT [FK_SaleSoldProduct_Sale_SoldProductId] FOREIGN KEY ([SoldProductId]) REFERENCES [SoldProduct] ([SoldProductId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SaleSoldProduct_SoldProduct_SaleId] FOREIGN KEY ([SaleId]) REFERENCES [Sale] ([SaleId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Sale_CustomerId] ON [Sale] ([CustomerId]);
GO

CREATE INDEX [IX_Sale_SellerId] ON [Sale] ([SellerId]);
GO

CREATE INDEX [IX_SaleSoldProduct_SoldProductId] ON [SaleSoldProduct] ([SoldProductId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220509054538_FirstMigration', N'6.0.4');
GO

COMMIT;
GO

