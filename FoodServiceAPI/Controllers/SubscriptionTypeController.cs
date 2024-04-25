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
        public SubscriptionType Get(Guid id)
        {
            return _connection.SubscriptionTypes.Find(id);
        }

        // POST api/<SubscriptionTypeController>
        [HttpPost]
        public SubscriptionType Post([FromBody] SubscriptionType SubscriptionType)
        {
            SubscriptionType.SubscriptionTypeID = Guid.NewGuid();
            _connection.Insert(SubscriptionType);

            return _connection.SubscriptionTypes.Find(SubscriptionType.SubscriptionTypeID);
        }

        // PUT api/<SubscriptionTypeController>/5
        [HttpPut("{id}")]
        public SubscriptionType Put(Guid id, [FromBody] SubscriptionType SubscriptionType)
        {
            //.connection.SubscriptionTypes.InsertOrUpdate()
            _connection.SubscriptionTypes.Where(c => c.SubscriptionTypeID == id)
                .Set(t => t.Description, SubscriptionType.Description)
                .Set(t => t.OrderTemplate, SubscriptionType.OrderTemplate)
                .Update();
            return _connection.SubscriptionTypes.Find(id);
        }

        // DELETE api/<SubscriptionTypeController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _connection.SubscriptionTypes.Where(c => c.SubscriptionTypeID == id).Delete();
        }
    }
}
