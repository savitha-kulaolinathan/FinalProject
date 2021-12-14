using FinalProject.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        


        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> CheckIn(int id)
        {
            
            var book = await _context.Books.FindAsync(id);
            book.DueDate = (DateTime.Now).AddDays(14);
            if (book.Status.Equals("0"))
            {
                book.CheckOutDate = (DateTime.Now);
                var bookInDb = _context.Books.Single(b => b.Id == id);
                bookInDb.CheckOutDate = book.CheckOutDate;
                bookInDb.Status = "1";
                _context.SaveChanges();
                return "Book Checked out successfully. Your due date is " + book.DueDate.ToString(); ;
            }
            else
            {
                return "Book already checked out";
            }
            
        }
    }
}
