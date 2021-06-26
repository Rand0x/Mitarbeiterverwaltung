USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteEmployee]    Script Date: 09.06.2021 15:01:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter proc [dbo].[sp_DeleteEmployee]
    @nKey               int,     
    @szError            nvarchar(500)       = N''         output,
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name: sp_DeleteEmployee

    Erstellt am: 18.05.2021

    Inhalt: löscht Daten eines Mitarbeiter

    History:
        TK    erstellt
********************************************************************************/

  if @bDebug <> 0
  begin
    print N'Übergebene Parameter:'
    print N'********************************************************'
    print N'Key: ' + CAST(@nKey as nvarchar)
    print N'********************************************************'
  end

  declare @nBankingLink int
  select @nBankingLink = nBankingLink from tblEmployee where nKey = @nKey

  declare @nAddressLink int
  select @nAddressLink = nAddressLink from tblEmployee where nKey = @nKey
  
  delete from tblEmployee where nKey = @nKey
  delete from tblUser where nEmployeeLink = @nKey

  if(0 = (select count(*) from tblEmployee where nBankingLink = @nBankingLink))
  begin
    delete from tblBanking where nKey = @nBankingLink
  end

  if(0 = (select count(*) from tblEmployee where nAddressLink = @nAddressLink))
  begin
    delete from tblAddress where nKey = @nAddressLink
  end
  
end


