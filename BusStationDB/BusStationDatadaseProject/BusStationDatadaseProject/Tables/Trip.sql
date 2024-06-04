CREATE TABLE [dbo].[Trip]
(
	[id] INT NOT NULL PRIMARY KEY, 
    [id_bus] INT NOT NULL, 
    [id_station] INT NOT NULL,
	[distance] FLOAT NOT NULL, 
    CONSTRAINT [FK_Trip_Station] FOREIGN KEY ([id_station]) REFERENCES [Station]([id]), 
)
