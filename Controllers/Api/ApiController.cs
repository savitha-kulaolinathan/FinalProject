using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Schema;
using FinalProject.Models.Api;
using FinalProject.Models.ViewModels;
using FinalProject.Data;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers.Api
{
    public class ApiController : Controller
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;
        private const int SEARCH_RESULTS_PER_PAGE = 10;
        private const int MAX_RESULTS_RETURNED = 50;
        private const int MAX_PAGE_NUMBER = MAX_RESULTS_RETURNED / SEARCH_RESULTS_PER_PAGE;

        public ApiController(ApplicationDbContext context)
        {
            _client = new HttpClient();
            _context = context;
        }

        public async Task<IActionResult> SearchAPI(int categoryId)
        {
            var category = await _context.Categories.Where(x => x.Id == categoryId).FirstOrDefaultAsync();
            var keyword = category.Name;
            var streamTask = _client.GetStreamAsync(SD.SearchAPIPath + keyword);
            var result = await JsonSerializer.DeserializeAsync<ApiSearchResult>(await streamTask);
            result.keyword = keyword;

            var allBooks = new List<ApiSearchResultViewModel>();

            foreach (var book in result.books)
            {
                book.CategoryId = categoryId;
                allBooks.Add(book);
            }

            if (!result.total.Equals("0"))
            {
                var pageNumber = Int32.Parse(result.page);
                var quit = false;
                var nextResult = new ApiSearchResult();


                while (!quit && pageNumber < MAX_PAGE_NUMBER)
                {
                    streamTask = _client.GetStreamAsync(SD.SearchAPIPath + keyword + "/" + pageNumber);
                    nextResult = await JsonSerializer.DeserializeAsync<ApiSearchResult>(await streamTask);
                    foreach (var book in nextResult.books)
                    {
                        book.CategoryId = categoryId;
                        allBooks.Add(book);
                    }

                    if (Int32.Parse(nextResult.total) == 0)
                    {
                        quit = true;
                    }

                    pageNumber++;
                }

                result.books = allBooks;
            }

            return View("ApiSearchResult", result);

        }

        public async Task<ApiSearchResultViewModel> GetMoreDetailsByISBN(string isbn)
        {
            var streamTask = _client.GetStreamAsync(SD.GetByISBNPath + isbn);
            var result = await JsonSerializer.DeserializeAsync<ApiSearchResultViewModel>(await streamTask);

            return result;

        }
    }
}
