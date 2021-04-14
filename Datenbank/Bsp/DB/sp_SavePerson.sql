

use dbBsp

go


create proc sp_SavePerson
  @szFirstName  nvarchar(200),
  @szLastName   nvarchar(200)
as begin

  if not exists (select * from tblPerson where szFirstName = @szFirstName and szLastName = @szLastName)
  begin
    insert tblPerson (szFirstName, szLastName)
    values (@szFirstName, @szLastName)
  end

end


go



