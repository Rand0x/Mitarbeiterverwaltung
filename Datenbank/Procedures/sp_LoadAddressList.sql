USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_LoadAddressList]    Script Date: 07.06.2021 18:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER proc [dbo].[sp_LoadAddressList]
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

  select @szFirstName = N'%' + LOWER(@szFirstName) + N'%'
  select @szLastName = N'%' + LOWER(@szLastName) + N'%'

  select e.nKey               as nKey
       , e.szFirstName        as szFirstName
       , e.szLastName         as szLastName
       , e.nEmployeeNumber    as nEmployeeNumber
       , e.szTelephone        as szLandlineNmb
       , d.szName             as szDepartement
  from tblEmployee e
  join tblDepartement d on e.nDepartementLink = d.nKey
                       and d.nKey = ISNULL(@nDepartementLink, d.nKey)
  where LOWER(e.szFirstName) like ISNULL(@szFirstName, LOWER(szFirstName))
    and LOWER(e.szLastName) like ISNULL(@szLastName, LOWER(szLastName))
    and e.nEmployeeNumber = ISNULL(@nEmployeeNmb, nEmployeeNumber)

end


