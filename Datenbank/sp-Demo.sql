

use dbXXXX

go

if not exists(select * from sys.objects where name = N'sp_XXXX')
begin
  create proc [dbo].[sp_XXXX] as begin end
end

go

alter proc [dbo].[sp_XXXX]
    @szError            nvarchar(500)       = N'',
    @bDebug             int                 = 0
as begin

/********************************************************************************
    Name:

    Erstellt am:

    Inhalt:

    History:
********************************************************************************/

  if @bDebug <> 0
  begin
    print N'Übergebene Parameter:'
  end


  error:
  select @szError

end


go


