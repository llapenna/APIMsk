CREATE TABLE [dbo].[logs](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[Message] [varchar](max) NOT NULL,
	[Stack] [varchar](max) NOT NULL,
	[datetime] [datetime] NOT NULL,
	[id_user] [bigint] NOT NULL,
	[id_company] [bigint] NOT NULL,
 CONSTRAINT [PK_logs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE OR ALTER PROCEDURE getLog
as
begin
select * from logs order by id desc
end
go

CREATE OR ALTER PROCEDURE insert_log
@message varchar(max),
@stack varchar(max),
@iduser bigint,
@idcompany bigint
as
begin
insert into logs(Message ,Stack ,datetime ,id_user,id_company) values
				(@message,@stack,GETDATE(),@iduser,@idcompany)
end
GO
CREATE OR ALTER PROCEDURE delete_log
as
begin
delete logs
end
GO



