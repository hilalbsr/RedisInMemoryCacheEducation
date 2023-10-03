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
            //memory de data var mı?

            //1.YOL
            //if (string.IsNullOrWhiteSpace(_memoryCache.Get<string>("zaman")))
            //{
            //    //memorye tarih bilgisi cacheleme
            //    _memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            //}

            //if (!_memoryCache.TryGetValue("zaman", out string zamancache))
            //{
            //    //memorye tarih bilgisi cacheleme
            //    _memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            //    var degsken = zamancache;
            //}

            //ömür bilgisi
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            //options.AbsoluteExpiration = DateTime.Now.AddSeconds(10);

            options.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
            options.SlidingExpiration = TimeSpan.FromSeconds(10);

            //memorye tarih bilgisi cacheleme
            _memoryCache.Set<string>("zaman", DateTime.Now.ToString(), options);


            return View();
        }

        public IActionResult Show()
        {
            //bu key'a ait değeri almaya çalışır. Yok ise oluşturur.
            //var olup olmadığını kontrol etmeye gerek kalmıyor.
            //_memoryCache.GetOrCreate<string>("zaman", entry =>
            //{
            //    return DateTime.Now.ToString();
            //});

            _memoryCache.TryGetValue("zaman", out string zamancache);
            ViewBag.zaman = zamancache;

            //tarih memorydan data çekme
            //ViewBag.zaman = _memoryCache.Get<string>("zaman");
            return View();
        }
    }
}
