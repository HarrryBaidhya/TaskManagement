USE [Taskmanagement]
GO
/****** Object:  StoredProcedure [dbo].[sproc_ManduUser_login]    Script Date: 3/1/2025 10:35:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



 create or ALTER      PROCEDURE [dbo].[sproc_BSUser_login] @flag VARCHAR(10)
	,@user_name VARCHAR(512)
	,@password VARCHAR(512)
	,@action_ip NVARCHAR(30)=NULL
	,@browser_info NVARCHAR(512)=NULL
	,@access_code VARCHAR(50) = null
	,@sessionId VARCHAR(max) = null
AS
SET NOCOUNT ON;

DECLARE @errortable TABLE (
	code INT
	,msg VARCHAR(100)
	,id VARCHAR(20)
	);
DECLARE @sys_date DATE;
---check if multiple login allow or not --                                        
DECLARE @allow_multiple_login CHAR(1)
	,@is_currently_loggedin CHAR(1)
	,@login_device_id NVARCHAR(50)
	,@login_session_id NVARCHAR(300)
	,@last_online_date NVARCHAR(50)
	,@user_type NVARCHAR(25)
	,@last_browser_info VARCHAR(1000)
	,@active_status VARCHAR(100)
	,@session_id NVARCHAR(100)
	,@checkCred NVARCHAR(max)

BEGIN
--	SET @browser_info = isnull(@browser_info, 'chrome');

	IF @flag = 'login'
	BEGIN
		IF EXISTS (
					SELECT 'x'
					FROM BS_UserMain WITH (NOLOCK)
					WHERE 
						BS_Email = @user_name 
						and BS_Password=@password
						--AND isnull(IsActive,'1') ='1'
					)
				SELECT 0 code
				,message = 'success'
				--,ud.UserId as UserId
				,ud.BS_UserName UserName
				,ud.BS_UserCode 			
			FROM BS_UserMain ud WITH (NOLOCK) where BS_email=@user_name 

			else
			select 1 code ,
			message ='password or username wrong'

			End
		End;
	

		
