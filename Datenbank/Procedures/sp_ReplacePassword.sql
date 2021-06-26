USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_LoadPassword]    Script Date: 09.06.2021 15:01:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter proc [dbo].[sp_ReplacePassword]
    @nKey               int,
    @szPassword         nvarchar(max),
    @szSalt             nvarchar(max),
    @szError            nvarchar(500)       = N''         output,
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name: sp_ReplacePassword

    Erstellt am: 18.05.2021

    Inhalt: ersätzt vorhandenes Passwort und Salt für übergebenen User

    History:
        TK    erstellt
********************************************************************************/

  if @bDebug <> 0
  begin
    print N'Übergebene Parameter:'
    print N'********************************************************'
    print N'Key: ' + CAST(@nKey as nvarchar)
    print N'Password: ' + CAST(@szPassword as nvarchar)
    print N'Salt: ' + CAST(@szSalt as nvarchar)
    print N'********************************************************'
  end

  update u
  set szPassword = @szPassword
    , szSalt = @szSalt
  from tblUser u
  where nKey = @nKey
  
end


