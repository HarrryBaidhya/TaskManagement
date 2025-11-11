using Azure;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.MSIdentity.Shared;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
using System.Net;
using TaskManagement.Interface;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    public class OrderController : Controller
    {

        private readonly IProductManagement TRepository;

        public OrderController(IProductManagement product)
        {
            this.TRepository = product;
        }




        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult  AddOrder()
        {
            var OrderStatus = new List<SelectListItem>
    {
        new SelectListItem { Value = "Pending", Text = "Pending" },
        new SelectListItem { Value = "Success", Text = "Success" },
        new SelectListItem { Value = "Hold", Text = "Hold" },
    };

            ViewBag.Status = OrderStatus;


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrder( OrderModel order)
        {
            try
            {
                var newFrnd = TRepository.NewOrder(order);
                return RedirectToAction("OrderDetail");
            }
            catch
            {
                return View();
            }
        }



        public async Task<IActionResult> Orderdetail(DateTime? searchDate, int pageNumber = 1, int pageSize = 10)
        {
            var data = await TRepository.GetOrder(searchDate, pageNumber, pageSize);
            ViewBag.SearchDate = searchDate?.ToString("yyyy-MM-dd");
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            return View(data);
        }

        public async Task<IActionResult> ExportToExcel(DateTime? searchDate)
        {
            var data = await TRepository.GetOrder(searchDate, 1, int.MaxValue);

            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Order");
            worksheet.Cell(1, 1).InsertTable(data);

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Products.xlsx");
        }

        // ---------- Export to PDF ----------
        public async Task<IActionResult> ExportToPdf(DateTime? searchDate)
        {
            var data = await TRepository.GetOrder(searchDate, 1, int.MaxValue);

            using var stream = new MemoryStream();
            var document = new iTextSharp.text.Document();
            var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, stream);

            document.Open();
            document.Add(new iTextSharp.text.Paragraph("Product List"));
            document.Add(new iTextSharp.text.Paragraph($"Filtered by: {searchDate?.ToString("yyyy-MM-dd") ?? "All"}"));

            var table = new iTextSharp.text.pdf.PdfPTable(5);
            table.AddCell("ProductID");
            table.AddCell("OrderDate");
            table.AddCell("SubTotal");
            table.AddCell("CustomerName");
            table.AddCell("OrderStatus");

           
            foreach (var item in data)
            {
                table.AddCell(item.ProductID.ToString());
                table.AddCell(item.OrderDate.ToString());
                table.AddCell(item.SubTotal.ToString());
                table.AddCell(item.CustomerName);
                table.AddCell(item.OrderStatus);
            }

            document.Add(table);
            document.Close();

            return File(stream.ToArray(), "application/pdf", "Products.pdf");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateOrder(int OrderID)
        {
            var dataList = await TRepository.GetOrderById(OrderID); // ✅ Await the task
            var data = dataList.FirstOrDefault();                   // ✅ Now it's IEnumerable<OrderModel>

            if (data == null)
                return NotFound();

         var OrderStatus = new List<SelectListItem>
    {
        new SelectListItem { Value = "Pending", Text = "Pending" },
        new SelectListItem { Value = "Success", Text = "Success" },
        new SelectListItem { Value = "Hold", Text = "Hold" },
    };

            ViewBag.Status = OrderStatus;

            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrder(OrderModel order)
        {
           var updateData=TRepository.UpdateOrder(order);
            return View();
        }

        public IActionResult Employee()
        {
            string Employeedata = GetEmployee();
            var result = JsonConvert.DeserializeObject<EmployeeResponse>(Employeedata);
            List<EmployeeModel> employees = result.Data; // assuming EmployeeModel matches Employee class
            return View(employees);
            return View();  
        }



        public static string GetEmployee()
        {

            try
            {
                string responseStr = "";
                string requestdata = "";
                string ApiName = "http://dummy.restapiexample.com/api/v1/employees";
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

