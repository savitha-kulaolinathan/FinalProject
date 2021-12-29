using FinalProject.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace FinalProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public UsersController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IEmailSender emailsender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailsender;
        }


        public async Task<string> CheckIn(int id)
        {
            var book = await _context.Books.FindAsync(id);
            var user = await _userManager.GetUserAsync(User);
            var email = user.Email;
            var userId = user.Id;
            var title = book.Title;
            
            book.DueDate = (DateTime.Now).AddDays(14);
            if (book.Status.Equals("0"))
            {
                book.CheckOutDate = (DateTime.Now);
                var bookInDb = _context.Books.Single(b => b.Id == id);
                bookInDb.CheckOutDate = book.CheckOutDate;
                bookInDb.UserId = userId;
                bookInDb.Status = "1";
                _context.SaveChanges();
                var message = title + " checkedout successfully";
                await _emailSender.SendEmailAsync(user.Email, "Book Checked out",message);
                return "Book checked out successfully. Your due date is " + book.DueDate.Value.ToShortDateString()+ "<br /><br />" 
                    + "Email sent to "+user.Email+" successfully. Click Refresh to continue."; 

            }
            else
            {
                return "Book already checked out";
            }
        }
        public async Task<IActionResult> CheckoutBooks()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            var books = await _context.Books.Where(b => b.UserId.Contains(userId)).ToListAsync();
            var sortedbooks=books.OrderBy(b=>b.DueDate);
            return View(sortedbooks);
        }
        public async Task<String> Return(int id)
        {
            var book = await _context.Books.FindAsync(id);
            var user = await _userManager.GetUserAsync(User);
            var email = user.Email;
            var userId = user.Id;

            if (book.Status.Equals("1"))
            {

                var bookInDb = _context.Books.Single(b => b.Id == id);
                bookInDb.CheckOutDate = null;
                bookInDb.UserId = null;
                bookInDb.DueDate = null;
                bookInDb.Status = "0";
                _context.SaveChanges();
               
            }
            return "Book returned successfully.";

        }
    }
}
