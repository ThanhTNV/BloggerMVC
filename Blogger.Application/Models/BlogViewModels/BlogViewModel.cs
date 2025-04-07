using Blogger.Domain.Models;

namespace Blogger.Application.Models.BlogViewModels
{
    public class BlogViewModel
    {
        public int BlogId { get; set; }

        public string BlogTitle { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public string? BlogSummary { get; set; }

        public string Content { get; set; } = null!;
        public string Category { get; set; } = null!;

        public virtual ICollection<BlogReaction> BlogReactions { get; set; } = new List<BlogReaction>();
    }
}
