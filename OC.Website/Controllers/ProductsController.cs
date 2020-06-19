using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OC.Website.Models;

namespace OC.Website.Controllers
{
    public class ProductsController : Controller
    {
        private const string JSON_MEDIA_TYPE = "application/json";
        private const string AUTHORIZATION_HEADER_NAME = "Authorization";
        private readonly Uri tokenUri = new Uri("https://localhost:44359/api/login");
        private readonly Uri productsUri = new Uri("https://localhost:44359/api/products");
        private readonly Uri categoriesUri = new Uri("https://localhost:44359/api/categories");
        private readonly Uri brandsUri = new Uri("https://localhost:44359/api/brands");

        // GET: Products
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                var token = await GetAccessToken();
                client.DefaultRequestHeaders.Add(AUTHORIZATION_HEADER_NAME, token);

                HttpResponseMessage response = await client.GetAsync(productsUri);

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<IEnumerable<ProductViewModel>>(jsonResponse);

                return View(responseData);
            }
        }

        // GET: Products/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                var token = await GetAccessToken();
                client.DefaultRequestHeaders.Add(AUTHORIZATION_HEADER_NAME, token);

                HttpResponseMessage response = await client.GetAsync($"{productsUri}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<ProductViewModel>(jsonResponse);

                return View(responseData);
            }
        }

        // GET: Products/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ViewBag.Categories = await GetGenresDropdownItemsAsync();
            ViewBag.Brands = await GetDirectorsDropdownItemsAsync();

            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public async Task<ActionResult> Create(ProductViewModel movie)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var token = await GetAccessToken();
                    client.DefaultRequestHeaders.Add(AUTHORIZATION_HEADER_NAME, token);

                    var serializedContent = JsonConvert.SerializeObject(movie);
                    var stringContent = new StringContent(serializedContent, Encoding.UTF8, JSON_MEDIA_TYPE);

                    HttpResponseMessage response = await client.PostAsync(productsUri, stringContent);

                    if (!response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(HomeController.Error), "Home");
                    }

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Error), "Home");
            }
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                var token = await GetAccessToken();
                client.DefaultRequestHeaders.Add(AUTHORIZATION_HEADER_NAME, token);

                HttpResponseMessage response = await client.GetAsync($"{productsUri}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<ProductViewModel>(jsonResponse);


                ViewBag.Categories = await GetGenresDropdownItemsAsync();
                ViewBag.Brands = await GetDirectorsDropdownItemsAsync();


                return View(responseData);
            }
        }

        // POST: Products/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, ProductViewModel movie)
        {
            movie.Id = id;

            try
            {
                using (var client = new HttpClient())
                {
                    var token = await GetAccessToken();
                    client.DefaultRequestHeaders.Add(AUTHORIZATION_HEADER_NAME, token);

                    var serializedContent = JsonConvert.SerializeObject(movie);
                    var stringContent = new StringContent(serializedContent, Encoding.UTF8, JSON_MEDIA_TYPE);

                    HttpResponseMessage response = await client.PutAsync($"{productsUri}/{id}", stringContent);

                    if (!response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(HomeController.Error), "Home");
                    }

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Error), "Home");
            }
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                var token = await GetAccessToken();
                client.DefaultRequestHeaders.Add(AUTHORIZATION_HEADER_NAME, token);

                HttpResponseMessage response = await client.GetAsync($"{productsUri}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<ProductViewModel>(jsonResponse);

                return View(responseData);
            }
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var token = await GetAccessToken();
                    client.DefaultRequestHeaders.Add(AUTHORIZATION_HEADER_NAME, token);

                    HttpResponseMessage response = await client.DeleteAsync($"{productsUri}/{id}");

                    if (!response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(HomeController.Error), "Home");
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Error), "Home");
            }
        }

        // GET: Products/Search
        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Search(ProductViewModel product)
        {
            using (var client = new HttpClient())
            {
                var token = await GetAccessToken();
                client.DefaultRequestHeaders.Add(AUTHORIZATION_HEADER_NAME, token);

                HttpResponseMessage response = await client.GetAsync(productsUri);

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<IEnumerable<ProductViewModel>>(jsonResponse);
                var filteredData = responseData.Where(p => p.ProductName.Contains(product.ProductName)).ToList();
                TempData["Products"] = filteredData;

                return View();
            }
        }

        private async Task<IEnumerable<SelectListItem>> GetGenresDropdownItemsAsync()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage genresResponse = await client.GetAsync(categoriesUri);

                if (!genresResponse.IsSuccessStatusCode)
                {
                    return Enumerable.Empty<SelectListItem>();
                }

                string genresJsonResponse = await genresResponse.Content.ReadAsStringAsync();

                var genres = JsonConvert.DeserializeObject<IEnumerable<CategoryViewModel>>(genresJsonResponse);

                return genres.Select(genre => new SelectListItem($"{genre.NameCategory} {genre.Subcategory} {genre.Delivery} {genre.Stars}", genre.Id.ToString()));
            }
        }

        private async Task<IEnumerable<SelectListItem>> GetDirectorsDropdownItemsAsync()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage directorsResponse = await client.GetAsync(brandsUri);

                if (!directorsResponse.IsSuccessStatusCode)
                {
                    return Enumerable.Empty<SelectListItem>();
                }

                string directorsJsonResponse = await directorsResponse.Content.ReadAsStringAsync();

                var directors = JsonConvert.DeserializeObject<IEnumerable<BrandViewModel>>(directorsJsonResponse);

                return directors.Select(director => new SelectListItem($"{director.BrandName} {director.ManufacturerCountry} {director.ProductClass} {director.Rating}" , director.Id.ToString()));
            }
        }

        private async Task<string> GetAccessToken()
        {
            using (var client = new HttpClient())
            {
                var serializedContent = JsonConvert.SerializeObject(new { Username = "test1Username", Password = "test1Password" });
                var stringContent = new StringContent(serializedContent, Encoding.UTF8, JSON_MEDIA_TYPE);

                HttpResponseMessage response = await client.PostAsync(tokenUri, stringContent);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                return $"Bearer {await response.Content.ReadAsStringAsync()}";
            }
        }
    }
}