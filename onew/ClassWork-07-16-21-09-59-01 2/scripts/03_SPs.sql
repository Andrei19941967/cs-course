-- AddProduct
DROP PROCEDURE IF EXISTS [sp_AddProduct]
GO
CREATE OR ALTER PROCEDURE [sp_AddProduct](
	@p_name AS VARCHAR(300),
	@p_price AS SMALLMONEY,
	@p_id AS INT OUTPUT)
AS
BEGIN
	INSERT INTO [Product] ([Name], [Price]) VALUES (@p_name, @p_price);
	SELECT @p_id = SCOPE_IDENTITY();
END
GO
-- AddOrder
DROP PROCEDURE IF EXISTS [sp_AddOrder]
GO
CREATE OR ALTER PROCEDURE [sp_AddOrder](
	@p_customerId AS INT,
	@p_orderDate AS DATETIMEOFFSET,
	@p_discount AS FLOAT = NULL,
	@p_id AS INT OUTPUT)
AS
BEGIN
	INSERT INTO [Order] ([CustomerId], [OrderDate], [Discount])
		VALUES (@p_customerId, @p_orderDate, @p_discount)
	SELECT @p_id = SCOPE_IDENTITY();
END
-- AddOrderLines
GO
DROP PROCEDURE IF EXISTS [sp_AddOrderLine]
GO
CREATE OR ALTER PROCEDURE [sp_AddOrderLine](
	@p_orderId AS INT,
	@p_productId AS INT,
	@p_count AS INT)
AS
BEGIN
	INSERT INTO [OrderLine] ([OrderId], [ProductId], [Count])
		VALUES (@p_orderId, @p_productId, @p_count)
END
GO