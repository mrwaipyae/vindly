using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using vindly.Data;
using vindly.Models;

namespace vindly.Controllers
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
            IEnumerable<Category> objList = _db.Category;
            return View(objList);
        }

        public IActionResult Add()
        {
            return View();
        }

        
        public IActionResult Filter(string query)
        {
            IEnumerable<Category> filteredData;

            if (query == "all")
            {
                // No specific query selected, show all data
                filteredData = _db.Category;
            }
            else if (query == "pmrd")
            {
                // Filter the data based on the 'PM/RD' query and select only the desired columns
                filteredData = _db.Category.Where(category => category.PM > 0 || category.RD > 0)
                                           .Select(category => new Category
                                           {
                                               Id = category.Id,
                                               Name = category.Name,
                                               PM = category.PM,
                                               RD = category.RD
                                           });
            }
            else if (query == "utot")
            {
                // Filter the data based on the 'UT/OT' query and select only the desired columns
                filteredData = _db.Category.Where(category => category.UT > 0 || category.OT > 0)
                                           .Select(category => new Category
                                           {
                                               Id = category.Id,
                                               Name = category.Name,
                                               UT = category.UT,
                                               OT = category.OT
                                           });
            }
            else
            {
                // Invalid query parameter, show all data
                filteredData = _db.Category;
            }

            // Pass the filtered data to your partial view
            return PartialView("_CategoryPartialView", filteredData);

        
        }
        


        /***
        public IActionResult Filter(string query)
        {
            IQueryable<Category> filteredData;

            if (query == "all")
            {
                // No specific query selected, show all data
                filteredData = _db.Category;
            }
            else if (query == "pmrd")
            {
                // Filter the data based on the 'PM/RD' query
                filteredData = _db.Category.Where(category => category.PM > 0 || category.RD > 0);
            }
            else if (query == "utot")
            {
                // Filter the data based on the 'UT/OT' query
                filteredData = _db.Category.Where(category => category.UT > 0 || category.OT > 0);
            }
            else
            {
                // Invalid query parameter, show no data
                filteredData = Enumerable.Empty<Category>().AsQueryable();
            }

            // Pass the filtered data to your partial view
            var filteredCategories = filteredData.Select(category => new Category
            {
                Id = category.Id,
                Name = category.Name,
                PM = category.PM,
                RD = category.RD,
                UT = category.UT,
                OT = category.OT
            }).ToList();

            return PartialView("_CategoryPartialView", filteredCategories);
        }

        ***/

    }
}
