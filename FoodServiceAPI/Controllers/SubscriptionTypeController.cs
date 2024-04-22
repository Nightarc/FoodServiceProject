using FoodServiceAPI.DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using static LinqToDB.Reflection.Methods.LinqToDB;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionTypeController(PostgresDB connection) : ControllerBase
    {
        private readonly PostgresDB _connection = connection;

        // GET: api/<SubscriptionTypeController>
        [HttpGet]
        public IEnumerable<SubscriptionType> Get()
        {
            return _connection.SubscriptionTypes.ToList();
        }

        // GET api/<SubscriptionTypeController>/5
        [HttpGet("{id}")]
        public SubscriptionType Get(int id)
        {
            return _connection.SubscriptionTypes.Find(id);
        }

        // POST api/<SubscriptionTypeController>
        [HttpPost]
        public SubscriptionType Post([FromBody] SubscriptionType SubscriptionType)
        {
            var SubscriptionTypeID = _connection.InsertWithIdentity(SubscriptionType);

            return _connection.SubscriptionTypes.Find(Convert.ToInt32(SubscriptionTypeID));
        }

        // PUT api/<SubscriptionTypeController>/5
        [HttpPut("{id}")]
        public SubscriptionType Put(int id, [FromBody] SubscriptionType SubscriptionType)
        {
            //.connection.SubscriptionTypes.InsertOrUpdate()
            _connection.SubscriptionTypes.Where(c => c.SubscriptionTypeID == id)
                .Set(t => t.Description, SubscriptionType.Description)
                .Set(t => t.OrderTemplate, SubscriptionType.OrderTemplate)
                .Update();
            return _connection.SubscriptionTypes.Find(Convert.ToInt32(id));
        }

        // DELETE api/<SubscriptionTypeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _connection.SubscriptionTypes.Where(c => c.SubscriptionTypeID == id).Delete();
        }
    }
}
