using System.ComponentModel.DataAnnotations;

namespace MiniProject.Models
{
    public class Department : MandatoryFields
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? NoOfEmp { get; set; }

    }
}
