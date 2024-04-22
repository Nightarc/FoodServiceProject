using FoodServiceAPI.DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using static LinqToDB.Reflection.Methods.LinqToDB;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(PostgresDB connection) : ControllerBase
    {
        private readonly PostgresDB _connection = connection;

        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _connection.Customers.ToList();
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return _connection.Customers.Find(id);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public Customer Post([FromBody] Customer customer)
        {
            var customerID = _connection.InsertWithIdentity(customer);

            return _connection.Customers.Find(Convert.ToInt32(customerID));
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public Customer Put(int id, [FromBody] Customer customer)
        {
            //.connection.Customers.InsertOrUpdate()
            _connection.Customers.Where(c => c.CustomerID == id)
                .Set(t => t.Name, customer.Name)
                .Set(t => t.LastAddress, customer.LastAddress)
                .Set(t => t.PhoneNumber, customer.PhoneNumber)
                .Set(t => t.PromotionList, customer.PromotionList)
                .Set(t => t.Email, customer.Email)
                .Update();
            return _connection.Customers.Find(Convert.ToInt32(id)); 
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _connection.Customers.Where(c => c.CustomerID == id).Delete();
        }
    }
}
