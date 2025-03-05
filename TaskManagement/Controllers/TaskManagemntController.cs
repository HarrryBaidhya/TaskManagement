using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Interface;

namespace TaskManagement.Controllers
{
    public class TaskManagemntController : Controller
    {

        private readonly ITaskManagment TRepository;
        public TaskManagemntController(ITaskManagment product)
        {
            this.TRepository = product;
        }


        public async Task<IActionResult> Index()
        {
            var data = await TRepository.Get();
            return View(data);
        }
        public IActionResult ViewPartial()
        {
            return PartialView("ViewPartial");
        }
        [HttpGet]
        public IActionResult AddTask()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTask(Models.Task Tdata)
        {
            try
            {
                var newFrnd = TRepository.NewTask(Tdata);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public async Task<IActionResult> UpdateTask( int id)
        {
         //   var det = TRepository.GetTaskByID(id);
            return View();
       
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateTask(Models.Task task)
        {

            try
            {
                var edttask = TRepository.UpdateTask(task);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
      
        public IActionResult DeleteTask(int id)
        {
            try
            {
                var edtFrnd = TRepository.DeleteTask(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }
        public async Task<IActionResult> ExportToExcel()
        {
            var tasks = await TRepository.Get(); // Fetch all tasks from the database

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Tasks");

            // Define Headers
            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Title";
            worksheet.Cell(1, 3).Value = "Description";
            worksheet.Cell(1, 4).Value = "Due Date";
            worksheet.Cell(1, 5).Value = "Status";

            // Populate Data
            int row = 2;
            foreach (var task in tasks)
            {
                worksheet.Cell(row, 1).Value = task.Id;
                worksheet.Cell(row, 2).Value = task.Title;
                worksheet.Cell(row, 3).Value = task.Description;
                worksheet.Cell(row, 4).Value = task.DueDate;
                worksheet.Cell(row, 5).Value = task.Status;
                row++;
            }

            // Auto-fit columns for better readability
            worksheet.Columns().AdjustToContents();

            // Save to MemoryStream
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            // Return Excel File as Response
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Tasks.xlsx");
        }


    }
}
