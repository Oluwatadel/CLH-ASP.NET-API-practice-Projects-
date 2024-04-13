using CustomerAPiPractice.Models;
using System.Text;
using System.Text.Json;

namespace CustomerAPiPractice.Adapter
{
    public class JsonPlaceholderAdapter
    {
        private readonly HttpClient _httpClient;
        private const string _baseUrl = "https://jsonplaceholder.typicode.com";

        public JsonPlaceholderAdapter()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            try
            {
                var path = $"{_baseUrl}/posts";
                var response = await _httpClient.GetAsync(path);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                //this line is to ensure mapping your class with the class been fetch
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var posts = JsonSerializer.Deserialize<IEnumerable<Post>>(responseContent, options);
                return posts;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"An error occured while fetching post : {ex.Message}");
                throw;
            }
        }

        public async Task<Post> AddPostAsync(CreatePostModel createPostModel)
        {
            try
            {
                var path = $"{_baseUrl}/posts";
                var contentJson = JsonSerializer.Serialize(createPostModel);
                var httpContent = new StringContent(contentJson, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(path, httpContent);
                var responseContent = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var post = JsonSerializer.Deserialize<Post>(responseContent, options);
                return post;
            }
            catch(HttpRequestException ex)
            {
                Console.WriteLine($"An error occured while adding post : {ex.Message}");
                throw;
            }
        }

		public async Task<IEnumerable<Comment>> GetCommentsAsync()
		{
			try
			{
				var path = $"{_baseUrl}/comments";
				var response = await _httpClient.GetAsync(path);
				response.EnsureSuccessStatusCode();
				var responseContent = await response.Content.ReadAsStringAsync();
				//this line is to ensure mapping your class with the class been fetch
				var options = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				};
				var comments = JsonSerializer.Deserialize<IEnumerable<Comment>>(responseContent, options);
                return comments;
			}
			catch (HttpRequestException ex)
			{
				Console.WriteLine($"An error occured while fetching post : {ex.Message}");
				throw;
			}
		}



	}
}
