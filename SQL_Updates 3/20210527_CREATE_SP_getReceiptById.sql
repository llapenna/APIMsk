CREATE OR ALTER PROCEDURE usp_GetReciptTypeByid
@id bigint
as
begin
select id,name  from receipts_type where id=@id
end