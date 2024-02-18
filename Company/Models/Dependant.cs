using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Company.Models
{
    public class Dependant
    {
        [ForeignKey("Employee")]
        public int EmpSSN { get; set; }
        public string DependantName { get; set; }

        public string Sex { get; set; }
        public DateOnly BirthDate { get; set; }

        public Employee? Employee { get; set; }
    }
}
