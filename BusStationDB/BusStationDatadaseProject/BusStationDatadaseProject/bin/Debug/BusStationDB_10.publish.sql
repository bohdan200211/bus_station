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
PRINT N'Dropping [dbo].[FK_Trip_Station]...';


GO
ALTER TABLE [dbo].[Trip] DROP CONSTRAINT [FK_Trip_Station];


GO
PRINT N'Starting rebuilding table [dbo].[Station]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Station] (
    [id]   INT        NOT NULL,
    [name] NCHAR (80) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([name] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Station])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_Station] ([id], [name])
        SELECT   [id],
                 [name]
        FROM     [dbo].[Station]
        ORDER BY [id] ASC;
    END

DROP TABLE [dbo].[Station];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Station]', N'Station';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[FK_Trip_Station]...';


GO
ALTER TABLE [dbo].[Trip] WITH NOCHECK
    ADD CONSTRAINT [FK_Trip_Station] FOREIGN KEY ([id_station]) REFERENCES [dbo].[Station] ([id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Trip] WITH CHECK CHECK CONSTRAINT [FK_Trip_Station];


GO
PRINT N'Update complete.';


GO
