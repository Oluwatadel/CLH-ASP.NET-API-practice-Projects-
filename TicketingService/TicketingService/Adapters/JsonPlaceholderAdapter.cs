using System.Text;
using System.Text.Json;
using TicketingService.Models;

namespace TicketingService.Adapters
{
    public class JsonPlaceholderAdapter
    {
        private readonly HttpClient _httpClient;
        private const string _baseUrl = "https://jsonplaceholder.typicode.com";
        public JsonPlaceholderAdapter()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            try
            {
                var path = $"{_baseUrl}/posts";
                var response = await _httpClient.GetAsync(path);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var posts = JsonSerializer.Deserialize<IEnumerable<Post>>(responseContent, options);
                return posts;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"An error occured while getting posts : {ex.Message}");
                throw;
            }

        }

        public async Task<Post> AddPost(CreatePostRequest postRequest)
        {
            try
            {
                var path = $"{_baseUrl}/posts";
                var contentJson = JsonSerializer.Serialize(postRequest);
                var httpContent = new StringContent(contentJson, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(path, httpContent);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var post = JsonSerializer.Deserialize<Post>(responseContent, options);
                return post;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"An error occured while getting posts : {ex.Message}");
                throw;
            }

        }
    }
}
