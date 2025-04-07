using Blood.domain.Models.Login;
using Blood.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.Business.LoginUser
{
    public interface ILoginBusiness
    {

        CommonDBResponse LoginUser(LoginCommon model);
    }
}
