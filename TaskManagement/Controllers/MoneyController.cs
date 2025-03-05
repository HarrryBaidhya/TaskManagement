using Azure;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Data.Entity.Core.Metadata.Edm;
using System.Net;
using System.Text.Json;
using TaskManagement.Interface;
using TaskManagement.Models;
using static TaskManagement.Models.MoneyModel;

namespace TaskManagement.Controllers
{
    public class MoneyController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITaskManagment TRepository;
        public MoneyController(ITaskManagment product, IHttpContextAccessor httpContextAccessor)
        {
            this.TRepository = product;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
           
            return View();
        }
        public IActionResult Paymentdetail(MoneyModel.ReciveDetails recive)
        {
            string exchange = NRBExchange();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Allows case-insensitive deserialization
            };

            Payload pay = new Payload();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(exchange, options);
            foreach (var payload in apiResponse.Data.Payload)
            {
                foreach (var rate in payload.Rates)
                {
                    if (rate.Currency.Iso3 == "MYR")
                    {
                        var amount = Convert.ToDouble(rate.Sell) * recive.TransferAmount;
                        recive.Payamount = amount;
                        recive.ExchangeRate = rate.Sell;
                    }
                }
            }


            return View(recive);
        }


        public IActionResult SavePaymentdetail(MoneyModel.ReciveDetails recive)
        {


            try
            {
                //  recive.FirstName = HttpContext.Session.GetString(Sessionmodel.sess);
                //var httpContext = _httpContextAccessor.HttpContext;
                ////Sessionmodel sessionmodel = new Sessionmodel();
                //var sessionmodel = new Sessionmodel
                //{
                //    sessionFirstname = "Hari", // Ensure this is not null
                //    SessionCountry = "Myanmar"     // Ensure this is not null
                //};

                //HttpContext.Session.SetString(sessionmodel.sessionFirstname, "Hari");
                //HttpContext.Session.SetString(sessionmodel.SessionCountry, "Myanmar");

                //recive.FirstName = HttpContext.Session.GetString(sessionmodel.sessionFirstname);
                //recive.Country= HttpContext.Session.GetString(sessionmodel.SessionCountry);

                recive.SenderName = "Harry";
                recive.SenderCountry = "Myanmar";
                recive.SenderLastName = "chaudhary";


                var edttask = TRepository.AddRemit(recive);
                return RedirectToAction("ReportABC");
            }
            catch
            {
                return View();
            }
            
        }

        public async Task<IActionResult> ReportABC()
        {
            
            var data = await TRepository.GetReport();

            return View(data);
            //return View();
             
        }




        public static string NRBExchange ()
        {

            try
            {
                string responseStr = "";
                string requestdata = "";
                string ApiName = "https://www.nrb.org.np/api/forex/v1/rates?page=1&per_page=5&from=2024-06-12&to=2024-06-12";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(ApiName);
                byte[] bytes = null;
                bytes = System.Text.Encoding.ASCII.GetBytes(requestdata);
                request.ContentType = "application/json";
                request.ContentLength = bytes.Length;
                request.Method = "Get";
                request.KeepAlive = false;
                request.Timeout = System.Threading.Timeout.Infinite;
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
              //  request.Headers["Authorization"] = CableCarAuthentication;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        responseStr = new StreamReader(stream).ReadToEnd();
                        stream.Flush();
                        stream.Close();
                    }
                }
                return responseStr;


            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        return text;  
                    }
                }
            }
           

        }



    }
}
