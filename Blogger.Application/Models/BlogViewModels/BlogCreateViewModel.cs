using Blogger.Domain.Models;

namespace Blogger.Application.Models.BlogViewModels
{
    public class BlogCreateViewModel
    {
        public virtual IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }
}
