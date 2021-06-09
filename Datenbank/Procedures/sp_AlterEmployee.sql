USE [dbMAV]
GO
/****** Object:  StoredProcedure [dbo].[sp_AlterEmployee]    Script Date: 09.06.2021 15:01:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


alter proc [dbo].[sp_AlterEmployee]
    @nKey               int,    
    @nDepartementLink   int                 null,
    @szJobName          nvarchar(200)       null,
    @nHoursPerWeek      int                 null,
    @rWage              decimal(10,2)       null,
    @nHolidyPerYear     int                 null,
    @nNoticePeriod      int                 null,
    @nTaxClass          int                 null,
    @szComment          nvarchar(max)       null,       
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
    print N'********************************************************'
  end

  update e
  set nDepartementLink = ISNULL(@nDepartementLink, nDepartementLink)
    , szJobName = ISNULL(@szJobName, szJobName)
    , nHoursPerWeek = ISNULL(@nHoursPerWeek, nHoursPerWeek)
    , rWage = ISNULL(@rWage, rWage)
    , nHolidyPerYear = ISNULL(@nHolidyPerYear, nHolidyPerYear)
    , nNoticePeriod = ISNULL(@nNoticePeriod, nNoticePeriod)
    , nTaxClass = ISNULL(@nTaxClass, nTaxClass)
    , szComment = ISNULL(@szComment, szComment)
  from tblEmployee e
  where nKey = @nKey
  
  
end


