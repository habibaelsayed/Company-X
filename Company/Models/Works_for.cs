using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    [PrimaryKey("EmpSSN", "Pnum")]
    public class Works_for
    {
        [ForeignKey("employee")]
        public int EmpSSN { get; set; }
        [ForeignKey("project")]
        public int Pnum { get; set; }
        public int Hours { get; set; }

        public Employee employee { get; set; }
        public Project project { get; set; }

    }
}
