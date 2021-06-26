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
    @szTelephone        nvarchar(200)       = null,
    @szTelephonePrivate nvarchar(200)       = null,
    @szMail             nvarchar(200)       = null,
    @szSex              nvarchar(1)         = null,
    @nDepartementLink   int                 = null,
    @szJobName          nvarchar(200)       = null,
    @nHoursPerWeek      int                 = null,
    @dtRecruitDate      datetime            = null,
    @rWage              decimal(10,2)       = null,
    @nHolidyPerYear     int                 = null,
    @nNoticePeriod      int                 = null,
    @nTaxClass          int                 = null,
    @szComment          nvarchar(max)       = null,       
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

  if @nEmployeeNmb is not null
  begin
    update e
    set nEmployeeNumber = @nEmployeeNmb
    from tblEmployee e
    where nKey = @nKey
  end

  if @dtBirthdate is not null
  begin
    update e
    set dtBirthdate = @dtBirthdate
    from tblEmployee e
    where nKey = @nKey
  end

  if @szTelephone is not null
  begin
    update e
    set szTelephone = @szTelephone
    from tblEmployee e
    where nKey = @nKey
  end

  if @szMail is not null
  begin
    update e
    set szMail = @szMail
    from tblEmployee e
    where nKey = @nKey
  end

  if @szSex is not null
  begin
    update e
    set szSex = @szSex
    from tblEmployee e
    where nKey = @nKey
  end

  if @nDepartementLink is not null
  begin
    update e
    set nDepartementLink = @nDepartementLink
    from tblEmployee e
    where nKey = @nKey
  end

  if @szJobName is not null
  begin
    update e
    set szJobName = @szJobName
    from tblEmployee e
    where nKey = @nKey
  end

  if @nHoursPerWeek is not null
  begin
    update e
    set nHoursPerWeek = @nHoursPerWeek
    from tblEmployee e
    where nKey = @nKey
  end

  if @dtRecruitDate is not null
  begin
    update e
    set dtRecruitDate = @dtRecruitDate
    from tblEmployee e
    where nKey = @nKey
  end

  if @rWage is not null
  begin
    update e
    set rWage = @rWage
    from tblEmployee e
    where nKey = @nKey
  end

  if @nHolidyPerYear is not null
  begin
    update e
    set nHolidyPerYear = @nHolidyPerYear
    from tblEmployee e
    where nKey = @nKey
  end

  if @nNoticePeriod is not null
  begin
    update e
    set nNoticePeriod = @nNoticePeriod
    from tblEmployee e
    where nKey = @nKey
  end

  if @nTaxClass is not null
  begin
    update e
    set nTaxClass = @nTaxClass
    from tblEmployee e
    where nKey = @nKey
  end

  if @szComment is not null
  begin
    update e
    set szComment = @szComment
    from tblEmployee e
    where nKey = @nKey
  end

  --update e
  --set e.nEmployeeNumber = ISNULL(@nEmployeeNmb, e.nEmployeeNumber)
  --  , e.dtBirthdate = ISNULL(@dtBirthdate, e.dtBirthdate) 
  --  , e.szTelephone = ISNULL(@szTelephone, e.szTelephone)
  --  , e.szMail = ISNULL(@szMail, szMail)
  --  , e.szSex = ISNULL(@szSex, e.szSex)
  --  , e.nDepartementLink = ISNULL(@nDepartementLink, e.nDepartementLink)
  --  , e.szJobName = ISNULL(@szJobName, e.szJobName)
  --  , e.nHoursPerWeek = ISNULL(@nHoursPerWeek, e.nHoursPerWeek)
  --  , e.dtRecruitDate = ISNULL(@dtRecruitDate, e.dtRecruitDate)
  --  , e.rWage = ISNULL(@rWage, e.rWage)
  --  , e.nHolidyPerYear = ISNULL(@nHolidyPerYear, e.nHolidyPerYear)
  --  , e.nNoticePeriod = ISNULL(@nNoticePeriod, e.nNoticePeriod)
  --  , e.nTaxClass = ISNULL(@nTaxClass, e.nTaxClass)
  --  , e.szComment = ISNULL(@szComment, e.szComment)
  --from tblEmployee e
  --where e.nKey = @nKey
  
  if @szMail is not null
  begin
    update a 
    set a.szPrivateTelephone = ISNULL(@szTelephonePrivate, szPrivateTelephone)
    from tblAddress a
    join tblEmployee e on e.nAddressLink = a.nKey
    where e.nKey = @nKey
  end
  
end


