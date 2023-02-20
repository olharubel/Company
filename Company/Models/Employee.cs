using System.ComponentModel.DataAnnotations;

namespace Company.Models
{
    public class Employee  
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [MinLength(1), MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(1), MaxLength(50)]
        public string Surname { get; set; }

        [Required]
        [MinLength(1), MaxLength(50)]
        public string JobTitle { get; set; }

        [Required]
        public DateTime EmploymentDate { get; set; }

        [Required]
        public double Salary { get; set; }
        public int? HeadId { get; set; }

       
    }
}
