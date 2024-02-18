using Company.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.Controllers
{
    public class AccountController : Controller
    {
        CompanyContext CompanyContext = new CompanyContext();
        public IActionResult LoginForm()
        {
            if (HttpContext.Session.GetInt32("id") != null)
                return RedirectToAction("Index", "Employee");

            return View();
        }

        public IActionResult Login(string name, string id)
        {
            Employee employee = CompanyContext.Employees.SingleOrDefault(emp=> emp.SSN == int.Parse(id) && emp.FirstName == name);

            if(employee == null) {

                return View("LoginForm");
            }

            HttpContext.Session.SetString("name", employee.FirstName);
            HttpContext.Session.SetInt32("id", employee.SSN);

            return RedirectToAction("Index", "Employee");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("id");
            HttpContext.Session.Remove("name");
            return View("LoginForm");
        }
    }
}
