using Internship.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiApp.Model
{
    public class PersonViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [ForeignKey("Position")]
        public int PositionId { get; set; }
        
        [ForeignKey("Salary")]
        public int SalaryId { get; set; }
        
    }
}
