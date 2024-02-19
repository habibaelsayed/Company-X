using Company.ServerValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Company.ViewModel
{
    public class EmployeeVM
    {
        public int SSN { get; set; }
        [Length(2, 50)]
        [UniqueName]
        public string FirstName { get; set; }
        [Length(2, 50)]
     
        public string LastName { get; set; }
        //[Range(typeof(DateOnly), "1955-01-01", "2005-12-31", ErrorMessage = "Date must be in 2005 or earlier.")]
        public DateOnly BirthDate { get; set; }
        [RegularExpression("^(Cairo|Alex|Giza)$", ErrorMessage = "Address Should be from Cairo, Alex or Giza")]
        [Required(ErrorMessage ="*")]
        public string Address { get; set; }
        [RegularExpression("^(Female|Male)$", ErrorMessage = "Sex Should be Male of Female")]
        [Required]
        public string Sex { get; set; }
        [Remote("SalaryRange", "ClientSideValidation")]
        public decimal Salary { get; set; }

        [Compare("ConfirmPassword", ErrorMessage = "Password must match confirm")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Confirm Password must match Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "*")]
        public int? DeptId { get; set; }
        [ValidateNever]
        public SelectList Departments {  get; set; }


    }
}
