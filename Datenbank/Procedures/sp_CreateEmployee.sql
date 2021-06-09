USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateEmployee]    Script Date: 09.06.2021 15:01:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter proc [dbo].[sp_CreateEmployee]
    @nEmployeeNumber    int,                 
    @szFirstName        nvarchar(200), 
    @szLastName         nvarchar(200),       
    @dtBirthdate        datetime,
    @szSex              nvarchar(1)         null,
    @szMail             nvarchar(200),       
    @szTelephone        nvarchar(50),        
    @nDepartementLink   int,                             
    @szJobName          nvarchar(200),          
    @dtRecruitDate      datetime,                 
    @nHoursPerWeek      int,                
    @rOvertime          decimal(5,1)        null,       
    @rWage              decimal(10,2),      
    @nHolidyPerYear     int,                 
    @nNoticePeriod      int,
    @nTaxClass          int,                 
    @szComment          nvarchar(max)       null,       
    @szError            nvarchar(500)       = N''         output,
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name: sp_CreateEmployee

    Erstellt am: 18.05.2021

    Inhalt: erstellt einen Mitarbeiter

    History:
        TK    erstellt
********************************************************************************/

  if @bDebug <> 0
  begin
    print N'Übergebene Parameter:'
    print N'********************************************************'
    print N'********************************************************'
  end

  insert into tblEmployee
  (
    nEmployeeNumber, 
    szFirstName,     
    szLastName,      
    dtBirthdate,  
    szSex,
    szMail,          
    szTelephone,     
    nDepartementLink,
    szJobName,       
    dtRecruitDate,   
    nHoursPerWeek,   
    rOvertime,       
    rWage,           
    nHolidyPerYear,  
    nNoticePeriod,   
    nTaxClass,       
    szComment       
  )
  values
  (
    @nEmployeeNumber 
  , @szFirstName     
  , @szLastName      
  , @dtBirthdate
  , @szSex
  , @szMail          
  , @szTelephone     
  , @nDepartementLink
  , @szJobName       
  , @dtRecruitDate   
  , @nHoursPerWeek   
  , @rOvertime       
  , @rWage           
  , @nHolidyPerYear  
  , @nNoticePeriod   
  , @nTaxClass       
  , @szComment       
  )
  
  
end


