using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace demohttpcaller.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IConfiguration Configuration;

        public string? ApiResonse { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

        public async Task OnGet()
        {
            try
            {
                HttpClient client = new HttpClient();

                // Send the GET request and get the response
                HttpResponseMessage response = await client.GetAsync(Configuration["TodoServer"]);

                // Ensure the request was successful (status code 2xx)
                response.EnsureSuccessStatusCode();
                response.Headers.Add("Accept", "application/json");

                // Read the response content as a string
                ApiResonse = await response.Content.ReadAsStringAsync();

                _logger.LogInformation("API Response received successfully.");
            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"Request Exception: {e.Message}");
            }
            catch (Exception e)
            {
                _logger.LogError($"An unexpected error occurred: {e.Message}");
            }
        }
    }
}
