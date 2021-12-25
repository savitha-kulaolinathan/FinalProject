using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject
{
    public class SD
    {
        public static string APIBaseUrl = "https://api.itbook.store/1.0/";

        public static string SearchAPIPath = APIBaseUrl + "search/";

        public static string NewAPIPath = APIBaseUrl + "new";

        public static string GetByISBNPath = APIBaseUrl + "books/";
    }
}
