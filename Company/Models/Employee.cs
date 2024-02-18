using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Employee
    {
        [Key]
        public int SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }

        [Column(TypeName = "money")]
        public decimal Salary { get; set; }


        [ForeignKey("Supervisor")]
        public int? SuperSSN { get; set; }

        [ForeignKey("EmpDept")]
        public int? Dno { get; set; }

        [InverseProperty("Employees")]
        public Employee? Supervisor { get; set; }

        public Department? EmpDept { get; set; }

        [InverseProperty("Supervisor")]
        public List<Employee>? Employees { get; set;}

        public Department? ManageDept { get; set; }

        public List<Works_for>? works_For { get; set; }

        public List<Dependant>? dependants { get; set; }
    }
}
