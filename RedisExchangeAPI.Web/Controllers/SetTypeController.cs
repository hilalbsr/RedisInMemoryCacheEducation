using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    //Redis Set  -> HashSet
    //Tekrardan aynı datayı kaydetmez.
    public class SetTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;

        private string listKey = "hashnames";

        public SetTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(2);
        }

        public IActionResult Index()
        {
            HashSet<string> namesList = new HashSet<string>();

            //key redis'te var mı?
            if (db.KeyExists(listKey))
            {
                db.SetMembers(listKey).ToList().ForEach(x =>
                {
                    namesList.Add(x.ToString());
                });
            }

            return View(namesList);
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            //SlidingExpiration
            //if (db.KeyExists(listKey))
            //{
            //    db.KeyExpire(listKey, DateTime.Now.AddMinutes(5));
            //}

           
            //db.SetRandomMember(name);

            //key'e timeout atanması
            db.KeyExpire(listKey, DateTime.Now.AddMinutes(5));

            db.SetAdd(listKey, name);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteItem(string name)
        {
            await db.SetRemoveAsync(listKey, name);
            return RedirectToAction("Index");
        }
    }
}