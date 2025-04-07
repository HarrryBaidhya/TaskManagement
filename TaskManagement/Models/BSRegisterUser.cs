namespace BloodManagement.Models
{
    public class BSRegisterUser
    {
        public string MemberID { get; set; }
        public string DeviceId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string AgentID { get; set; }
        public string Password { get; set; }
        public string UserID { get; set; }
        public string MiddleName { get; set; }
        public string ActionUser { get; set; }
        public string IpAddress { get; set; }
       
        public string contact { get; set; }
    }
        
    public class Login
    {

        public string Email { get; set; }
        public string Password { get; set; }
    }
}