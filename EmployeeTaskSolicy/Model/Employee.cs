using System.ComponentModel.DataAnnotations;

namespace EmployeeTaskSolicy.Model
{
    public class Employee
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be positive number!")]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Range(16, 65, ErrorMessage = "Age must be more than {1} and less than {2}")]
        public int Age { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
