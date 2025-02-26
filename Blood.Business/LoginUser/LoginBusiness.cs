using Blood.domain.Models.Login;
using Blood.domain;
using Blood.Infrastructure.LoginInfra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
