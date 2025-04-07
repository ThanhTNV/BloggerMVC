using Blogger.Domain.Models;

namespace Blogger.Application.Models.CategoryViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; } = -1;

        public string CategoryName { get; set; } = null!;

        public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
    }
}
