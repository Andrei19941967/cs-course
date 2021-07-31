-- drop foreign keys
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Order]') AND type in (N'U'))
ALTER TABLE [Order] DROP CONSTRAINT IF EXISTS [FK_Order_CustomerId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[OrderLine]') AND type in (N'U'))
ALTER TABLE [OrderLine] DROP CONSTRAINT IF EXISTS [FK_OrderLine_OrderId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[OrderLine]') AND type in (N'U'))
ALTER TABLE [OrderLine] DROP CONSTRAINT IF EXISTS [FK_OrderLine_ProductId]
GO

-- re-create tables
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Product]') AND type in (N'U'))
	DROP TABLE [Product]
GO
CREATE TABLE [Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] VARCHAR(300) NOT NULL,
	[Price] SMALLMONEY NOT NULL,
	CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED (Id ASC),
);
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Customer]') AND type in (N'U'))
	DROP TABLE [Customer]
GO
CREATE TABLE [Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] VARCHAR(50) NOT NULL,
	CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED (Id ASC)
);
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Order]') AND type in (N'U'))
	DROP TABLE [Order]
GO
CREATE TABLE [Order](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[CustomerId] INT NOT NULL,
	[OrderDate] DATETIMEOFFSET NOT NULL,
	[Discount] float,
	CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED (Id ASC)
);
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[OrderLine]') AND type in (N'U'))
	DROP TABLE [OrderLine]
GO
CREATE TABLE [OrderLine](
	[OrderId] INT NOT NULL,
	[ProductId] INT NOT NULL,
	[Count] INT NOT NULL,
	CONSTRAINT [PK_OrderLine] PRIMARY KEY CLUSTERED (OrderId ASC, ProductId ASC)
);
GO
-- create foreign keys
ALTER TABLE OrderLine ADD CONSTRAINT
	FK_OrderLine_OrderId FOREIGN KEY (OrderId)
		REFERENCES [Order] (Id)
GO
ALTER TABLE OrderLine ADD CONSTRAINT
	FK_OrderLine_ProductId FOREIGN KEY (ProductId)
		REFERENCES Product (Id)
GO
ALTER TABLE [Order] ADD CONSTRAINT
	FK_Order_CustomerId FOREIGN KEY (CustomerId)
		REFERENCES Customer (Id)
GO