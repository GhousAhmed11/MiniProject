using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProject.Models
{
    public class AppUser : MandatoryFields
    {
        [Key]
        public int Id{ get; set; }
        public int Role { get; set; }
        [Required]
        public string Password { get; set; }

        [ForeignKey("Employees")]
        public int? EmployeeId { get; set; }
        public Employees Employees { get; set; }
    }
}
