using Blood.domain;
using Blood.domain.Models.Login;
using Blood.infrastructure;

namespace Blood.Infrastructure.LoginInfra
{
    public class  LoginRepo:ILoginRepo
    {
        RepositoryDao Dao;
        public LoginRepo(RepositoryDao dao)
        {
            Dao = dao;
        }

        public CommonDBResponse LoginUser(LoginCommon model)
        {
            LoginCommon r = new LoginCommon();
            // set SQL 
            var sql = "Exec sproc_BSUser_login @flag='login',@user_name=" + Dao.FilterString(model.UserName) + ",@password=" + Dao.FilterString(model.Password);
            // Consume DB
            var dt = Dao.ExecuteDataRow(sql);

            if (dt != null)
            {
                string code = Dao.ParseColumnValue(dt, "code").ToString();
                // string Username = Dao.ParseColumnValue(dt, "Username").ToString();
                string message = Dao.ParseColumnValue(dt, "message").ToString();
                if ((code ?? "1") != "0")
                {
                   // r.Code = CommonDBResponse.Failed;
                    r.Message = message;
                    r.Code = Convert.ToInt32(code);
                }
                else
                {
                    r.UserId = model.UserId;
                    r.SessionId = model.SessionId;
                    r.UserName = model.UserName;
                    r.UserId = model.MobileNo;
                   // r.Code = CommonDBResponse.Success;
                    r.Message = message;
                    r.Code = Convert.ToInt32(code);
                    return r;
                }
            }
            else
            {
                //r.Code = CommonDBResponse.Failed;
                r.Message = "Login Failed!";
            }
            return null;
        }
    }
}