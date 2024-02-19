using Microsoft.AspNetCore.Mvc;

namespace Company.Controllers
{
    public class ClientSideValidationController : Controller
    {
        public IActionResult SalaryRange(decimal salary)
        {
            if (salary >= 15000 && salary <= 50000)
                return Json(true);
            return Json(false);
        }
    }
}
