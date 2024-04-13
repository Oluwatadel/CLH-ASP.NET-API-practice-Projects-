namespace CustomerAPiPractice.Models
{
	public class Comment
	{
		public int PostId { get; set; }
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? email { get; set; }
		public string? body { get; set; }
	}



	public class CommentRequest
	{
		public int PostId { get; set; }
		public string? Name { get; set; }
		public string? email { get; set; }
		public string? body { get; set; }
	}
}
