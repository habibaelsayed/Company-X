using Company.Models;
using Company.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
                List<Employee> employees = companyContext.Employees.Include(emp=> emp.EmpDept).ToList();

                return View(employees);
            }

            return RedirectToAction("LoginForm", "Account");
            
        }

        public IActionResult Details(int id) { 

            Employee employee = companyContext.Employees.SingleOrDefault(emp=> emp.SSN == id);

            return View(employee);

        }

        public IActionResult Add()
        {
            EmployeeVM employeeVM = new()
            {
                Departments = new SelectList(companyContext.Departments, nameof(Department.Dnum), nameof(Department.Dname))
            };

            return View(employeeVM);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(EmployeeVM employeeVM)
        {
            List<Department> departments = companyContext.Departments.ToList();
            employeeVM.Departments = new SelectList(departments, nameof(Department.Dnum), nameof(Department.Dname));
            if (ModelState.IsValid)
            {
                Employee employee = new()
                {
                    FirstName = employeeVM.FirstName,
                    LastName = employeeVM.LastName,
                    Dno = employeeVM.DeptId,
                    BirthDate = employeeVM.BirthDate,
                    Address = employeeVM.Address,
                    Salary = employeeVM.Salary,
                    Sex = employeeVM.Sex
                };
                companyContext.Employees.Add(employee);
                companyContext.SaveChanges();

                return RedirectToAction("Index");

            }

            return View(employeeVM);
        }

        public IActionResult AddEmpToProj()
        {
            List<Employee> employees = companyContext.Employees.ToList();
            List<Project> projects = companyContext.Projects.ToList();

            ViewBag.Employees = employees;
            ViewBag.Projects = projects;

            return View();

        }

        public IActionResult AddProjects(int empid, int[] projectsid, int hours)
        {
            Employee employee = companyContext.Employees.SingleOrDefault(emp => emp.SSN == empid);
            foreach (int i in projectsid)
            {
                Works_for exists = companyContext.works_For.SingleOrDefault(proj => proj.EmpSSN == empid && proj.Pnum == i);
                if(exists != null)
                {
                    exists.Hours += hours;
                }
                else
                {
                    Works_for works_For = new()
                    {
                        EmpSSN = empid,
                        Pnum = i,
                        Hours = 0
                    };

                    companyContext.works_For.Add(works_For);
                }
                companyContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
