

use dbXXXX

go

if not exists(select * from sys.objects where name = N'fn_XXXX')
begin
  create function [dbo].[fn_XXXX] returns datatype/tbl begin end
end

go

alter function [dbo].[fn_XXXX]
()
returns datatype/tbl
begin

/********************************************************************************
    Name:

    Erstellt am:

    Inhalt:

    History:
********************************************************************************/

  

end


go


