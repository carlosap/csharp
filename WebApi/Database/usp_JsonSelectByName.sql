/*============================================================================
aman:Document_GetParentChildRelations
Originator: cperez
Date: 1/26/16
Description:  returns parent/child relations from Document table
=============================================================================*/
USE [EJPARSE]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_JsonSelectByName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_JsonSelectByName]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  PROCEDURE [dbo].[usp_JsonSelectByName]
        @ComponentName as nvarchar(20)
AS
SELECT     Json.JsonText AS data
FROM         Site INNER JOIN
                      Document ON Site.Id = Document.SiteId INNER JOIN
                      Json ON Document.Id = Json.ShowJsonsId AND Document.SiteId = Json.SiteId
WHERE     (Document.Name = @ComponentName) AND (Document.IsDeleted = 0)
GO

