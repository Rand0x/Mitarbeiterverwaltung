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
    @dtBirthdate        datetime            = null,
    @szSex              nvarchar(1)         = null,
    @szMail             nvarchar(200)       = null,       
    @szTelephone        nvarchar(50)        = null,        
    @nDepartementLink   int                 = null,                            
    @szJobName          nvarchar(200)       = null,          
    @dtRecruitDate      datetime            = null,                 
    @nHoursPerWeek      int                 = null,                
    @rOvertime          decimal(5,1)        = null,       
    @rWage              decimal(10,2)       = null,      
    @nHolidyPerYear     int                 = null,                 
    @nNoticePeriod      int                 = null,
    @nTaxClass          int                 = null,                 
    @szComment          nvarchar(max)       = null,       
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
    print N'EmployeeNumber: ' + CAST(@nEmployeeNumber as nvarchar)
    print N'FirstName: ' + CAST(@szFirstName as nvarchar)
    print N'LastName: ' + CAST(@szLastName as nvarchar)
    print N'Birthdate: ' + CAST(@dtBirthdate as nvarchar)
    print N'Sex: ' + CAST(@szSex as nvarchar)
    print N'Mail: ' + CAST(@szMail as nvarchar)
    print N'Telephone: ' + CAST(@szTelephone as nvarchar)
    print N'DepartementLink: ' + CAST(@nDepartementLink as nvarchar)
    print N'JobName: ' + CAST(@szJobName as nvarchar)
    print N'RecruitDate: ' + CAST(@dtRecruitDate as nvarchar)
    print N'HoursPerWeek: ' + CAST(@nHoursPerWeek as nvarchar)
    print N'Overtime: ' + CAST(@rOvertime as nvarchar)
    print N'Wage: ' + CAST(@rWage as nvarchar)
    print N'HolidyPerYear: ' + CAST(@nHolidyPerYear as nvarchar)
    print N'NoticePeriod: ' + CAST(@nNoticePeriod as nvarchar)
    print N'TaxClass: ' + CAST(@nTaxClass as nvarchar)
    print N'Comment: ' + CAST(@szComment as nvarchar)
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


