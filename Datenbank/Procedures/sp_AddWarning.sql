USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_AddWarning]    Script Date: 09.06.2021 15:01:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter proc [dbo].[sp_AddWarning]
    @nEmployeeLink      int,
    @szReason           nvarchar(200),
    @dtIssueDate        datetime,
    @szComment          nvarchar(max)       null,
    @szError            nvarchar(500)       = N''         output,
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name: sp_AddWarning

    Erstellt am: 18.05.2021

    Inhalt: fügt Verwarnung hinzu

    History:
        TK    erstellt
********************************************************************************/

  if @bDebug <> 0
  begin
    print N'Übergebene Parameter:'
    print N'********************************************************'
    print N'EmployeeLink: ' + CAST(@nEmployeeLink as nvarchar)
    print N'Reason: ' + CAST(@szReason as nvarchar)
    print N'IssueDate: ' + CAST(@dtIssueDate as nvarchar)
    print N'Comment: ' + CAST(@szComment as nvarchar)
    print N'********************************************************'
  end

  insert into tblWarning(nEmployeeLink, szReason, dtIssueDate, szComment)
  values(@nEmployeeLink
       , @szReason          
       , @dtIssueDate    
       , @szComment)

end

