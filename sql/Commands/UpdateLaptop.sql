USE EMarketDb;

SET IMPLICIT_TRANSACTIONS OFF;
SET NOCOUNT ON;

DECLARE @Brand varchar(4000) = 'qUpdatedBrand';
DECLARE @Price decimal = 236;
DECLARE @Series varchar(4000) = 'qUpdatedSeries';
DECLARE @Id UNIQUEIDENTIFIER = '4fc75962-9b5a-4e1f-9446-08db6365f83e';

UPDATE [Laptops] SET [Brand] = @Brand, [Price] = @Price, [Series] = @Series
OUTPUT 1
WHERE [Id] = @Id;