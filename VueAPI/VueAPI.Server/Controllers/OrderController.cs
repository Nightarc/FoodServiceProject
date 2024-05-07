﻿using Microsoft.AspNetCore.Mvc;
using VueAPI.Server.DataModels;
using LinqToDB;
using System.Data.Common;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VueAPI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(PostgresDB connection) : ControllerBase
    {
        private readonly PostgresDB _connection = connection;

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return  _connection.Orders.ToList();
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public Order Get(Guid id)
        {
            return _connection.Orders.Find(id);
        }

        // POST api/<OrderController>
        [HttpPost]
        public Order Post([FromBody] Order Order)
        {
            Order.OrderID = Guid.NewGuid();
            Order.Time = DateTime.Now.ToUniversalTime();
            Order.Date = DateTime.Now.Date;
            _connection.Insert(Order);

            return _connection.Orders.Find(Order.OrderID);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public Order Put(Guid id, [FromBody] Order order)
        {
            _connection.Orders.Where(c => c.OrderID == id)
                .Set(t => t.Address, order.Address)
                .Set(t => t.PaymentID, order.PaymentID)
                .Set(t => t.Time, order.Time)
                .Set(t => t.Date, order.Date)
                .Set(t => t.CustomerID, order.CustomerID)
                .Set(t => t.DeliveryPointID, order.DeliveryPointID)
                .Update();

            return _connection.Orders.Find(id);
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _connection.Orders.Where(c => c.OrderID == id).Delete();
        }
    }
}
