using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.ViewModels
{
    public class ApiSearchResultViewModel
    {
        public string title { get; set; }
        public string subtitle { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public string isbn13 { get; set; }
        public string url { get; set; }
    }
}
