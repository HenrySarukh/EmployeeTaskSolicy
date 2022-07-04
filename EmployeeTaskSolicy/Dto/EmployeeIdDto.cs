using System.ComponentModel.DataAnnotations;
using EmployeeTaskSolicy.Model;

namespace EmployeeTaskSolicy.Dto
{
    public class EmployeeIdDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be positive number!")]
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(maximumLength:25,MinimumLength = 2)]
        public string LastName { get; set; }
        [Required]
        [Range(16, 65, ErrorMessage = "Age must be more than {1} and less than {2}")]
        public int Age { get; set; }
        [Required]
        [StringLength(maximumLength: 56)]
        public string Country { get; set; }
    }

}
