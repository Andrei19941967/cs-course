CREATE TABLE [DepartureBoard](
	[Id] [INT] NOT NULL,
	[Town] [char](30) NOT NULL,
	[FirstCountry] [char](30) NOT NULL,
	[SecondCountry] [char](30) NOT NULL,
	[TimeInAir] [time] NOT NULL,
	[Company] [char](30) NOT NULL,
	[Model] [char](30) NOT NULL
);

INSERT INTO [DepartureBoard] ([Id], [Town], [FirstCountry], [SecondCountry], [TimeInAir], [Company], [Model]) 
VALUES ('1', 'Moscow', 'Russia', 'Ukraine',  '03:09', 'AirRU', 'Boing');

INSERT INTO [DepartureBoard] ([Id], [Town], [FirstCountry], [SecondCountry], [TimeInAir], [Company], [Model]) 
VALUES ('1', 'Kiev', 'Ukraine', 'Russia',  '02:57', 'AirRU', 'Boing');

SELECT * FROM [DepartureBoard];

DROP TABLE [DepartureBoard];