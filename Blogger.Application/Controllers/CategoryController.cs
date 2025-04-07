using System.Threading.Tasks;
using Blogger.Application.Models.CategoryViewModels;
using Blogger.Domain.Interfaces;
using Blogger.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Application.Controllers
{
    public class CategoryController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Category> _repo;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = _unitOfWork.Repository<Category>();
        }
        // GET: CategoryController
        public ActionResult Index()
        {
            var categories = _repo.QueryableEntities().AsEnumerable();
            List<CategoryViewModel> categoryViewModels = new List<CategoryViewModel>();
            foreach (var category in categories)
            {
                categoryViewModels.Add(new CategoryViewModel
                {
                    Blogs = category.Blogs,
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                });
            }
            return View(categoryViewModels);
        }

        // GET: CategoryController/Details/5
        public async Task<ActionResult> Details(CancellationToken cancellationToken, int? id = -1)
        {
            CategoryViewModel categoryViewModel = new();
            if(id > 0)
            {
                var category = await _repo.FindAsync((int)id, cancellationToken);
                if (category == null)
                    return View("NotFound");
                categoryViewModel.Blogs = category.Blogs;
                categoryViewModel.CategoryId = category.CategoryId;
                categoryViewModel.CategoryName = category.CategoryName;
            }
            return View(categoryViewModel);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection, CancellationToken cancellationToken)
        {
            try
            {
                var CategoryName = collection["CategoryName"];
                if (String.IsNullOrWhiteSpace(CategoryName))
                    return View("Index");
                await _unitOfWork.CreateTransactionAsync(cancellationToken);
                await _repo.InsertAsync(cancellationToken, new Category
                {
                    CategoryName = CategoryName
                });

                await _unitOfWork.SaveAsync(cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                await _unitOfWork.RollbackAsync(cancellationToken);
                return View("Index");
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoryController/Edit/5
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

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoryController/Delete/5
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
