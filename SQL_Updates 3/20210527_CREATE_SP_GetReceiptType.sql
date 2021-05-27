CREATE OR ALTER PROCEDURE usp_GetReceiptType
as
begin
select id, [name]  from receipts_type
end