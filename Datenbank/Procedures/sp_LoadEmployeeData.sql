USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_LoadEmployeeData]    Script Date: 09.06.2021 15:01:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter proc [dbo].[sp_LoadEmployeeData]
    @nKey               int,
    @szError            nvarchar(500)       = N''         output,
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name: sp_LoadEmployeeData

    Erstellt am: 09.06.2021

    Inhalt: selektiert alle Daten die über einen Mitarbeiter gespeichert werden

    History:
        TK    erstellt
********************************************************************************/

  if @bDebug <> 0
  begin
    print N'Übergebene Parameter:'
    print N'********************************************************'
    print N'Key: ' + @nKey
    print N'********************************************************'
  end

  select e.*
       , a.szStreet
       , a.szHouseNumber
       , a.szCity
       , a.szPLZ
       , a.szPrivateMail
       , a.szPrivateTelephone
       , b.szBankName
       , b.szBIC
       , b.szIBAN
       , d.szName as szDepartementName
       , em.szFirstName + N' ' + em.szLastName as szManager
  from tblEmployee e
  left join tblAddress a on e.nAddressLink = a.nKey
  left join tblBanking b on e.nBankingLink = b.nKey
  left join tblDepartement d on e.nDepartementLink = d.nKey
  left join tblEmployee em on d.nManagerLink = em.nKey
  where e.nKey = @nKey
  
  
end


