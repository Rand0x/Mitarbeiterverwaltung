
use dbMAV

go


alter proc [dbo].[sp_LogIn]
    @szUserName         nvarchar(200),
    @szPassword         nvarchar(max),
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
    print N'Password: ' + @szPassword
    print N'********************************************************'
  end

  --prüfen ob username vorhanden
  if not exists (select top 1 * from tblUser where szName = @szUserName)
  begin 
    select @szError = N'User existiert nicht'
  end else begin
    
    --abgleichen des Passworts und ermittln der Daten
    select u.nEmployeeLink, u.nRightLink, r.szName
    from tblUser u
    join tblRight r on u.nRightLink = r.nKey
    where u.szName = @szUserName and u.szPassword = @szPassword

    --fals Passwort falsch war, Fehlermeldung schreiben
    if(@@ROWCOUNT = 0) 
    begin
      select @szError = N'Übergebene Daten sind falsch'
    end

  end

end


go


