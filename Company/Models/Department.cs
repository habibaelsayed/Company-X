using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Department
    {
        [Key]
        public int Dnum { get; set; }

        [Display(Name ="Department Name")]
        public string Dname { get; set; }


        [ForeignKey("Manager")]
        public int? MGRSSN { get; set; }

        [Display(Name ="Start Date")]
        public DateOnly MGRStartDate { get; set; }


        [InverseProperty("ManageDept")]
        public Employee? Manager { get; set; }

        public List<Project> Projects { get; set; }


        [InverseProperty("EmpDept")]
        public List<Employee>? Employees { get; set; }
    }
}
