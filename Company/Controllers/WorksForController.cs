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
            //var EmployeeProjectsGroups = CompanyContext.works_For.Include(wf => wf.employee).Include(wf => wf.project).GroupBy(wf => wf.EmpSSN).ToList();

            //List<object> EmployeeProjects = new List<object>();

            //foreach (var group in EmployeeProjectsGroups)
            //{
            //    EmployeeProjects.Add(new { employee = group.Select(g => g.employee), project = group.Select(g => g.project), hours = group.Select(g => g.Hours) });
            //}



            //foreach (var item in EmployeeProjects)
            //{
            //    var employee = item.GetType().GetProperty("employee").GetValue(item, null);
            //    var projects = (List<Project>)item.GetType().GetProperty("project").GetValue(item, null);
            //    var hours = (List<int>)item.GetType().GetProperty("hours").GetValue(item, null);



            //}

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
    }
}
