USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_LoadEmployeeNumbers]    Script Date: 09.06.2021 15:01:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter proc [dbo].[sp_LoadEmployeeNumbers]
    @szError            nvarchar(500)       = N''         output,
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name: sp_LoadEmployeeNumbers

    Erstellt am: 25.06.2021

    Inhalt: lädt alle Mitarbeiternummern

    History:
        TK    erstellt
********************************************************************************/

  if @bDebug <> 0
  begin
    print N'Übergebene Parameter:'
    print N'********************************************************'
    print N'********************************************************'
  end

  select nKey
       , nEmployeeNumber
  from tblEmployee

end

