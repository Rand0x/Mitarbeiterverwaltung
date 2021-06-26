USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_LoadWarnings]    Script Date: 09.06.2021 15:01:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter proc [dbo].[sp_LoadWarnings]
    @nEmployeeLink      int                 null,
    @dtIssueDate        datetime            null,
    @szError            nvarchar(500)       = N''         output,
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name: sp_LoadWarnings

    Erstellt am: 18.05.2021

    Inhalt: lädt Verwarnungen

    History:
        TK    erstellt
********************************************************************************/

  if @bDebug <> 0
  begin
    print N'Übergebene Parameter:'
    print N'********************************************************'
    print N'EmployeeLink: ' + CAST(@nEmployeeLink as nvarchar)
    print N'IssueDate: ' + CAST(@dtIssueDate as nvarchar)
    print N'********************************************************'
  end

  select w.nKey
       , w.szReason
       , e.nKey               as EmployeeKey
       , e.szFirstName
       , e.szLastName
       , e.nEmployeeNumber
       , w.dtIssueDate
       , w.szComment
  from tblWarning w
  join tblEmployee e on w.nEmployeeLink = e.nKey
  where w.nEmployeeLink = ISNULL(@nEmployeeLink, w.nEmployeeLink)
    and w.dtIssueDate >= ISNULL(@dtIssueDate, w.dtIssueDate)

end

