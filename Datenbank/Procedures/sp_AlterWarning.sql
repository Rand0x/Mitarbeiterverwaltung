USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_AlterWarning]    Script Date: 09.06.2021 15:01:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter proc [dbo].[sp_AlterWarning]
    @nKey               int,
    @szReason           nvarchar(200)       = null,
    @dtIssueDate        datetime            = null,
    @szComment          nvarchar(max)       = null,
    @szError            nvarchar(500)       = N''         output,
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name: sp_AlterWarning

    Erstellt am: 18.05.2021

    Inhalt: bearbeitet eine Verwarnung

    History:
        TK    erstellt
********************************************************************************/

  if @bDebug <> 0
  begin
    print N'Übergebene Parameter:'
    print N'********************************************************'
    print N'Key: ' + CAST(@nKey as nvarchar)
    print N'Reason: ' + CAST(@szReason as nvarchar)
    print N'IssueDate: ' + CAST(@dtIssueDate as nvarchar)
    print N'Comment: ' + CAST(@szComment as nvarchar)
    print N'********************************************************'
  end

  update w
  set szReason = ISNULL(@szReason, szReason)
    , dtIssueDate = ISNULL(@dtIssueDate, dtIssueDate)
    , szComment = ISNULL(@szComment, szComment)
  from tblWarning w
  where nKey = @nKey
 
end

