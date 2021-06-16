USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_LoadRights]    Script Date: 09.06.2021 15:01:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter proc [dbo].[sp_LoadRights]
    @szError            nvarchar(500)       = N''         output,
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name: sp_LoadRights

    Erstellt am: 18.05.2021

    Inhalt: lädt alle Rechte

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
       , szName
       , szInfo
  from tblRight
  
end


