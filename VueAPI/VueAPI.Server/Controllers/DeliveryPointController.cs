using VueAPI.Server.DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using static LinqToDB.Reflection.Methods.LinqToDB;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VueAPI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryPointController(PostgresDB connection) : ControllerBase
    {
        private readonly PostgresDB _connection = connection;

        // GET: api/<DeliveryPointController>
        [HttpGet]
        public IEnumerable<DeliveryPoint> Get()
        {
            return _connection.DeliveryPoints.ToList();
        }

        // GET api/<DeliveryPointController>/5
        [HttpGet("{id}")]
        public DeliveryPoint Get(Guid id)
        {
            return _connection.DeliveryPoints.Find(id);
        }

        // POST api/<DeliveryPointController>
        [HttpPost]
        public DeliveryPoint Post([FromBody] DeliveryPoint DeliveryPoint)
        {
            DeliveryPoint.DeliveryPointID = Guid.NewGuid();
            _connection.Insert(DeliveryPoint);

            return _connection.DeliveryPoints.Find(DeliveryPoint.DeliveryPointID);
        }

        // PUT api/<DeliveryPointController>/5
        [HttpPut("{id}")]
        public DeliveryPoint Put(Guid id, [FromBody] DeliveryPoint DeliveryPoint)
        {
            //.connection.DeliveryPoints.InsertOrUpdate()
            _connection.DeliveryPoints.Where(c => c.DeliveryPointID == id)
                .Set(t => t.Name, DeliveryPoint.Name)
                .Set(t => t.Address, DeliveryPoint.Address)
                .Update();
            return _connection.DeliveryPoints.Find(id);
        }

        // DELETE api/<DeliveryPointController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _connection.DeliveryPoints.Where(c => c.DeliveryPointID == id).Delete();
        }
    }
}
