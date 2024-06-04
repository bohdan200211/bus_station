CREATE TABLE [dbo].[Profile]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [firstname] NCHAR(10) NULL, 
    [lastname] NCHAR(10) NULL, 
    CONSTRAINT [FK_Profile_User] FOREIGN KEY ([id]) REFERENCES [User]([id])
)
