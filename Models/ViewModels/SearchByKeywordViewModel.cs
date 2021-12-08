using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class SearchByKeywordViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public string SearchKeyword { get; set; }
    }
}
