USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_AlterBonusPayment]    Script Date: 09.06.2021 15:01:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter proc [dbo].[sp_AlterBonusPayment]
    @nKey               int,
    @szReason           nvarchar(200)       = null,
    @rAmount            decimal(10,2)       = null,
    @dtDateOfPayment    datetime            = null,
    @szComment          nvarchar(max)       = null,
    @szError            nvarchar(500)       = N''         output,
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name: sp_AlterBonusPayment

    Erstellt am: 18.05.2021

    Inhalt: bearbeitet eine Bonus Zahlung

    History:
        TK    erstellt
********************************************************************************/

  if @bDebug <> 0
  begin
    print N'Übergebene Parameter:'
    print N'********************************************************'
    print N'Key: ' + CAST(@nKey as nvarchar)
    print N'Reason: ' + CAST(@szReason as nvarchar)
    print N'Amount: ' + CAST(@rAmount as nvarchar)
    print N'DateOfPayment: ' + CAST(@dtDateOfPayment as nvarchar)
    print N'Comment: ' + CAST(@szComment as nvarchar)
    print N'********************************************************'
  end

  update b
  set szReason = ISNULL(@szReason, szReason)
    , rAmount = ISNULL(@rAmount, rAmount)
    , dtDateOfPayment = ISNULL(@dtDateOfPayment, dtDateOfPayment)
    , szComment = ISNULL(@szComment, szComment)
  from tblBonusPayment b
  where nKey = @nKey
 
end

