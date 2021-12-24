using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FinalProject.Models.Api;

namespace FinalProject.Controllers.Api
{
    public class ApiController : Controller
    {
        private readonly HttpClient _client; 

        public ApiController(HttpClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> SearchAPI(string keyword)
        {

            var streamTask = _client.GetStreamAsync("https://api.itbook.store/1.0/search/" + keyword);
            var result = await JsonSerializer.DeserializeAsync<ApiSearchResult>(await streamTask);
            result.keyword = keyword;

            return View("ApiSearchResult", result);

        }
    }
}
