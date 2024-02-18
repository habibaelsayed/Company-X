using Company.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.Controllers
{
    public class EmployeeController : Controller
    {
        private CompanyContext companyContext;

        public EmployeeController()
        {
            companyContext = new CompanyContext();
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("id") != null)
            {
                List<Employee> employees = companyContext.Employees.ToList();

                return View(employees);
            }

            return RedirectToAction("LoginForm", "Account");
            
        }

        public IActionResult Details(int id) { 

            Employee employee = companyContext.Employees.SingleOrDefault(emp=> emp.SSN == id);

            return View(employee);

        }


    }
}
