USE EJPARSE
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.usp_GetDataSourceByName') AND type in (N'P', N'PC'))
DROP PROCEDURE dbo.usp_GetDataSourceByName
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  PROCEDURE dbo.usp_GetDataSourceByName
        @Name as NVARCHAR(20)
AS
SELECT TOP 1 Label AS [Key],JsonText AS [Value] FROM Json WHERE Label = @Name
