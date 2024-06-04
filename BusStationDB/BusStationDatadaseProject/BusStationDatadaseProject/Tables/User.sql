CREATE TABLE [dbo].[User]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [username] NCHAR(10) NOT NULL, 
    [password] NCHAR(10) NOT NULL, 
    [createAt] DATETIME NOT NULL DEFAULT GETDATE() 
)
