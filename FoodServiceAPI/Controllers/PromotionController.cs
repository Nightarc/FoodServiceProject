using FoodServiceAPI.DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using static LinqToDB.Reflection.Methods.LinqToDB;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController(PostgresDB connection) : ControllerBase
    {
        private readonly PostgresDB _connection = connection;

        // GET: api/<PromotionController>
        [HttpGet]
        public IEnumerable<Promotion> Get()
        {
            return _connection.Promotions.ToList();
        }

        // GET api/<PromotionController>/5
        [HttpGet("{id}")]
        public Promotion Get(int id)
        {
            return _connection.Promotions.Find(id);
        }

        // POST api/<PromotionController>
        [HttpPost]
        public Promotion Post([FromBody] Promotion Promotion)
        {
            var PromotionID = _connection.InsertWithIdentity(Promotion);

            return _connection.Promotions.Find(Convert.ToInt32(PromotionID));
        }

        // PUT api/<PromotionController>/5
        [HttpPut("{id}")]
        public Promotion Put(int id, [FromBody] Promotion Promotion)
        {
            //.connection.Promotions.InsertOrUpdate()
            _connection.Promotions.Where(c => c.PromotionID == id)
                .Set(t => t.Name, Promotion.Name)
                .Set(t => t.Discount, Promotion.Discount)
                .Update();
            return _connection.Promotions.Find(Convert.ToInt32(id));
        }

        // DELETE api/<PromotionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _connection.Promotions.Where(c => c.PromotionID == id).Delete();
        }
    }
}
