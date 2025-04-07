using System.Data;

namespace Blood.domain
{
    public class CommonDBResponse
    {
        public string Extra1;
        public static string Failed { get; set; }
        public static string Success { get; set; }
        public string? Code { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public string? ExtraValue1 { get; set; }
        public string? ExtraValue2 { get; set; }
        public string? ExtraValue3 { get; set; }
        public string? Id { get; set; }

        public void SetMessage(string? code, string? message, string? extra1, string? extra2, string? extra3, string? extra4, string? extra5, DataTable dt)
        {
            throw new NotImplementedException();
        }
    }

    public class CommonDBResponseEncryption
    {
        public string? encryption_key { get; set; }
        public string? code { get; set; }
        public string? message { get; set; }
    }

    public class CommonDBResponseToken
    {
        public string token { get; set; }
    }
}