using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GeekGardenCase2.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }

        public Role Role { get; set; }
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
    }
}
