﻿/*
Deployment script for bus_station

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "bus_station"
:setvar DefaultFilePrefix "bus_station"
:setvar DefaultDataPath "C:\Users\lenovo\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB"
:setvar DefaultLogPath "C:\Users\lenovo\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Rename refactoring operation with key 2c47639a-b34a-46d3-8abc-b994d05de9b7 is skipped, element [dbo].[Book].[Id] (SqlSimpleColumn) will not be renamed to id';


GO
PRINT N'Rename refactoring operation with key 87724d25-8384-4021-a0bd-29f0fa265e0d, de7904fe-d5c1-42fd-85f1-457267734e09 is skipped, element [dbo].[Book].[bus] (SqlSimpleColumn) will not be renamed to id_trip';


GO
PRINT N'Rename refactoring operation with key b2088c59-6d3f-4cfe-8dfb-ce12f390ef70 is skipped, element [dbo].[Trip].[Id] (SqlSimpleColumn) will not be renamed to id';


GO
PRINT N'Rename refactoring operation with key 831aaf0a-b513-432c-8356-466886abfa7f is skipped, element [dbo].[Station].[Id] (SqlSimpleColumn) will not be renamed to id';


GO
PRINT N'Creating [dbo].[Book]...';


GO
CREATE TABLE [dbo].[Book] (
    [id]      INT NOT NULL,
    [id_trip] INT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
PRINT N'Creating [dbo].[Station]...';


GO
CREATE TABLE [dbo].[Station] (
    [id]   INT        NOT NULL,
    [name] NCHAR (40) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
PRINT N'Creating [dbo].[Trip]...';


GO
CREATE TABLE [dbo].[Trip] (
    [id]         INT        NOT NULL,
    [id_bus]     INT        NULL,
    [id_station] INT        NOT NULL,
    [distance]   FLOAT (53) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_Trip_Station]...';


GO
ALTER TABLE [dbo].[Trip] WITH NOCHECK
    ADD CONSTRAINT [FK_Trip_Station] FOREIGN KEY ([id_station]) REFERENCES [dbo].[Station] ([id]);


GO
PRINT N'Altering [dbo].[Fulluser]...';


GO
ALTER VIEW [dbo].[Fulluser]
	AS select
	u.id as id, u.username as username, u.password as password, 
	p.lastname as lastname, p.firstname as firstname, 
	u.createAt as createAt 
	from [User] u 
	inner join [Profile] p 
	on 
	u.id = p.id;
GO
-- Refactoring step to update target server with deployed transaction logs
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '2c47639a-b34a-46d3-8abc-b994d05de9b7')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('2c47639a-b34a-46d3-8abc-b994d05de9b7')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '87724d25-8384-4021-a0bd-29f0fa265e0d')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('87724d25-8384-4021-a0bd-29f0fa265e0d')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'de7904fe-d5c1-42fd-85f1-457267734e09')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('de7904fe-d5c1-42fd-85f1-457267734e09')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'b2088c59-6d3f-4cfe-8dfb-ce12f390ef70')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('b2088c59-6d3f-4cfe-8dfb-ce12f390ef70')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '831aaf0a-b513-432c-8356-466886abfa7f')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('831aaf0a-b513-432c-8356-466886abfa7f')

GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Trip] WITH CHECK CHECK CONSTRAINT [FK_Trip_Station];


GO
PRINT N'Update complete.';


GO
