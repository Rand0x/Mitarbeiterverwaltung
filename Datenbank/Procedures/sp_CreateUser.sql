USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateUser]    Script Date: 09.06.2021 15:01:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter proc [dbo].[sp_CreateUser]
    @szFirstName        nvarchar(200),
    @szLastName         nvarchar(200),
    @szPassword         nvarchar(max),
    @szSalt             nvarchar(200),
    @nRightLink         int,
    @szError            nvarchar(500)       = N''         output,
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name: sp_CreateUser

    Erstellt am: 18.05.2021

    Inhalt: erstellt einen User

    History:
        TK    erstellt
********************************************************************************/

  if @bDebug <> 0
  begin
    print N'Übergebene Parameter:'
    print N'********************************************************'
    print N'********************************************************'
  end

  declare @szUserName nvarchar(400)
  select @szUserName = @szFirstName + N'_' + @szLastName

  if not exists(select * from tblUser where szName = @szUserName)
  begin

    declare @nEmployeeLink int
    select @nEmployeeLink = nKey from tblEmployee where szFirstName = @szFirstName and szLastName = @szLastName
    
    insert into tblUser(
      szName       
    , szPassword   
    , szSalt       
    , nEmployeeLink
    , nRightLink
    )
    select @szUserName
         , @szPassword
         , @szSalt
         , @nEmployeeLink
         , @nRightLink

  end
  
end


