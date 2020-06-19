using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BrandReference;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OC.Website.Models;

namespace OC.Website.Controllers
{
    public class BrandsController : Controller
    {
        private readonly Uri uri = new Uri("https://localhost:44359/api/brands");

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            BrandsClient brandsClient = new BrandsClient();

            var brands = await brandsClient.GetAllAsync();

            var result = brands
                .Select(b => new BrandViewModel
                {
                    Id = b.Id,
                    BrandName = b.BrandName,
                    ManufacturerCountry = b.ManufacturerCountry,
                    ProductClass = b.ProductClass,
                    Rating = b.Rating
                }).ToArray();

            await brandsClient.CloseAsync();

            return View(result);
        }

        // GET: Brands/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            BrandsClient brandsClient = new BrandsClient();

            var brand = await brandsClient.GetByIdAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            var result = new BrandViewModel
            {
                Id = brand.Id,
                BrandName = brand.BrandName,
                ManufacturerCountry = brand.ManufacturerCountry,
                ProductClass = brand.ProductClass,
                Rating = brand.Rating
            };

            await brandsClient.CloseAsync();

            return View(result);
        }

        // GET: Brands/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        [HttpPost]
        public async Task<ActionResult> Create(BrandViewModel brand)
        {
            try
            {
                BrandsClient brandsClient = new BrandsClient();

                var brandDto = new BrandDto
                {
                    Id = brand.Id,
                    BrandName = brand.BrandName,
                    ManufacturerCountry = brand.ManufacturerCountry,
                    ProductClass = brand.ProductClass,
                    Rating = brand.Rating
                };

                await brandsClient.CreateAsync(brandDto);

                await brandsClient.CloseAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Brands/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            BrandsClient brandsClient = new BrandsClient();

            var brand = await brandsClient.GetByIdAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            var result = new BrandViewModel
            {
                Id = brand.Id,
                BrandName = brand.BrandName,
                ManufacturerCountry = brand.ManufacturerCountry,
                ProductClass = brand.ProductClass,
                Rating = brand.Rating
            };

            await brandsClient.CloseAsync();

            return View(result);
        }

        // POST: Brands/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, BrandViewModel brand)
        {
            /*if (brand.Id != id) // ako neshto gyrmi iztrij ili promeni if
            {
                return NotFound();
            }
            */
            brand.Id = id;
            try
            {
                BrandsClient brandsClient = new BrandsClient();

                var brandDto = new BrandDto
                {
                    Id = brand.Id,
                    BrandName = brand.BrandName,
                    ManufacturerCountry = brand.ManufacturerCountry,
                    ProductClass = brand.ProductClass,
                    Rating = brand.Rating
                };

                await brandsClient.UpdateAsync(brandDto);

                await brandsClient.CloseAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Brands/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            BrandsClient brandsClient = new BrandsClient();

            var brand = await brandsClient.GetByIdAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            var result = new BrandViewModel
            {
                Id = brand.Id,
                BrandName = brand.BrandName,
                ManufacturerCountry = brand.ManufacturerCountry,
                ProductClass = brand.ProductClass,
                Rating = brand.Rating
            };

            await brandsClient.CloseAsync();

            return View(result);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                BrandsClient brandsClient = new BrandsClient();

                await brandsClient.DeleteAsync(id);
                // TODO: Add delete logic here

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

        [HttpPost]
        public async Task<ActionResult> Search(BrandViewModel brand)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(uri);

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<IEnumerable<BrandViewModel>>(jsonResponse);
                var filteredData = responseData.Where(p => p.BrandName.Contains(brand.BrandName)).ToList();
                TempData["Brands"] = filteredData;

                return View();
            }
        }
    }
}