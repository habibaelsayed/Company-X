using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    public class WorksForController : Controller
    {
        public CompanyContext CompanyContext = new CompanyContext();
        public IActionResult Index()
        {

            if (HttpContext.Session.GetInt32("id") != null)
            {
                var worksFor = CompanyContext.works_For.Include(wf => wf.employee).Include(wf => wf.project).GroupBy(wf => wf.EmpSSN).ToList();


                List<object> list = new List<object>();

                foreach (var work in worksFor)
                {
                    List<object> projects = new List<object>();

                    foreach (var project in work)
                    {
                        if (project.Hours < 5)
                            projects.Add(new { project.project, project.Hours, project.Pnum, color = "red" });
                        else
                            projects.Add(new { project.project, project.Hours, project.Pnum ,color = "green" });
                    }
                    Employee employee = CompanyContext.Employees.SingleOrDefault(emp => emp.SSN == work.Key);
                    list.Add(new { employee, projects });
                }


                return View(list);
            }

            return RedirectToAction("LoginForm", "Account");

            

        }

        public IActionResult EmpProjDetails(int id)
        {
            List<Works_for> works_s = CompanyContext.works_For.Where(wf=> wf.EmpSSN == id).Include(wf=>wf.project).Include(wf=>wf.employee).ToList();

            return View(works_s);

        }

        public IActionResult ProjectDetails(int id)
        {
            List<Works_for> employees = CompanyContext.works_For.Where(wf => wf.Pnum == id).Include(wf => wf.project).Include(wf => wf.employee).ToList();

            return View(employees);
        }


        public IActionResult ProjectHoursDetails()
        {

            List<Employee> employees = CompanyContext.Employees.ToList();
            ViewBag.Employees = employees;

            List<Project> projects = CompanyContext.Projects.ToList();
            ViewBag.Projects = projects;

            //List<Works_for> works = CompanyContext.works_For.Include(wf=> wf.employee).Include(wf=>wf.project).ToList();
 

            return View();
        }

        public IActionResult GetProjects(int id)
        {
            List<Project> projects = CompanyContext.works_For.Where(wf => wf.EmpSSN == id).Select(wf => wf.project).ToList();

            return PartialView("_EmployeeProjectsPartial", projects);
        }

        public IActionResult GetHours(int projId, int empId)
        {
            var worksFor = CompanyContext.works_For.FirstOrDefault(wf => wf.Pnum == projId && wf.EmpSSN == empId);
            int hours = worksFor.Hours;
            return PartialView("_ProjectsHoursPartial", hours);
        }
    }
}

