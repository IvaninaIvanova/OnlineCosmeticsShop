using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CategoryReference;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OC.Website.Models;

namespace OC.Website.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly Uri uri = new Uri("https://localhost:44359/api/categories");

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            CategoriesClient categoriesClient = new CategoriesClient();

            var categories = await categoriesClient.GetAllAsync();

            var result = categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    NameCategory = c.NameCategory,
                    Stars = c.Stars,
                    Subcategory = c.Subcategory,
                    Delivery = c.Delivery
                }).ToArray();

            await categoriesClient.CloseAsync();

            return View(result);
        }

        // GET: Categories/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            CategoriesClient categoriesClient = new CategoriesClient();

            var category = await categoriesClient.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var result = new CategoryViewModel
            {
                Id = category.Id,
                NameCategory = category.NameCategory,
                Stars = category.Stars,
                Subcategory = category.Subcategory,
                Delivery = category.Delivery
            };

            await categoriesClient.CloseAsync();

            return View(result);
        }

        // GET: Categories/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        public async Task<ActionResult> Create(CategoryViewModel category)
        {
            try
            {
                CategoriesClient categoriesClient = new CategoriesClient();

                var categoryDto = new CategoryDto
                {
                    Id = category.Id,
                    NameCategory = category.NameCategory,
                    Stars = category.Stars,
                    Subcategory = category.Subcategory,
                    Delivery = category.Delivery
                };

                await categoriesClient.CreateAsync(categoryDto);

                await categoriesClient.CloseAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Categories/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            CategoriesClient categoriesClient = new CategoriesClient();

            var category = await categoriesClient.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var result = new CategoryViewModel
            {
                Id = category.Id,
                NameCategory = category.NameCategory,
                Stars = category.Stars,
                Subcategory = category.Subcategory,
                Delivery = category.Delivery
            };

            await categoriesClient.CloseAsync();

            return View(result);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CategoryViewModel category)
        {
            category.Id = id;

            try
            {
                CategoriesClient categoriesClient = new CategoriesClient();

                var categoryDto = new CategoryDto
                {
                    Id = category.Id,
                    NameCategory = category.NameCategory,
                    Stars = category.Stars,
                    Subcategory = category.Subcategory,
                    Delivery = category.Delivery
                };

                await categoriesClient.UpdateAsync(categoryDto);

                await categoriesClient.CloseAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Categories/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            CategoriesClient categoriesClient = new CategoriesClient();

            var category = await categoriesClient.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var result = new CategoryViewModel
            {
                Id = category.Id,
                NameCategory = category.NameCategory,
                Stars = category.Stars,
                Subcategory = category.Subcategory,
                Delivery = category.Delivery
            };

            await categoriesClient.CloseAsync();

            return View(result);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                CategoriesClient categoriesClient = new CategoriesClient();

                await categoriesClient.DeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        // POST: Categories/Search
        [HttpPost]
        public async Task<ActionResult> Search(CategoryViewModel category)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(uri);

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<IEnumerable<CategoryViewModel>>(jsonResponse);
                var filteredData = responseData.Where(p => p.NameCategory.Contains(category.NameCategory)).ToList();
                TempData["Categories"] = filteredData;

                return View();
            }
        }
    }
}