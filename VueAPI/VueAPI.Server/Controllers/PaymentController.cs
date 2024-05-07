using VueAPI.Server.DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VueAPI.Server.Controllers
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
        public Payment Get(Guid id)
        {
            return _connection.Payments.Find(id);
        }

        // POST api/<PaymentController>
        [HttpPost]
        public Payment Post([FromBody] Payment Payment)
        {
            Payment.PaymentID = Guid.NewGuid();
            Payment.Time = DateTime.Now.ToUniversalTime();
            Payment.Date = DateTime.Now.Date;
            _connection.Insert(Payment);

            return _connection.Payments.Find(Payment.PaymentID);
        }

        // PUT api/<PaymentController>/5
        [HttpPut("{id}")]
        public Payment Put(Guid id, [FromBody] Payment Payment)
        {
            //.connection.Payments.InsertOrUpdate()
            _connection.Payments.Where(c => c.PaymentID == id)
                .Set(t => t.NetPrice, Payment.NetPrice)
                .Set(t => t.PaymentAmount, Payment.PaymentAmount)
                .Set(t => t.Time, Payment.Time)
                .Set(t => t.Date, Payment.Date)
                .Update();
            return _connection.Payments.Find(id);
        }

        // DELETE api/<PaymentController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _connection.Payments.Where(c => c.PaymentID == id).Delete();
        }
    }
}
