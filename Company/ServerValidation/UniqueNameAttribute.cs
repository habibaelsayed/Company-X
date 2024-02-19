using Company.Models;
using Company.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace Company.ServerValidation
{
    public class UniqueNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            EmployeeVM employeeVM = (EmployeeVM)validationContext.ObjectInstance;

            CompanyContext companyContext = new();

            var isUnique = companyContext.Employees.FirstOrDefault(emp => emp.FirstName == employeeVM.FirstName && emp.LastName == employeeVM.LastName && emp.Dno == employeeVM.DeptId);

            if (isUnique != null)
            {
                return new ValidationResult("The name should be unique in this Department.");
            }

            return ValidationResult.Success;

        }
    }
}
