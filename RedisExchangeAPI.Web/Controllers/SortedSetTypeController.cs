using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    //Skor verilmesi
    //Skor farklı verilse de aynı itemdan olamaz.Olanın skoru değişir.
    public class SortedSetTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        private string listKey = "sortedsetnames";

        public SortedSetTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(3);
        }

        public IActionResult Index()
        {
            HashSet<string> list = new HashSet<string>();

            if (db.KeyExists(listKey))
            {
                //rediste nasıl bir sıralama varsa ona göre getirir.
                db.SortedSetScan(listKey).ToList().ForEach(x =>
                {
                    list.Add(x.ToString());
                });

                //skora göre sıralı getirilmesi
                db.SortedSetRangeByRank(listKey, 0, 5, order: Order.Descending).ToList().ForEach(x =>
                  {
                      list.Add(x.ToString());
                  });
            }

            return View(list);
        }

        //Skor değerine göre sırlanacak
        [HttpPost]
        public IActionResult Add(string name, int score)
        {
            db.SortedSetAdd(listKey, name, score);
            db.KeyExpire(listKey, DateTime.Now.AddMinutes(1));
            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(string name)
        {
            db.SortedSetRemove(listKey, name);
            return RedirectToAction("Index");
        }
    }
}