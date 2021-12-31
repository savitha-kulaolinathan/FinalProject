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

        public string Subtitle { get; set; }

        
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public string ISBN13 { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public string Authors { get; set; }

        [Display(Name="More Info URL")]
        public string MoreInfoUrl { get; set; }

        public string Year { get; set; }

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

        public string UserId { get; set; }
        public Book()
        {
            CheckOutDate = null;
        }
    }

    
}
