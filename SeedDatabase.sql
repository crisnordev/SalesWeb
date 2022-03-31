USE [SalesWeb]
GO
INSERT INTO [Customer] VALUES ('eff07c46-7875-45b1-8996-8a59c389e742','Cristiano', 'email@email.com', 12345678901, 01/01/2000)
INSERT INTO [Customer] VALUES ('37d922e1-55dd-4497-9210-d9be97a9aacd','Paulo Silva', 'email@email.com', 12345678901, 01/01/2000)
INSERT INTO [Customer] VALUES ('6440cdcd-6c82-4acc-ac96-9535c5300718','José Santos', 'email@email.com', 12345678901, 01/01/2000)

USE [SalesWeb]
GO
INSERT INTO [Product] VALUES ('6aa66d6f-a740-4132-b0ea-6c2867f8209e','Copo Descartável', 3.99)
INSERT INTO [Product] VALUES ('c9523f77-bf4f-46c2-9092-5b4a6a926f89','Sacola Plástica', 2.98)
INSERT INTO [Product] VALUES ('6769de2f-31ff-468d-8dba-deadb8d9f8ce','Saco de Lixo', 32.50)

USE [SalesWeb]
GO
INSERT INTO [Seller] VALUES ('ecef27cd-8849-4296-a359-a85d9a1aa82e','Cristiano', 'email@email.com', 12345678901, 123456, 01/01/2000)
INSERT INTO [Seller] VALUES ('c3a9918f-33b0-4115-8db9-b44022984642','Paulo Silva', 'email@email.com', 12345678901, 123456, 01/01/2000)
INSERT INTO [Seller] VALUES ('468510b3-6c7f-43fa-9447-6fb2eac2d9ee','José Santos', 'email@email.com', 12345678901, 123456, 01/01/2000)

USE [SalesWeb]
GO
INSERT INTO [Sale] VALUES ('6fa7455c-e020-4d13-9bb4-22636c75e786', 'ecef27cd-8849-4296-a359-a85d9a1aa82e', 'eff07c46-7875-45b1-8996-8a59c389e742', 3.99)
INSERT INTO [Sale] VALUES ('58894be1-93ec-4f0e-8cf8-a9fcdcc4ce67', 'c3a9918f-33b0-4115-8db9-b44022984642', '37d922e1-55dd-4497-9210-d9be97a9aacd', 2.98)
INSERT INTO [Sale] VALUES ('0d17eb4b-7cca-4fc6-9e0d-647cdf4f02ae', '468510b3-6c7f-43fa-9447-6fb2eac2d9ee', '6440cdcd-6c82-4acc-ac96-9535c5300718', 65.00)

USE [SalesWeb]
GO
INSERT INTO [SoldProduct] VALUES ('53cfea94-17ac-42ff-a277-49fd0fc9c463', '6aa66d6f-a740-4132-b0ea-6c2867f8209e', 'Copo Descartável', 1, 3.99)
INSERT INTO [SoldProduct] VALUES ('f0824f0a-0c25-43bd-ba28-93c277f7b721', 'c9523f77-bf4f-46c2-9092-5b4a6a926f89', 'Sacola Plástica', 1, 2.98)
INSERT INTO [SoldProduct] VALUES ('c80354e7-c4f7-4554-85c4-7665698d2ca3', '6769de2f-31ff-468d-8dba-deadb8d9f8ce', 'Saco de Lixo', 2, 32.50)

USE [SalesWeb]
GO
INSERT INTO [SaleSoldProduct] VALUES ('6fa7455c-e020-4d13-9bb4-22636c75e786', '53cfea94-17ac-42ff-a277-49fd0fc9c463')
INSERT INTO [SaleSoldProduct] VALUES ('58894be1-93ec-4f0e-8cf8-a9fcdcc4ce67', 'f0824f0a-0c25-43bd-ba28-93c277f7b721')
INSERT INTO [SaleSoldProduct] VALUES ('0d17eb4b-7cca-4fc6-9e0d-647cdf4f02ae', 'c80354e7-c4f7-4554-85c4-7665698d2ca3')



-- USE [SalesWeb]
-- GO
-- INSERT INTO [SaleProduct] VALUES ('6aa66d6f-a740-4132-b0ea-6c2867f8209e', '6fa7455c-e020-4d13-9bb4-22636c75e786')
-- INSERT INTO [SaleProduct] VALUES ('c9523f77-bf4f-46c2-9092-5b4a6a926f89', '58894be1-93ec-4f0e-8cf8-a9fcdcc4ce67')
-- INSERT INTO [SaleProduct] VALUES ('6769de2f-31ff-468d-8dba-deadb8d9f8ce', '0d17eb4b-7cca-4fc6-9e0d-647cdf4f02ae')



DROP DATABASE [SalesWeb]