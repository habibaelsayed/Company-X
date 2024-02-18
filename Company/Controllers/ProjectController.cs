using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    public class ProjectController : Controller
    {
        public CompanyContext CompanyContext = new CompanyContext();
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("id") != null)
            {
                List<Project> projects = CompanyContext.Projects.Include(p => p.ProjDept).ToList();
                return View(projects);
            }

            return RedirectToAction("LoginForm", "Account");
            
        }

        public IActionResult AddForm()
        {
            List<Department> departments = CompanyContext.Departments.ToList();
            return View(departments);
        }

        public IActionResult Add(string name, string location, string city, int deptId) {

            Project project = new()
            {
                PName = name,
                PLoc = location,
                City = city,
                DeptNum = deptId
            };

            CompanyContext.Projects.Add(project);
            CompanyContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult details(int id)
        {
            List<Department> departments = CompanyContext.Departments.ToList();

            Project project = CompanyContext.Projects.SingleOrDefault(p => p.Pnum == id);

            ViewData["departments"] = departments;

            return View(project);
        }

        public IActionResult Update(int id, string name, string location, string city, int deptId)
        {

            Project project = CompanyContext.Projects.SingleOrDefault(p => p.Pnum == id);

            project.PName = name;
            project.PLoc = location;
            project.City = city;
            project.DeptNum = deptId;

            CompanyContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Project project = CompanyContext.Projects.SingleOrDefault(p => p.Pnum == id);
            CompanyContext.Projects.Remove(project);

            CompanyContext.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
