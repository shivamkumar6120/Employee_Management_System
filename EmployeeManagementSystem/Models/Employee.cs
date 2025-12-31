
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]

        public string Email { get; set;  }

        [StringLength (25)]
        public string? Department { get; set;  }

        [Range(0, 10000000)]
        public decimal Salary { get; set;  }
    }
}
