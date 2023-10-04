using InMemoryApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public ProductController(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        public ActionResult Index()
        {
            #region SimpleCaching
            ////memory de data var mı?

            ////1.YOL
            ////if (string.IsNullOrWhiteSpace(_memoryCache.Get<string>("zaman")))
            ////{
            ////    //memorye tarih bilgisi cacheleme
            ////    _memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            ////}

            ////if (!_memoryCache.TryGetValue("zaman", out string zamancache))
            ////{
            ////    //memorye tarih bilgisi cacheleme
            ////    _memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            ////    var degsken = zamancache;
            ////}

            ////ömür bilgisi
            //MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            ////options.AbsoluteExpiration = DateTime.Now.AddSeconds(10);

            //options.AbsoluteExpiration = DateTime.Now.AddSeconds(1);
            //options.SlidingExpiration = TimeSpan.FromSeconds(10);
            //options.Priority = CacheItemPriority.NeverRemove;

            //options.RegisterPostEvictionCallback((key, value, reason, state) =>
            //{
            //    //silinecek olan = key
            //    //değeri = value
            //    //silinme sebebi = reason
            //    _memoryCache.Set("callback", $"{key} -> {value} => sebep : {reason}");
            //});

            ////memorye tarih bilgisi cacheleme
            //_memoryCache.Set<string>("zaman", DateTime.Now.ToString(), options);

            #endregion

            Product product = new Product { Id = 1, Name = "Kalem", Price = 200 };

            _memoryCache.Set<Product>("product:1", product);

            return View();
        }

        public IActionResult Show()
        {
            #region SimpleCaching
            ////bu key'a ait değeri almaya çalışır. Yok ise oluşturur.
            ////var olup olmadığını kontrol etmeye gerek kalmıyor.
            ////_memoryCache.GetOrCreate<string>("zaman", entry =>
            ////{
            ////    return DateTime.Now.ToString();
            ////});

            //_memoryCache.TryGetValue("zaman", out string zamancache);
            //_memoryCache.TryGetValue("callback", out string callback);

            //ViewBag.zaman = zamancache;
            //ViewBag.callback = callback;


            ////tarih memorydan data çekme
            ////ViewBag.zaman = _memoryCache.Get<string>("zaman");
            #endregion

            ViewBag.product = _memoryCache.Get<Product>("product:1");

            return View();
        }
    }
}
