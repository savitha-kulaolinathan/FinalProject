using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Controllers.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Models.ViewModels;

namespace FinalProject.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Books.Include(b => b.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        [HttpGet]
        public IActionResult Create()
        {
            var newBook = new Book();
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", newBook.CategoryId);
            return View();
        }


        //POST: Books/Create
        [HttpPost]
        public async Task<IActionResult> Create(string isbn13, int categoryId)
        {
            var newBook = new Book();

            var apiController = new ApiController(_context);

            var bookVM = await apiController.GetMoreDetailsByISBN(isbn13);


            if (bookVM != null)
            {
                var book = new Book()
                {
                    Title = bookVM.title,
                    Subtitle = bookVM.subtitle,
                    Description = bookVM.desc,
                    Image = bookVM.image,
                    MoreInfoUrl = bookVM.url,
                    ISBN13 = bookVM.isbn13,
                    Authors = bookVM.authors,
                    Year = bookVM.year,
                    CategoryId = categoryId,
                    Status = "0"
                };

                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", newBook.CategoryId);
            return View();
        }

    

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Subtitle,ISBN13,MoreInfoURL,Authors,Year,Status,CategoryId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }


        //GET: Books/SearchByKeyword/String
        public async Task<IActionResult> SearchByKeyword(string keyword)
        {
            var books = await _context.Books.Include(b => b.Category)
                .Where(b =>b.Title.Contains(keyword) || b.Category.Name.Contains(keyword)).ToListAsync();

            var viewModel = new SearchByKeywordViewModel()
            {
                Books = books,
                SearchKeyword = keyword
            };

            return View(viewModel);

        }


    }
}
