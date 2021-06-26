USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_AddAbsence]    Script Date: 09.06.2021 15:01:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter proc [dbo].[sp_AddAbsence]
    @nEmployeeLink      int,
    @szReason           nvarchar(200),
    @szInfo             nvarchar(max)       null,
    @dtStart            datetime,
    @dtEnd              datetime,
    @nEffective         int                 null,
    @szError            nvarchar(500)       = N''         output,
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name: sp_AddAbsence

    Erstellt am: 18.05.2021

    Inhalt: fügt Absenz hinzu

    History:
        TK    erstellt
********************************************************************************/

  if @bDebug <> 0
  begin
    print N'Übergebene Parameter:'
    print N'********************************************************'
    print N'EmployeeLink: ' + CAST(@nEmployeeLink as nvarchar)
    print N'Reason: ' + CAST(@szReason as nvarchar)
    print N'Info: ' + CAST(@szInfo as nvarchar)
    print N'Start: ' + CAST(@dtStart as nvarchar)
    print N'End: ' + CAST(@dtEnd as nvarchar)
    print N'Effective: ' + CAST(@nEffective as nvarchar)
    print N'********************************************************'
  end

  insert into tblAbsence(nEmployeeLink, szReason, szInfo, dtStart, dtEnd, nEffective)
  values(@nEmployeeLink
       , @szReason     
       , @szInfo       
       , @dtStart      
       , @dtEnd        
       , @nEffective)

end

