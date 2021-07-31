-- drop foreign keys
ALTER TABLE [Order]
    DROP CONSTRAINT IF EXISTS [FK_Order_CustomerId]
ALTER TABLE [OrderLine]
    DROP CONSTRAINT IF EXISTS [FK_OrderLine_OrderId]
ALTER TABLE [OrderLine]
    DROP CONSTRAINT IF EXISTS [FK_OrderLine_ProductId]
-- truncate tables
TRUNCATE TABLE [OrderLine]
GO
TRUNCATE TABLE [Order]
GO
TRUNCATE TABLE [Product]
GO
TRUNCATE TABLE [Customer]
GO
-- create foreign keys
ALTER TABLE [OrderLine]
    ADD CONSTRAINT FK_OrderLine_OrderId
        FOREIGN KEY (OrderId) REFERENCES [Order] (Id)
GO
ALTER TABLE [OrderLine]
    ADD CONSTRAINT FK_OrderLine_ProductId
        FOREIGN KEY (ProductId) REFERENCES Product (Id)
GO
ALTER TABLE [Order]
    ADD CONSTRAINT FK_Order_CustomerId
        FOREIGN KEY (CustomerId) REFERENCES Customer (Id)
GO
-- insert data
INSERT INTO [Customer] ([Name]) VALUES
    ('Андрей'),
    ('Алёна'),
    ('Алексей'),
    ('Полина'),
    ('Светлана'),
    ('Мария'),
    ('Тимофей');
GO
INSERT INTO [Product] ([Name], [Price]) VALUES
    ('Браслет Xiaomi Mi Band 3', 1548),
    ('Часы Amazfit Bip', 4199),
    ('Часы Samsung Galaxy Watch Active', 12050),
    ('Браслет Honor Band 4', 2249),
    ('Часы Samsung Galaxy Watch (46 mm)', 16250),
    ('Часы Samsung Gear S3 Frontier', 12860),
    ('Часы CARCAM DZ09', 987),
    ('Браслет HUAWEI Band 3 Pro', 4169),
    ('Часы CARCAM S2', 2990),
    ('Браслет Xiaomi Mi Band 2', 1250),
    ('Часы Garmin Instinct', 19770),
    ('Часы Amazfit Stratos', 10289),
    ('Часы Amazfit Verge', 8171),
    ('Браслет Amazfit Cor 2', 3850),
    ('Часы IWO Smart Watch IWO 7', 6500),
    ('Часы Amazfit Pace', 6899),
    ('Часы Garmin Forerunner 235', 12970),
    ('Часы Apple Watch Series 3 38mm Aluminum Case with Sport Band', 16740),
    ('Браслет GSMIN WR12', 2790),
    ('Часы KREZ Jazz', 3990);
GO
INSERT INTO [Order] ([CustomerId], [OrderDate], [Discount]) VALUES
    (1, '2017-03-21', NULL),
    (1, '2017-04-22', 0.02),
    (1, '2017-05-23', 0.1),
    (1, '2018-01-04', 0.05),
    (1, '2019-02-12', 0.03),
    (3, '2017-03-16', NULL),
    (3, '2017-03-26', 0.01),
    (3, '2019-06-03', 0.03),
    (4, '2015-07-25', 0.01),
    (4, '2015-05-07', 0.02),
    (4, '2016-01-14', 0.03),
    (4, '2016-03-29', 0.04),
    (4, '2017-05-03', 0.05),
    (4, '2018-02-06', 0.1),
    (4, '2019-01-27', 0.1),
    (4, '2019-05-12', 0.15),
    (6, '2019-01-20', NULL),
    (6, '2019-01-29', 0.02),
    (6, '2019-02-17', 0.02),
    (6, '2019-03-01', 0.02),
    (7, '2019-03-02', 0.03),
    (7, '2019-03-11', 0.1),
    (7, '2019-04-22', 0.12),
    (7, '2019-06-01', 0.15);
GO
INSERT INTO [OrderLine] ([OrderId], [ProductId], [Count]) VALUES
    (1, 11, 1),
    (1, 20, 1),
    (2, 1, 1),
    (3, 2, 1),
    (4, 3, 1),
    (5, 4, 1),
    (6, 5, 1),
    (7, 6, 1),
    (8, 7, 1),
    (9, 8, 1),
    (10, 9, 1),
    (11, 9, 2),
    (12, 10, 1),
    (13, 10, 1),
    (14, 11, 1),
    (15, 11, 5),
    (16, 12, 1),
    (17, 12, 1),
    (18, 13, 2),
    (19, 13, 1),
    (20, 13, 4),
    (21, 1, 2),
    (21, 4, 1),
    (21, 9, 1),
    (22, 1, 2),
    (22, 4, 1),
    (22, 9, 1),
    (23, 1, 2),
    (24, 7, 2),
    (24, 4, 1),
    (24, 3, 2);
GO
