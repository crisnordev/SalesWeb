USE [SalesWeb]
GO
INSERT INTO [Customer] VALUES ('eff07c46-7875-45b1-8996-8a59c389e742','Cristiano','Noronha', 'email@email.com', '123.456.789-01', 01/01/2000)
INSERT INTO [Customer] VALUES ('37d922e1-55dd-4497-9210-d9be97a9aacd','Paulo', 'Silva', 'email@email.com', '123.456.789-01', 01/01/2000)
INSERT INTO [Customer] VALUES ('6440cdcd-6c82-4acc-ac96-9535c5300718','José', 'Santos', 'email@email.com', '123.456.789-01', 01/01/2000)

USE [SalesWeb]
GO
INSERT INTO [Product] VALUES ('5ca72738-fc3c-4f56-aa81-125e6b30b8c0', 'Plastic Cup', 3.99)
INSERT INTO [Product] VALUES ('a717f72d-2d54-47ee-aaf3-70008fe84668', 'Plastic Bag', 2.98)
INSERT INTO [Product] VALUES ('a6045257-1280-4de7-b4f7-81000e93ba31', 'Garbage Bag', 32.50)

USE [SalesWeb]
GO
INSERT INTO [Seller] VALUES ('ecef27cd-8849-4296-a359-a85d9a1aa82e','Cristiano', 'Noronha', 'cris@email.com', '123.456.789-01', 123456, 01/01/2000)
INSERT INTO [Seller] VALUES ('c3a9918f-33b0-4115-8db9-b44022984642','Paulo', 'Silva', 'paulo@email.com', '123.456.789-01', 123456, 01/01/2000)
INSERT INTO [Seller] VALUES ('468510b3-6c7f-43fa-9447-6fb2eac2d9ee','José', 'Santos', 'jose@email.com', '123.456.789-01', 123456, 01/01/2000)

USE [SalesWeb]
GO
INSERT INTO [SoldProduct] VALUES ('53cfea94-17ac-42ff-a277-49fd0fc9c463', '5ca72738-fc3c-4f56-aa81-125e6b30b8c0', 'Plastic Cup', 1, 3.99)
INSERT INTO [SoldProduct] VALUES ('f0824f0a-0c25-43bd-ba28-93c277f7b721', 'a717f72d-2d54-47ee-aaf3-70008fe84668', 'Plastic Bag', 3, 2.98)
INSERT INTO [SoldProduct] VALUES ('c80354e7-c4f7-4554-85c4-7665698d2ca3', 'a6045257-1280-4de7-b4f7-81000e93ba31', 'Garbage Bag', 2, 32.50)

USE [SalesWeb]
GO
INSERT INTO [Sale] VALUES ('6fa7455c-e020-4d13-9bb4-22636c75e786', 'eff07c46-7875-45b1-8996-8a59c389e742', 'ecef27cd-8849-4296-a359-a85d9a1aa82e', 3.99)
INSERT INTO [Sale] VALUES ('58894be1-93ec-4f0e-8cf8-a9fcdcc4ce67', '37d922e1-55dd-4497-9210-d9be97a9aacd', 'c3a9918f-33b0-4115-8db9-b44022984642', 8.94)
INSERT INTO [Sale] VALUES ('0d17eb4b-7cca-4fc6-9e0d-647cdf4f02ae', '6440cdcd-6c82-4acc-ac96-9535c5300718', '468510b3-6c7f-43fa-9447-6fb2eac2d9ee', 65.00)

USE [SalesWeb]
GO
INSERT INTO [SaleSoldProduct] VALUES ('6fa7455c-e020-4d13-9bb4-22636c75e786', '53cfea94-17ac-42ff-a277-49fd0fc9c463')
INSERT INTO [SaleSoldProduct] VALUES ('58894be1-93ec-4f0e-8cf8-a9fcdcc4ce67', 'f0824f0a-0c25-43bd-ba28-93c277f7b721')
INSERT INTO [SaleSoldProduct] VALUES ('0d17eb4b-7cca-4fc6-9e0d-647cdf4f02ae', 'c80354e7-c4f7-4554-85c4-7665698d2ca3')