using Blogger.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Application.Controllers
{
    public class BlogController : Controller
    {
        // GET: BlogController
        public ActionResult Index()
        {
            var blogs = Array.Empty<BlogViewModel>(); 
            return View(blogs);
        }

        // GET: BlogController/Details/5
        public ActionResult Details(int id)
        {
            // In a real app, you'd retrieve the blog post from a database using the id
            // For now, we'll simulate finding a blog post
            var blog = new BlogViewModel
            {
                BlogId = id,
                BlogTitle = "Sample Blog Post",
                CreatedAt = DateTime.Now,
                BlogSummary = "This is a summary of the blog post.",
                Content = "<p>This is the full content of the blog post with <strong>HTML formatting</strong>.</p>"
            };

            return View(blog);
        }

        // GET: BlogController/Create
        public ActionResult Create()
        {
            return View();
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
