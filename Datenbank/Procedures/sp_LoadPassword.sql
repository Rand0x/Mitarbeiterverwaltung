USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_LoadPassword]    Script Date: 09.06.2021 15:01:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter proc [dbo].[sp_LoadPassword]
    @nKey               int,
    @szError            nvarchar(500)       = N''         output,
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name: sp_LoadPassword

    Erstellt am: 18.05.2021

    Inhalt: lädt vorhandenes User-Passwort und Salt

    History:
        TK    erstellt
********************************************************************************/

  if @bDebug <> 0
  begin
    print N'Übergebene Parameter:'
    print N'********************************************************'
    print N'********************************************************'
  end

  select szPassword
       , szSalt
  from tblUser
  where nKey = @nKey
  
end


