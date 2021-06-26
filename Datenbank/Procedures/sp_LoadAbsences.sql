USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_LoadAbsences]    Script Date: 09.06.2021 15:01:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter proc [dbo].[sp_LoadAbsences]
    @nEmployeeLink      int                 null,
    @dtStart            datetime            null,
    @dtEnd              datetime            null,
    @szError            nvarchar(500)       = N''         output,
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name: sp_LoadAbsences

    Erstellt am: 18.05.2021

    Inhalt: lädt Absenzen

    History:
        TK    erstellt
********************************************************************************/

  if @bDebug <> 0
  begin
    print N'Übergebene Parameter:'
    print N'********************************************************'
    print N'EmployeeLink: ' + CAST(@nEmployeeLink as nvarchar)
    print N'Start: ' + CAST(@dtStart as nvarchar)
    print N'End: ' + CAST(@dtEnd as nvarchar)
    print N'********************************************************'
  end

  select a.nKey
       , a.szReason
       , a.szInfo
       , e.nKey               as EmployeeKey
       , e.szFirstName
       , e.szLastName
       , e.nEmployeeNumber
       , a.dtStart
       , a.dtEnd
       , a.nEffective
  from tblAbsence a
  join tblEmployee e on a.nEmployeeLink = e.nKey
  where a.nEmployeeLink = ISNULL(@nEmployeeLink, a.nEmployeeLink)
    and a.dtStart >= ISNULL(@dtStart, a.dtStart)
    and a.dtEnd <= ISNULL(@dtEnd, a.dtEnd)

end

