USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_LoadAddressList]    Script Date: 18.05.2021 22:10:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter proc [dbo].[sp_LoadAddressList]
    @szFirstName        nvarchar(200)       = null,
    @szLastName         nvarchar(200)       = null,
    @nEmployeeNmb       int                 = null,
    @nDepartementLink   int                 = null,
    @szError            nvarchar(500)       = N''         output,
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name: sp_LoadAddressList

    Erstellt am: 18.05.2021

    Inhalt: selektiert nach übergebenen Filter wenn diese ungleich null sind

    History:
        TK    erstellt
********************************************************************************/

  if @bDebug <> 0
  begin
    print N'Übergebene Parameter:'
    print N'********************************************************'
    print N'Vorname: ' + @szFirstName
    print N'Nachname: ' + @szLastName
    print N'MitarbeiterNr: ' + @nEmployeeNmb
    print N'Abteilung: ' + @nDepartementLink
    print N'********************************************************'
  end

  select e.szFirstName        as szFirstName
       , e.szLastName         as szLastName
       , e.nEmployeeNumber    as nEmployeeNumber
       , d.szName             as szDepartement
  from tblEmployee e
  join tblDepartement d on e.nDepartementLink = d.nKey
                       and e.nDepartementLink = ISNULL(@nDepartementLink, nDepartementLink)
  where e.szFirstName = ISNULL(@szFirstName, szFirstName)
    and e.szLastName = ISNULL(@szLastName, szLastName)
    and e.nEmployeeNumber = ISNULL(@nEmployeeNmb, nEmployeeNumber)

end


