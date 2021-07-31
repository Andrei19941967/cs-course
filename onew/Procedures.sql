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
-- DeleteProduct
DROP PROCEDURE IF EXISTS [sp_DeleteProduct]
GO
CREATE OR ALTER PROCEDURE [sp_DeleteProduct](
    @p_id AS INT)
AS
BEGIN
    DELETE FROM [Product] WHERE [Id] = @p_id;
END
GO
--UpdateProduct
DROP PROCEDURE IF EXISTS [sp_UpdateProduct]
GO
CREATE OR ALTER PROCEDURE [sp_UpdateProduct](
    @p_id AS INT,
    @p_name AS VARCHAR(300),
    @p_price AS SMALLMONEY)
AS
BEGIN
    UPDATE [Product] SET [Name] = @p_name, [Price] = @p_price WHERE [Id] = @p_id;
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
--DeleteOrder
GO
DROP PROCEDURE IF EXISTS [sp_DeleteOrder]
GO
CREATE OR ALTER PROCEDURE [sp_DeleteOrder](
    @p_id AS INT
)
AS
BEGIN
    DELETE FROM [Order] WHERE [Id] = @p_id;
END
GO
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
--DeleteOrderLines
DROP PROCEDURE IF EXISTS [sp_DeleteOrderLines]
GO
CREATE OR ALTER PROCEDURE [sp_DeleteOrderLines](
    @p_orderId AS INT
)
AS
BEGIN
    DELETE FROM [OrderLine] WHERE [OrderId] = @p_orderId;
END

--