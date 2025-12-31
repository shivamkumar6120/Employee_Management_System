using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {

        //Views/Employee/List.cshtml

        public IActionResult List()
        {
            return View();
        }
    }
}
