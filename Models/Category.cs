using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

    }
}