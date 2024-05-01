﻿using VueAPI.Server.DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using static LinqToDB.Reflection.Methods.LinqToDB;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VueAPI.Server.Controllers
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
        public Customer Get(Guid id)
        {
            return _connection.Customers.Find(id);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public Customer Post([FromBody] Customer customer)
        {
            customer.CustomerID = Guid.NewGuid();
            _connection.Insert(customer);

            return _connection.Customers.Find(customer.CustomerID);
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public Customer Put(Guid id, [FromBody] Customer customer)
        {
            //.connection.Customers.InsertOrUpdate()
            _connection.Customers.Where(c => c.CustomerID == id)
                .Set(t => t.Name, customer.Name)
                .Set(t => t.LastAddress, customer.LastAddress)
                .Set(t => t.PhoneNumber, customer.PhoneNumber)
                .Set(t => t.PromotionList, customer.PromotionList)
                .Set(t => t.Email, customer.Email)
                .Update();
            return _connection.Customers.Find(id); 
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _connection.Customers.Where(c => c.CustomerID == id).Delete();
        }
    }
}