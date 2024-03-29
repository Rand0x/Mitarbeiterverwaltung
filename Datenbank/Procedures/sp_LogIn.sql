USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_LogIn]    Script Date: 07.06.2021 18:43:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER proc [dbo].[sp_LogIn]
    @szUserName         nvarchar(200),
    @szError            nvarchar(500)       = N''         output,
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name: sp_LogIn

    Erstellt am: 18.05.2021

    Inhalt: prüft ob user vorhande, wenn ja gleicht Passwort ab und gibt die UserID, RechtID und Rechtnamen zurück

    History:
        TK    erstellt
********************************************************************************/

  if @bDebug <> 0
  begin
    print N'Übergebene Parameter:'
    print N'********************************************************'
    print N'UserName: ' + @szUserName
    print N'********************************************************'
  end

  --prüfen ob username vorhanden
  if not exists (select top 1 * from tblUser where szName = @szUserName)
  begin 
    select @szError = N'User existiert nicht'
  end else begin
    
    --abgleichen des Passworts und ermittln der Daten
    select top 1
           u.nKey             as nKey
         , u.nEmployeeLink    as nEmployeeLink
         , u.szPassword       as szPassword
         , u.szSalt           as szSalt
         , u.nRightLink       as nRightLink
         , r.szName           as szRightName
    from tblUser u
    join tblRight r on u.nRightLink = r.nKey
    where u.szName = @szUserName

    --fals Passwort falsch war, Fehlermeldung schreiben
    if(@@ROWCOUNT = 0) 
    begin
      select @szError = N'Übergebene Daten sind falsch'
    end

  end

end


