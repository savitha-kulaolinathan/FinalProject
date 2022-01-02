using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.ViewModels
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }
        public string StatusMessage { get; set; }

    }
}
