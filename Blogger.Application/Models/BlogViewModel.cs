namespace Blogger.Application.Models
{
    public class BlogViewModel
    {
        public int BlogId { get; set; }

        public string BlogTitle { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public string? BlogSummary { get; set; }

        public string Content { get; set; } = null!;
    }
}
