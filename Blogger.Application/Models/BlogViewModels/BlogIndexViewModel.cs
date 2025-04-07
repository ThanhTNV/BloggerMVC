namespace Blogger.Application.Models.BlogViewModels
{
    public class BlogIndexViewModel
    {
        public IEnumerable<BlogViewModel> Blogs { get; set; } = new List<BlogViewModel>();
        public PaginationInfo Pagination { get; set; } = new PaginationInfo();
    }

    public class PaginationInfo
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}
