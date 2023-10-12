using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;

        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(0);
        }

        public IActionResult Index()
        {
            db.StringSet("name", "Hilal Büşra BODUR");
            db.StringSet("ziyaretci", 100);

            return View();
        }

        public IActionResult Show()
        {
            var getValue = db.StringGet("name");
            //Değer var mı
            if (getValue.HasValue)
            {
                //var
                ViewBag.getValue = getValue.ToString();
            }

            //db.StringDecrementAsync("ziyaretci", 1).Wait();//10 azalması
            //db.StringIncrementAsync("ziyaretci", 10).Wait(); //10 artması

            //var getRange = db.StringGetRange("name",0,3);
            //ViewBag.getRange = getRange.ToString();


            var value = db.StringLength("name");

            // db.StringIncrement("ziyaretci", 10);
            // var count = db.StringDecrementAsync("ziyaretci", 1).Result;

            ViewBag.value = value.ToString();

            return View();
        }
    }
}