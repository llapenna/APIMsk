BEGIN TRANSACTION;

ALTER TABLE order_header
ADD id_user bigint


ROLLBACK;