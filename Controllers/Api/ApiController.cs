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

namespace FinalProject.Controllers.Api
{
    public class ApiController : Controller
    {
        private readonly HttpClient _client;
        private const int SEARCH_RESULTS_PER_PAGE = 10;
        private const int MAX_RESULTS_RETURNED = 50;
        private const int MAX_PAGE_NUMBER = MAX_RESULTS_RETURNED / SEARCH_RESULTS_PER_PAGE;

        public ApiController()
        {
            _client = new HttpClient();
        }

        public async Task<IActionResult> SearchAPI(string keyword)
        {

            var streamTask = _client.GetStreamAsync(SD.SearchAPIPath + keyword);
            var result = await JsonSerializer.DeserializeAsync<ApiSearchResult>(await streamTask);
            result.keyword = keyword;

            var allBooks = new List<ApiSearchResultViewModel>();

            foreach (var book in result.books)
            {
                allBooks.Add(book);
            }

            var pageNumber = Int32.Parse(result.page);
            var quit = false;  
            var nextResult = new ApiSearchResult();
            

            while (!quit && pageNumber < MAX_PAGE_NUMBER)
            {
                streamTask = _client.GetStreamAsync(SD.SearchAPIPath + keyword + "/" + pageNumber);
                nextResult = await JsonSerializer.DeserializeAsync<ApiSearchResult>(await streamTask);
                foreach (var book in nextResult.books)
                {
                    allBooks.Add(book);
                }

                if (Int32.Parse(nextResult.total) == 0)
                {
                    quit = true; 
                }

                pageNumber++;
            }

            result.books = allBooks;

            return View("ApiSearchResult", result);

        }
    }
}
