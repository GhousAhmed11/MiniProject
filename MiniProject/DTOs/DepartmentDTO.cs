using System.Text.Json.Serialization;

namespace MiniProject.DTOs
{
    public class DepartmentDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
