﻿using FoodServiceAPI.DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using static LinqToDB.Reflection.Methods.LinqToDB;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionListController(PostgresDB connection) : ControllerBase
    {
        private readonly PostgresDB _connection = connection;

        // GET: api/<PromotionListController>
        [HttpGet]
        public IEnumerable<PromotionList> Get()
        {
            return _connection.PromotionLists.ToList();
        }

        // GET api/<PromotionListController>/5
        [HttpGet("{id}")]
        public PromotionList Get(int CustomerID, int PromotionID)
        {
            return _connection.PromotionLists.Find(CustomerID, PromotionID);
        }

        // POST api/<PromotionListController>
        [HttpPost]
        public PromotionList Post([FromBody] PromotionList PromotionList)
        {
            _connection.Insert(PromotionList);

            return _connection.PromotionLists.Find(PromotionList.CustomerID.Value, PromotionList.PromotionID.Value);
        }

        // PUT api/<PromotionListController>/5
        [HttpPut("{id}")]
        public PromotionList Put(int CustomerID, int PromotionID, [FromBody] PromotionList PromotionList)
        {
            //.connection.PromotionLists.InsertOrUpdate()
            _connection.PromotionLists.Where(c => (c.CustomerID == CustomerID && c.PromotionID == PromotionID))
                .Set(t => t.CustomerID, PromotionList.CustomerID)
                .Set(t => t.PromotionID, PromotionList.PromotionID)
                .Update();
            return _connection.PromotionLists.Find(Convert.ToInt32(CustomerID), (Convert.ToInt32(PromotionID)));
        }

        // DELETE api/<PromotionListController>/5
        [HttpDelete("{id}")]
        public void Delete(int CustomerID, int PromotionID)
        {
            _connection.PromotionLists.Where(t =>
                (t.CustomerID == CustomerID && t.PromotionID == PromotionID)).Delete();
        }
    }
}
