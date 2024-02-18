using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Project
    {
        [Key]
        public int Pnum { get; set; }

        public string PName { get; set; }
        public string PLoc { get; set; }
        public string City { get; set; }

        [ForeignKey("ProjDept")]
        public int DeptNum { get; set; }

        public Department ProjDept { get; set; }

        public List<Works_for> works_For { get; set; }
    }
}
