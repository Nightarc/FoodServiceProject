using FoodServiceAPI.DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using static LinqToDB.Reflection.Methods.LinqToDB;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController(PostgresDB connection) : ControllerBase
    {
        private readonly PostgresDB _connection = connection;

        // GET: api/<SubscriptionController>
        [HttpGet]
        public IEnumerable<Subscription> Get()
        {
            return _connection.Subscriptions.ToList();
        }

        // GET api/<SubscriptionController>/5
        [HttpGet("{id}")]
        public Subscription Get(int id)
        {
            return _connection.Subscriptions.Find(id);
        }

        // POST api/<SubscriptionController>
        [HttpPost]
        public Subscription Post([FromBody] Subscription Subscription)
        {
            var SubscriptionID = _connection.InsertWithIdentity(Subscription);

            return _connection.Subscriptions.Find(Convert.ToInt32(SubscriptionID));
        }

        // PUT api/<SubscriptionController>/5
        [HttpPut("{id}")]
        public Subscription Put(int id, [FromBody] Subscription Subscription)
        {
            //.connection.Subscriptions.InsertOrUpdate()
            _connection.Subscriptions.Where(c => c.SubscriptionID == id)
                .Set(t => t.DateStart, Subscription.DateStart)
                .Set(t => t.DateEnd, Subscription.DateEnd)
                .Set(t => t.CustomerID, Subscription.CustomerID)
                .Set(t => t.PaymentID, Subscription.PaymentID)
                .Set(t => t.SubscriptionType, Subscription.SubscriptionType)
                .Update();
            return _connection.Subscriptions.Find(Convert.ToInt32(id));
        }

        // DELETE api/<SubscriptionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _connection.Subscriptions.Where(c => c.SubscriptionID == id).Delete();
        }
    }
}
