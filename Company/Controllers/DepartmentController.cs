using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    public class DepartmentController : Controller
    {
        public CompanyContext CompanyContext = new CompanyContext();
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("id") != null)
            {
                List<Department> departments = CompanyContext.Departments.Include(d => d.Manager).ToList();
                return View(departments);
            }
            return RedirectToAction("LoginForm", "Account");
        }

        public IActionResult Details(int id)
        {
            Department department = CompanyContext.Departments.SingleOrDefault(dept => dept.Dnum == id);
            List<Employee> employees = CompanyContext.Employees.ToList();
            List<Employee> Managers = CompanyContext.Departments.Select(emp => emp.Manager).ToList();

            List<Employee> Unmanagers = employees.Except(Managers).ToList();

            ViewBag.Unmanagers = Unmanagers;

            return View(department);
        }

        public IActionResult Update(Department department)
        {
            CompanyContext.Update(department);
            CompanyContext.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult AddForm()
        {
            List<Employee> employees = CompanyContext.Employees.ToList();
            List<Employee> Managers = CompanyContext.Departments.Select(emp => emp.Manager).ToList();

            List<Employee> Unmanagers = employees.Except(Managers).ToList();

            ViewBag.Unmanagers = Unmanagers;
            return View();
        }

        public IActionResult Add(Department department)
        {
            CompanyContext.Add(department);
            CompanyContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Department department = CompanyContext.Departments.FirstOrDefault(dept => dept.Dnum == id);

            CompanyContext.Departments.Remove(department);
            CompanyContext.SaveChanges();
            return RedirectToAction("Index");
        
        }
    }


}
