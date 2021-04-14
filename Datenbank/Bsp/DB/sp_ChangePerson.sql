

use dbBsp

go


create proc sp_ChangePerson
  @nID  int,
  @szFirstName  nvarchar(200),
  @szLastName   nvarchar(200)
as begin

  if exists (select * from tblPerson where nID = @nID and (szFirstName <> @szFirstName or szLastName <> @szLastName))
  begin
    update tblPerson
    set szFirstName = @szFirstName, szLastName = @szLastName
    where nID = @nID
  end

end


go



