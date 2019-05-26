select * from Configs
update Configs set [Value] = 'www.facebook.com' where [Key] = 'FacebookUrl'
insert into Configs ([Key],[Value]) Values ('Email','m.ashiqkhan@outlook.com')