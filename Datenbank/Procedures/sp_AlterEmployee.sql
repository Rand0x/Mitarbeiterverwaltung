USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_AlterEmployee]    Script Date: 09.06.2021 15:01:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter proc [dbo].[sp_AlterEmployee]
    @nKey               int,    
    @nEmployeeNmb       int                 = null,
    @dtBirthdate        datetime            = null,
    @szTelephone        nvarchar(50)        = null,
    @szMobile           nvarchar(50)        = null,
    @szMail             nvarchar(200)       = null,
    @szSex              nvarchar(1)         = null,
    @nDepartementLink   int                 = null,
    @szJobName          nvarchar(200)       = null,
    @nHoursPerWeek      int                 = null,
    @dtRecruitDate      datetime            = null,
    @rWage              decimal(10,2)       = null,
    @rOvertime          decimal(5,1)        = null,
    @nHolidyPerYear     int                 = null,
    @nNoticePeriod      int                 = null,
    @nTaxClass          int                 = null,
    @szMaritalStatus    nvarchar(50)        = null,
    @szComment          nvarchar(max)       = null,       
    @szTelephonePrivate nvarchar(50)        = null,
    @szMobileNmbPrivate nvarchar(50)        = null,
    @szHouseNumber      nvarchar(10)        = null,
    @szStreet           nvarchar(200)       = null,
    @szPLZ              nvarchar(5)         = null,
    @szCity             nvarchar(200)       = null,
    @szIBAN             nvarchar(22)        = null,
    @szBIC              nvarchar(11)        = null,
    @szBankName         nvarchar(200)       = null,
    @szError            nvarchar(500)       = N''         output,
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name: sp_AlterEmployee

    Erstellt am: 18.05.2021

    Inhalt: ändert Daten eines Mitarbeiter

    History:
        TK    erstellt
********************************************************************************/

  if @bDebug <> 0
  begin
    print N'Übergebene Parameter:'
    print N'********************************************************'
    print N'EmployeeNmb: ' + CAST(@nEmployeeNmb as nvarchar)
    print N'Birthdate: ' + CAST(@dtBirthdate as nvarchar)
    print N'Telephone: ' + CAST(@szTelephone as nvarchar)
    print N'TelephonePrivate: ' + CAST(@szTelephonePrivate as nvarchar)
    print N'Mail: ' + CAST(@szMail as nvarchar)
    print N'Sex: ' + CAST(@szSex as nvarchar)
    print N'DepartementLink: ' + CAST(@nDepartementLink as nvarchar)
    print N'JobName: ' + CAST(@szJobName as nvarchar)
    print N'HoursPerWeek: ' + CAST(@nHoursPerWeek as nvarchar)
    print N'RecruitDate: ' + CAST(@dtRecruitDate as nvarchar)
    print N'Wage: ' + CAST(@rWage as nvarchar)
    print N'HolidyPerYear: ' + CAST(@nHolidyPerYear as nvarchar)
    print N'NoticePeriod: ' + CAST(@nNoticePeriod as nvarchar)
    print N'TaxClass: ' + CAST(@nTaxClass as nvarchar)
    print N'Comment: ' + CAST(@szComment as nvarchar)
    print N'********************************************************'
  end

  update e
  set e.nEmployeeNumber = ISNULL(@nEmployeeNmb, e.nEmployeeNumber)
    , e.dtBirthdate = ISNULL(@dtBirthdate, e.dtBirthdate) 
    , e.szTelephone = ISNULL(@szTelephone, e.szTelephone)
    , e.szMobileNumber = ISNULL(@szMobile, e.szMobileNumber)
    , e.szMail = ISNULL(@szMail, szMail)
    , e.szSex = ISNULL(@szSex, e.szSex)
    , e.nDepartementLink = ISNULL(@nDepartementLink, e.nDepartementLink)
    , e.szJobName = ISNULL(@szJobName, e.szJobName)
    , e.nHoursPerWeek = ISNULL(@nHoursPerWeek, e.nHoursPerWeek)
    , e.dtRecruitDate = ISNULL(@dtRecruitDate, e.dtRecruitDate)
    , e.rWage = ISNULL(@rWage, e.rWage)
    , e.rOvertime = ISNULL(@rOvertime, e.rOvertime)
    , e.nHolidyPerYear = ISNULL(@nHolidyPerYear, e.nHolidyPerYear)
    , e.nNoticePeriod = ISNULL(@nNoticePeriod, e.nNoticePeriod)
    , e.nTaxClass = ISNULL(@nTaxClass, e.nTaxClass)
    , e.szMaritalStatus = ISNULL(@szMaritalStatus, e.szMaritalStatus)
    , e.szComment = ISNULL(@szComment, e.szComment)
  from tblEmployee e
  where e.nKey = @nKey
  
  update a 
  set a.szPrivateTelephone = ISNULL(@szTelephonePrivate, a.szPrivateTelephone)
    , a.szHouseNumber = ISNULL(@szHouseNumber, a.szHouseNumber)
    , a.szStreet = ISNULL(@szStreet, a.szStreet)
    , a.szPLZ = ISNULL(@szPLZ, a.szPLZ)
    , a.szCity = ISNULL(@szCity, a.szCity)
    , a.szPrivateMobileNmb = ISNULL(@szMobileNmbPrivate, a.szPrivateMobileNmb)
  from tblAddress a
  join tblEmployee e on e.nAddressLink = a.nKey
  where e.nKey = @nKey

  update b
  set b.szBankName = ISNULL(@szBankName, b.szBankName)
    , b.szBIC = ISNULL(@szBIC, b.szBIC)
    , b.szIBAN = ISNULL(@szIBAN, b.szIBAN)
  from tblBanking b
  join tblEmployee e on e.nBankingLink = b.nKey
  where e.nKey = @nKey
  
end


