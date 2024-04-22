using FoodServiceAPI.DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using static LinqToDB.Reflection.Methods.LinqToDB;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController(PostgresDB connection) : ControllerBase
    {
        private readonly PostgresDB _connection = connection;

        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<Food> Get()
        {
            return _connection.Foods.ToList();
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public Food Get(int id)
        {
            return _connection.Foods.Find(id);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public Food Post([FromBody] Food Food)
        {
            var customerID = _connection.InsertWithIdentity(Food);

            return _connection.Foods.Find(Convert.ToInt32(customerID));
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public Food Put(int id, [FromBody] Food Food)
        {
            _connection.Foods.Where(c => c.FoodID == id)
                .Set(t => t.FoodName, Food.FoodName)
                .Set(t => t.Price, Food.Price)
                .Set(t => t.Components, Food.Components)
                .Set(t => t.Description, Food.Description)
                .Update();
            return _connection.Foods.Find(Convert.ToInt32(id));
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _connection.Foods.Where(c => c.FoodID == id).Delete();
        }
    }
}
