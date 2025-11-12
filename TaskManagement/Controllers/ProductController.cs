using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using TaskManagement.Interface;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductManagement TRepository;

        public ProductController(IProductManagement product)
        {
            this.TRepository = product;
        }


        [HttpGet]
        public IActionResult Supplier()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Supplier(Supplier supplier)
        {
            try
            {
                var newFrnd = TRepository.NewSupplier(supplier);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public async Task<IActionResult> DetailsSupplier()
        {
            var data = await TRepository.Get();
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }


        //public async Task<IActionResult> ProductDetail()
        //{
        //    var data = await TRepository.GetProduct();
        //    if (data == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(data);
        //}


        public async Task<IActionResult> ProductDetail(DateTime? searchDate, int pageNumber = 1, int pageSize = 10)
        {
            var data = await TRepository.GetProduct(searchDate, pageNumber, pageSize);
            ViewBag.SearchDate = searchDate?.ToString("yyyy-MM-dd");
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            return View(data);
        }



        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(ProductModel Pm)
        {
            try
            {
                var newFrnd = TRepository.NewProduct(Pm);
                return RedirectToAction("ProductDetail");
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> ExportToExcel(DateTime? searchDate)
        {
            var data = await TRepository.GetProduct(searchDate, 1, int.MaxValue);

            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Products");
            worksheet.Cell(1, 1).InsertTable(data);

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Products.xlsx");
        }

        // ---------- Export to PDF ----------
        public async Task<IActionResult> ExportToPdf(DateTime? searchDate)
        {
            var data = await TRepository.GetProduct(searchDate, 1, int.MaxValue);

            using var stream = new MemoryStream();
            var document = new iTextSharp.text.Document();
            var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, stream);

            document.Open();
            document.Add(new iTextSharp.text.Paragraph("Product List"));
            document.Add(new iTextSharp.text.Paragraph($"Filtered by: {searchDate?.ToString("yyyy-MM-dd") ?? "All"}"));

            var table = new iTextSharp.text.pdf.PdfPTable(5);
            table.AddCell("ProductID");
            table.AddCell("ProductName");
            table.AddCell("Price");
            table.AddCell("StockQuantity");
            table.AddCell("CreatedDate");

            foreach (var item in data)
            {
                table.AddCell(item.ProductID.ToString());
                table.AddCell(item.ProductName);
                table.AddCell(item.Price.ToString("0.00"));
                table.AddCell(item.StockQuantity.ToString());
                table.AddCell(item.CreatedDate);
            }

            document.Add(table);
            document.Close();

            return File(stream.ToArray(), "application/pdf", "Products.pdf");
        }

       

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int Productid)
        {
            var dataList = await TRepository.GetProductById(Productid); // ✅ Await the task
            var data = dataList.FirstOrDefault();                   // ✅ Now it's IEnumerable<OrderModel>
            return View(data);
        }



        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductModel model)
        {
            var dataList = await TRepository.UpdateProduct(model);
             return RedirectToAction("ProductDetail");
        }


        public async Task<IActionResult> DeleteProuct(int Productid)
        {


            try
            {
                var edtFrnd = TRepository.DeleteProduct(Productid);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }



    }







