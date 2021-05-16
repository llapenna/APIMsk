BEGIN TRANSACTION;

ALTER TABLE order_header
DROP COLUMN id_company

ALTER TABLE order_header
ADD id_user bigint

UPDATE order_header
SET id_user = 2
WHERE id_customer <> 0

UPDATE order_header
SET id_user = 1
WHERE id_customer = 0

select * from order_header where id_user = 1

exec usp_GetOrders 1

select * from login

ROLLBACK;