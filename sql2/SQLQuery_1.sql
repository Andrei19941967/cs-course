

-- Добавить разбивку по годам и сортировку по имени, а зачем по году
WITH CustomerDiscountTotal([Id], [Discount], [Year], [Total], [Name])
AS (
 SELECT
    O.Customerid,
    O.Discount,
    YEAR(O.OrderDate),
    SUM(OL.Count * P.Price) * (1 - ISNULL(O.Discount, 0)),
    C.Name
  FROM [Order] O
  JOIN [orderline] OL
    ON OL.OrderId = O.Id
  JOIN [Product] P
    ON OL.ProductId = P.Id
  JOIN Customer AS C
    ON C.Id = O.CustomerId
 GROUP BY YEAR(O.OrderDate), O.CustomerId, O.Discount, C.Name
),
CustomerTotal([Id], [Year], [Total], [Name]) AS (
  SELECT
    Id,
    [Year],
    SUM(Total),
    [Name]
  FROM CustomerDiscountTotal
 GROUP BY [Year], Id, [Name]
)


SELECT CT.Id, CT.Name, CT.[Year], CT.Total FROM CustomerTotal AS CT WHERE CT.Id IN(
    SELECT CT2.Total FROM CustomerTotal AS CT2 WHERE  CT2.Total IN(
        SELECT MAX(Total) FROM CustomerTotal WHERE [Year] = CT2.[Year]
    )
)



