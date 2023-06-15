Use EMarketDb;

SET IMPLICIT_TRANSACTIONS OFF;
SET NOCOUNT ON;

DECLARE @Id UNIQUEIDENTIFIER = 'd6d3c213-a4b3-4b72-6c84-08db6d8af818';
DECLARE @Brand VARCHAR(4000) = 'MyBrand';
DECLARE @Created DATETIME2 = '2023-06-15';
DECLARE @CreatedBy VARCHAR(4000) = null; 
DECLARE @Currency INT = 0;
DECLARE @LastModified DATETIME2 = null;
DECLARE @LastModifiedBy VARCHAR(4000) = null;
DECLARE @Price DECIMAL = 25;
DECLARE @Series VARCHAR(4000) = 'MySeries'

INSERT INTO [Laptops] ([Id], [Brand], [Created], [CreatedBy], [Currency], [LastModified], [LastModifiedBy], [Price], [Series])
VALUES (@Id, @Brand, @Created, @CreatedBy, @Currency, @LastModified, @LastModifiedBy, @Price, @Series);