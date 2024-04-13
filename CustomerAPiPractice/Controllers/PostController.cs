using CustomerAPiPractice.Adapter;
using CustomerAPiPractice.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPiPractice.Controllers
{
	[ApiController]
	[Route("api/post")]

	public class PostController : ControllerBase
    {
		private readonly JsonPlaceholderAdapter _jsonplaceholder;


        public PostController(JsonPlaceholderAdapter jsonplaceholder)
        {
            _jsonplaceholder = jsonplaceholder;
        }


		[HttpGet]
		//[Route("posts")]
		public async Task<IActionResult> GetPosts()
		{
			try
			{
				var posts = await _jsonplaceholder.GetPostsAsync();
				return Ok(posts);
			}
			catch (Exception ex)
			{
				return new JsonResult("Sorry, unexpected error while retrieving posts")
				{
					StatusCode = 500,
				};
			}

		}
		
		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetPosts([FromRoute] int id)
		{
			try
			{
				var posts = await _jsonplaceholder.GetPostsAsync();
				var post = posts.FirstOrDefault(a => a.Id == id);
				return Ok(post);

			}
			catch (Exception ex)
			{
				return new JsonResult("Sorry, unexpected error while retrieving posts")
				{
					StatusCode = 500,
				};
			}

		}



		[HttpPost]
		//[Route("posts")]
		public async Task<IActionResult> AddPost([FromBody] CreatePostModel createPostModel)
		{
			try
			{
				var post = await _jsonplaceholder.AddPostAsync(createPostModel);
				return Ok(post);
			}
			catch (Exception ex)
			{
				return new JsonResult("Sorry, unexpected error while retrieving posts")
				{
					StatusCode = 500,
				};
			}
		}

		[HttpGet]
		[Route("comments")]
		public async Task<IActionResult> Comments([FromQuery] int id)
		{
			var comments = await _jsonplaceholder.GetCommentsAsync();
			var comment = comments.FirstOrDefault(a => a.Id == id);
			if (comment != null) return Ok(comment);
			return Ok(comments);
		}
		
		[HttpGet]
		[Route("/{id}/comments")]
		public async Task<IActionResult> Comment([FromRoute] int id)
		{
			var post = await _jsonplaceholder.GetCommentsAsync();
			var comment = post.FirstOrDefault(a => a.Id == id);

			return Ok(comment);
		}
	}
}
