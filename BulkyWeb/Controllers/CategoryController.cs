using Bulky.DataAcess.Data;
using Bulky.Models;
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
            List<Category> ObjCategoriesLst = _db.Categories.OrderBy(i => i.DisplayOrder).ToList();
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
                //ModelState.AddModelError("Name","Display Order and Category Name cannot be same !");
                ModelState.AddModelError("", "Display Order and Category Name cannot be same !");
            }

            if (ModelState.IsValid) {
                _db.Categories.Add(ObjCategory);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully !";
                return RedirectToAction("Index", "Category");
            }

             return View();
        }

        [HttpGet]
        public IActionResult EditCategory(int? id)
        {
            if (id == null || id== 0)
            {
                return NotFound();
            }
            Category? EditCategory = _db.Categories.Find(id);
            //Category? EditCategory1 = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Category? EditCategory2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (EditCategory == null )
            {
                return NotFound();
            }

            return View(EditCategory);
        }

        [HttpPost]
        public IActionResult EditCategory(Category ObjCategory)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(ObjCategory);
                _db.SaveChanges();
                TempData["success"] = "Category Updated Successfully !";
                return RedirectToAction("Index", "Category");
            }

             return View();
        }
        [HttpGet]
        public IActionResult DeleteCategory(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? DeleteCategory = _db.Categories.Find(id);
            //Category? EditCategory1 = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Category? EditCategory2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (DeleteCategory == null)
            {
                return NotFound();
            }

            return View(DeleteCategory);          
        }

        [HttpPost,ActionName("DeleteCategory")]
        public IActionResult DeleteAction(int? id)
        {
            Category? DeleteCategory = _db.Categories.Find(id);

            if (DeleteCategory == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(DeleteCategory);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully !";
            return RedirectToAction("Index", "Category");
        }
    }
}
