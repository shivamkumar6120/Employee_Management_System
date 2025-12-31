using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Data;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {


        private readonly EmployeeRepository _repository;
        

        public EmployeeController(EmployeeRepository repository)
        {
            _repository = repository;
        }




        //Views/Employee/List.cshtml
        //Action:- Method inside controller
        public IActionResult List()
        {
            var employees = _repository.GetAllEmployees();

            // TEMP DEBUG (VERY IMPORTANT)
            ViewBag.Count = employees.Count;

            return View(employees);
        }

    }
}
