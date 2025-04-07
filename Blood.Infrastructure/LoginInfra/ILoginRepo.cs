using Blood.domain.Models.Login;
using Blood.domain;

namespace Blood.Infrastructure.LoginInfra
{
    public interface ILoginRepo
    {
        CommonDBResponse LoginUser(LoginCommon common);
    }
}