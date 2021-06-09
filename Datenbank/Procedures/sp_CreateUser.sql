USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateUser]    Script Date: 09.06.2021 15:01:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter proc [dbo].[sp_CreateUser]
    @nEmployee          int,
    @nRight             int,
    @szError            nvarchar(500)       = N''         output,
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name: sp_CreateUser

    Erstellt am: 18.05.2021

    Inhalt: erstellt einen User mit Standartpasswort

    History:
        TK    erstellt
********************************************************************************/

  if @bDebug <> 0
  begin
    print N'Übergebene Parameter:'
    print N'********************************************************'
    print N'Employee: ' + @nEmployee
    print N'Rivht: ' + @nRight
    print N'********************************************************'
  end

  insert into tblUser(szName, szPassword, nEmployeeLink, nRightLink)
  select e.szFirstName + N'_' + e.szLastName
       , N'Standart'
       , @nEmployee
       , @nRight
  from tblEmployee e
  where e.nKey = @nEmployee
  
  
end


