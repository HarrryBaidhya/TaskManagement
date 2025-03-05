namespace TaskManagement.Models
{
    public class UserRegModel
    {
        public string UserName { get; set; }    
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public  string Firstname {  get; set; }
        public string Lastname { get; set; }
        public string country { get; set; }
        public string Address {  get; set; }
    }
}
