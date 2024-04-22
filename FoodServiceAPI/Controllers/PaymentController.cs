using FoodServiceAPI.DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using static LinqToDB.Reflection.Methods.LinqToDB;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(PostgresDB connection) : ControllerBase
    {
        private readonly PostgresDB _connection = connection;

        // GET: api/<PaymentController>
        [HttpGet]
        public IEnumerable<Payment> Get()
        {
            return _connection.Payments.ToList();
        }

        // GET api/<PaymentController>/5
        [HttpGet("{id}")]
        public Payment Get(int id)
        {
            return _connection.Payments.Find(id);
        }

        // POST api/<PaymentController>
        [HttpPost]
        public Payment Post([FromBody] Payment Payment)
        {
            var PaymentID = _connection.InsertWithIdentity(Payment);

            return _connection.Payments.Find(Convert.ToInt32(PaymentID));
        }

        // PUT api/<PaymentController>/5
        [HttpPut("{id}")]
        public Payment Put(int id, [FromBody] Payment Payment)
        {
            //.connection.Payments.InsertOrUpdate()
            _connection.Payments.Where(c => c.PaymentID == id)
                .Set(t => t.NetPrice, Payment.NetPrice)
                .Set(t => t.PaymentAmount, Payment.PaymentAmount)
                .Set(t => t.Time, Payment.Time)
                .Set(t => t.Date, Payment.Date)
                .Update();
            return _connection.Payments.Find(Convert.ToInt32(id));
        }

        // DELETE api/<PaymentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _connection.Payments.Where(c => c.PaymentID == id).Delete();
        }
    }
}
