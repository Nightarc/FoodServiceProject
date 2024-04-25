using FoodServiceAPI.DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using static LinqToDB.Reflection.Methods.LinqToDB;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController(PostgresDB connection) : ControllerBase
    {
        private readonly PostgresDB _connection = connection;

        // GET: api/<OrderDetailController>
        [HttpGet]
        public IEnumerable<OrderDetail> Get()
        {
            return _connection.OrderDetails.ToList();
        }

        // GET api/<OrderDetailController>/5
        [HttpGet("{id}")]
        public OrderDetail Get(Guid orderID, Guid foodID)
        {
            return _connection.OrderDetails.Find(orderID, foodID);
        }

        // POST api/<OrderDetailController>
        [HttpPost]
        public OrderDetail Post([FromBody] OrderDetail OrderDetail)
        {
            _connection.Insert(OrderDetail);

            return _connection.OrderDetails.Find((Guid)OrderDetail.OrderID, (Guid)OrderDetail.FoodItem);
        }

        // PUT api/<OrderDetailController>/5
        [HttpPut("{id}")]
        public OrderDetail Put(Guid orderID, Guid foodItemID, [FromBody] OrderDetail OrderDetail)
        {
            //.connection.OrderDetails.InsertOrUpdate()
            _connection.OrderDetails.Where(c => (c.OrderID == orderID && c.FoodItem == foodItemID))
                .Set(t => t.FoodItem, OrderDetail.FoodItem)
                .Set(t => t.Quantity, OrderDetail.Quantity)
                .Set(t => t.OrderID, OrderDetail.OrderID)
                .Update();
            return _connection.OrderDetails.Find(orderID, foodItemID);
        }

        // DELETE api/<OrderDetailController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid OrderID, Guid FoodItemID)
        {
            _connection.OrderDetails.Where(t =>
                (t.OrderID == OrderID && t.FoodItem == FoodItemID)).Delete();
        }
    }
}
