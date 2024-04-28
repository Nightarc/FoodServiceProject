using VueAPI.Server.DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VueAPI.Server.Controllers
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
        public Promotion Get(Guid id)
        {
            return _connection.Promotions.Find(id);
        }

        // POST api/<PromotionController>
        [HttpPost]
        public Promotion Post([FromBody] Promotion Promotion)
        {
            Promotion.PromotionID = Guid.NewGuid();
            _connection.Insert(Promotion);

            return _connection.Promotions.Find(Promotion.PromotionID);
        }

        // PUT api/<PromotionController>/5
        [HttpPut("{id}")]
        public Promotion Put(Guid id, [FromBody] Promotion Promotion)
        {
            //.connection.Promotions.InsertOrUpdate()
            _connection.Promotions.Where(c => c.PromotionID == id)
                .Set(t => t.Name, Promotion.Name)
                .Set(t => t.Discount, Promotion.Discount)
                .Update();
            return _connection.Promotions.Find(id);
        }

        // DELETE api/<PromotionController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _connection.Promotions.Where(c => c.PromotionID == id).Delete();
        }
    }
}
