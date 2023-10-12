using IDistributedCacheRedis.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace IDistributedCacheRedis.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IDistributedCache _distributedCache;

        public ProductsController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<IActionResult> Index()
        {

            #region IDistributedCache-2 (Complex Type’ların cachlenmesi) Kaydedilmesi
            ////binary yada JSON serilaze edilmeli

            //DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions
            //{
            //    AbsoluteExpiration = DateTime.Now.AddMinutes(2)
            //};

            //Product product = new Product { Id = 1, Name = "Kalem", Price = 200 };
            //string jsonProduct = JsonConvert.SerializeObject(product);

            ////JsonSeriliaze
            ////await _distributedCache.SetStringAsync("product:1", jsonProduct, cacheEntryOptions);


            ////Byte
            //Byte[] byteProduct = Encoding.UTF8.GetBytes(jsonProduct);
            //await _distributedCache.SetAsync("product:1", byteProduct, cacheEntryOptions);


            #endregion

            #region IDistributedCache-1 (Basit dataların kaydedilmesi) Kaydedilmesi

            //DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions
            //{
            //    AbsoluteExpiration = DateTime.Now.AddMinutes(1)
            //};

            //_distributedCache.SetString("name", "Fatih", cacheEntryOptions);

            #endregion

            return View();
        }

        public IActionResult Show()
        {
            #region IDistributedCache-2 (Complex Type’ların cachlenmesi) --Okunması

            Byte[] byteProduct = _distributedCache.Get("product:1");
            string jsonProduct = Encoding.UTF8.GetString(byteProduct);

            //JsonSeriliaze
            //string jsonProduct = _distributedCache.GetString("product:1");

            Product product = JsonConvert.DeserializeObject<Product>(jsonProduct);
            ViewBag.product = product;

            #endregion

            #region IDistributedCache-1 (Basit dataların kaydedilmesi) --Okunması

            //ViewBag.name = _distributedCache.GetString("name");

            #endregion

            return View();
        }

        public IActionResult Remove()
        {

            #region IDistributedCache-1 (Basit dataların kaydedilmesi) --Silinmesi

            //_distributedCache.Remove("name");

            #endregion

            return View();
        }
        #region IDistributedCache-3 (Dosyaların cache’lenmesi) Dosyaların(image,pdf) cache'lenmesi

        public IActionResult ImageCache()
        {
            //binary yada JSON serilaze edilmeli

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/download.jpg");
            Byte[] imageByte = System.IO.File.ReadAllBytes(path);

            _distributedCache.Set("foto", imageByte);

            return View();
        }

        public IActionResult ImageUrl()
        {
            Byte[] imageByte = _distributedCache.Get("foto");
            return File(imageByte, "image/jpg");
        }


        #endregion

    }
}
