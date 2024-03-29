USE [Home]
GO
/****** Object:  Table [dbo].[tbl_CityDetail]    Script Date: 02-Feb-24 7:43:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_CityDetail](
	[cid] [int] IDENTITY(1,1) NOT NULL,
	[city] [varchar](100) NULL,
	[state] [varchar](100) NULL,
	[zip] [varchar](100) NULL,
	[sid] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[cid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_StuDetail]    Script Date: 02-Feb-24 7:43:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_StuDetail](
	[sid] [int] IDENTITY(1,1) NOT NULL,
	[fname] [varchar](100) NULL,
	[lname] [varchar](100) NULL,
	[username] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteStuDetail]    Script Date: 02-Feb-24 7:43:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_DeleteStuDetail]
@sid int=0
as
begin
Declare @Msg varchar(Max)='',@Focus Varchar(100)='' 
 delete from tbl_StuDetail where sid=@sid
 delete from tbl_CityDetail where sid=@sid
Set @Msg='Data has been Deletes successfully.' 
select @Msg as Msg
end
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertUpdateStuDetail]    Script Date: 02-Feb-24 7:43:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_InsertUpdateStuDetail]
@sid int=0,
@fname varchar(100),
@lname varchar(100),
@username varchar(100),
@cid int=0,
@city varchar(100),
@state varchar(100),
@zip varchar(100)
as
begin
 Declare @Msg varchar(max)='',@Focus varchar(100)='',@Status int=0
 if (@sid=0 and @cid=0)
 begin
  insert into tbl_StuDetail(fname,lname,username) values(@fname,@lname,@username);
  Set @sid=SCOPE_IDENTITY();
  insert into tbl_CityDetail(city,state,zip,sid) values(@city,@state,@zip,@sid);
  set @Msg='Data Insert Successfully!'
  set @Status=1
  end
 else
 begin
   update tbl_StuDetail set fname=@fname,@lname=@lname,username=@username where sid=@sid
   update tbl_CityDetail set city=@city,state=@state,zip=@zip where sid=@sid
   set @Msg='Data Update Successfully'
   set @Status=2
 end
 select @Msg as Msg,@Focus as Focus,@Status as [Status]
end




GO
/****** Object:  StoredProcedure [dbo].[sp_ShowStudentDetain]    Script Date: 02-Feb-24 7:43:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_ShowStudentDetain]
(
@sid int=0,
@cid int=0,
@Type varchar(1)
)
as
begin
 if(@Type='S')
 begin
  select st.sid,st.fname+ '  ' +st.lname as fullname,st.username,ct.city,ct.state,ct.zip from tbl_StuDetail st left join tbl_CityDetail ct  on st.[sid]=ct.[sid] order by st.sid asc;
 end
 else
  begin
    select st.sid,st.fname,st.lname,st.username,ct.cid,ct.city,ct.state,ct.zip from tbl_StuDetail st left join tbl_CityDetail ct on st.[sid]=ct.[cid] where st.sid=@sid ;
  end
end
GO
