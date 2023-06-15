use EMarketDb;

DECLARE @Offset int = 0;
DECLARE @Fetch int = 22;

SELECT *
FROM [Laptops] AS [l]
ORDER BY [l].[Price]
OFFSET @Offset ROWS FETCH NEXT @Fetch ROWS ONLY