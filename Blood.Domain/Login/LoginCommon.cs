using Blood.domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Xml.Linq;

namespace Blood.domain.Models.Login
{
    public class LoginCommon:CommonDBResponse
    {

        public int AId { get; set; }
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public string session_id { get; internal set; } = "";
        public int Code { get; internal set; }
        public string Message { get; set; } = "";
        public string? Flag { get; set; }
        public string? actionIp { get; set; } 
        public string? FirstTimeLogin { get; set; }
        public string? FullName { get; set; }
        public string? UserId { get; set; }
        public string? RoleId { get; set; }
        public string? agentLogo { get; set; }
        public string? SessionId { get; set; }
        public string? KycStatus { get; set; }
        public string? createdLocalDate { get; set; }
        public string? isPasswordForceful { get; set; }
        public string? isMpinForceful { get; set; }
        public string? isEmailVerified { get; set; }
        public string? isemailverificationStatus { get; set; }
        public string? userEmail { get; set; }
        public string? MobileNo { get; set; }
        public string DeviceId { get; set; }
    }
    public class LoginCommonApi
    {
        public string? user_login_id { get; set; }
        public string? created_platform { get; set; }
        public string? version_id { get; set; }
        public string? ipAddress { get; set; }
        public string? info { get; set; }
        public string? password { get; set; }

    }
    public class DeviceInfo
    {        
        public string? device_name { get; set; }        
        public string? os_version { get; set; }      
        public string? model { get; set; }  
        public string? device_id { get; set; }
    }
    public class ApiCommonRequest
    {
        public string user_login_id { get; set; }
        public string created_platform { get; set; }
        public string product_id { get; set; }
    }
}
