using Newtonsoft.Json;

namespace TaskManagement.Models
{
    public class EmployeeModel
    {
        
            public int Id { get; set; }

            [JsonProperty("employee_name")]
            public string EmployeeName { get; set; }

            [JsonProperty("employee_salary")]
            public string EmployeeSalary { get; set; }

            [JsonProperty("employee_age")]
            public string EmployeeAge { get; set; }

            [JsonProperty("profile_image")]
            public string ProfileImage { get; set; }
        

    }
    public class EmployeeResponse
    {
        public string Status { get; set; }
        public List<EmployeeModel> Data { get; set; }
        public string Message { get; set; }
    }
}
