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



         //GET: /Employee/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if(!ModelState.IsValid)
            {
                return View(employee);
            }

            _repository.AddEmployee(employee);
            // PRG Pattern: Post → Redirect → Get
            return RedirectToAction("List");    
        }

        // GET: /Employee/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _repository.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: /Employee/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            _repository.UpdateEmployee(employee);

            return RedirectToAction("List");
        }


        // GET: /Employee/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _repository.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: /Employee/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteEmployee(id);
            return RedirectToAction("List");
        }


    }
}
