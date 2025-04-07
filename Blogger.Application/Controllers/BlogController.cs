using Blogger.Application.Models;
using Blogger.Domain.Interfaces;
using Blogger.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Blogger.Domain.Models;

using Microsoft.EntityFrameworkCore;
using Blogger.Infrastructure.UnitOfWork.Repository;
using NuGet.Versioning;
using System.Threading.Tasks;
using Blogger.Application.Models.BlogViewModels;

namespace Blogger.Application.Controllers
{
    public class BlogController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Blog> _repo;
        public BlogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = _unitOfWork.Repository<Blog>();
        }
        // GET: BlogController
        public IActionResult Index(int page = 1)
        {
            int pageSize = 10; // Number of blogs per page
            int totalItems = _repo.Count();

            var blogs = _repo.SkipTakeOrderBy(blog => blog.CreatedAt, (page - 1) * pageSize, pageSize).AsEnumerable();
            var blogViewModels = new List<BlogViewModel>();
            foreach (var blog in blogs)
            {
                blogViewModels.Add(new BlogViewModel
                {
                    BlogId = blog.BlogId,
                    BlogReactions = blog.BlogReactions,
                    BlogSummary = blog.BlogSummary,
                    BlogTitle = blog.BlogTitle,
                    Category = blog.Category.CategoryName,
                    Content = blog.Content,
                    CreatedAt = blog.CreatedAt
                });
            }

            var viewModel = new BlogIndexViewModel
            {
                Blogs = blogViewModels,
                Pagination = new PaginationInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = totalItems
                }
            };

            return View(viewModel);
        }

        // GET: BlogController/Details/5
        public async Task<ActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var existingBlog = await _repo.FindAsync(id, cancellationToken);
            if (existingBlog == null)
                return View("NotFound");
            else
                return View(new BlogViewModel
                {
                    BlogId = existingBlog.BlogId,
                    Category = existingBlog.Category.CategoryName,
                    BlogReactions = existingBlog.BlogReactions,
                    BlogSummary = existingBlog.BlogSummary,
                    BlogTitle = existingBlog.BlogTitle,
                    Content = existingBlog.Content,
                    CreatedAt = existingBlog.CreatedAt
                });
        }

        // GET: BlogController/Create
        public async Task<ActionResult> Create(CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.Repository<Category>().GetListAsync(cancellationToken);
            return View(new BlogCreateViewModel
            {
                Categories = categories
            });
        }

        // POST: BlogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BlogController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BlogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BlogController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BlogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
