using Blood.domain.Models.Login;
using Blood.domain;
using Blood.Infrastructure.LoginInfra;

namespace Blood.Business.LoginUser
{
    public class LoginBusiness:ILoginBusiness
    {
        private readonly ILoginRepo repo;
        public LoginBusiness(ILoginRepo _repo)
        {
            repo = _repo;
        }

        public CommonDBResponse LoginUser(LoginCommon model)
        {
            return repo.LoginUser(model);
        }
    }
}