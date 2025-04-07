using AutoMapper;
using Blood.domain.Models.Login;
using TaskManagement.Models;

namespace BloodManagement.Extension
{
    public class AgentProfileMap : Profile
    {
        public AgentProfileMap()
        {

            CreateMap<LoginCommon, AdminUser>().ReverseMap();
        }
    }
}