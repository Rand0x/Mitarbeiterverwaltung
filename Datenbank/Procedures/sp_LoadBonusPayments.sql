USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_LoadBonusPayments]    Script Date: 09.06.2021 15:01:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter proc [dbo].[sp_LoadBonusPayments]
    @nEmployeeLink      int                 null,
    @dtDateOfPayment    datetime            null,
    @szError            nvarchar(500)       = N''         output,
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name: sp_LoadBonusPayments

    Erstellt am: 18.05.2021

    Inhalt: lädt Bonuszahlungen

    History:
        TK    erstellt
********************************************************************************/

  if @bDebug <> 0
  begin
    print N'Übergebene Parameter:'
    print N'********************************************************'
    print N'********************************************************'
  end

  select b.nKey
       , b.szReason
       , e.nKey               as EmployeeKey
       , e.szFirstName
       , e.szLastName
       , e.nEmployeeNumber
       , b.rAmount
       , b.dtDateOfPayment
       , b.szComment
  from tblBonusPayment b
  join tblEmployee e on b.nEmployeeLink = e.nKey
  where b.nEmployeeLink = ISNULL(@nEmployeeLink, b.nEmployeeLink)
    and b.dtDateOfPayment >= ISNULL(@dtDateOfPayment, b.dtDateOfPayment)

end

