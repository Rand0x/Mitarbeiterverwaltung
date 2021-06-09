USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_AddBonusPayment]    Script Date: 09.06.2021 15:01:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter proc [dbo].[sp_AddBonusPayment]
    @nEmployeeLink      int,
    @szReason           nvarchar(200),
    @rAmount            decimal(10,2),
    @dtDateOfPayment    datetime,
    @szComment          nvarchar(max)       null,
    @szError            nvarchar(500)       = N''         output,
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name: sp_AddBonusPayment

    Erstellt am: 18.05.2021

    Inhalt: fügt Bonuszahlungen hinzu

    History:
        TK    erstellt
********************************************************************************/

  if @bDebug <> 0
  begin
    print N'Übergebene Parameter:'
    print N'********************************************************'
    print N'********************************************************'
  end

  insert into tblBonusPayment(nEmployeeLink, szReason, rAmount, dtDateOfPayment, szComment)
  values(@nEmployeeLink
       , @szReason     
       , @rAmount     
       , @dtDateOfPayment    
       , @szComment)

end

