USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_LoadDepartements]    Script Date: 18.05.2021 22:10:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter proc [dbo].[sp_LoadDepartements]
    @szError            nvarchar(500)       = N''         output,
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name: sp_LoadDepartements

    Erstellt am: 01.06.2021

    Inhalt: selektiert nach übergebenen Filter wenn diese ungleich null sind

    History:
        TK    erstellt
********************************************************************************/

  select d.nKey
       , d.szName
       , d.szIdentifier
       , d.szInfo
       , d.nManagerLink
       , e.szFirstName + N' ' +e.szLastName as szManagerName
  from tblDepartement d
  join tblEmployee e on d.nManagerLink = e.nKey 

end


