use EMarketDb;

DECLARE @Id UNIQUEIDENTIFIER = 'bd0cbedf-a5b8-4951-943b-08db6365f83e';

SELECT TOP(1) l.Brand, l.Created, l.Currency, l.Price, l.Series 
FROM Laptops as l
WHERE l.Id = @Id