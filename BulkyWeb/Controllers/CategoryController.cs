using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> ObjCategoriesLst = _db.Categories.ToList();
            return View(ObjCategoriesLst);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category ObjCategory)
        {
            if (ObjCategory.Name == ObjCategory.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name","Display Order and Category Name cannot be same");
            }

            if (ModelState.IsValid) {
                _db.Categories.Add(ObjCategory);
                _db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            else
            {
                return View();
            }
        }
    }
}
