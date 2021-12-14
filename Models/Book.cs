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

        public DateTime? CheckOutDate { get; set; }

        public DateTime? DueDate { get; set; }

        [Required]
        public string Status { get; set; }

        public enum EStatus
        {
            [Display(Name="On Shelf")]
            OnShelf = 0,
            [Display(Name="Checked Out")]
            CheckedOut = 1
        }

        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }

        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        public Book()
        {
            CheckOutDate = null;
        }
    }

    
}
