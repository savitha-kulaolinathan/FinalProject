using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Book
    {       
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Author { get; set; }
        
        [Display(Name = "Number In Stock")]
        public int NumberInStock { get; set; }

        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }

        [Display(Name = "Genre")]
        public int GenreId { get; set; }
    }
}
