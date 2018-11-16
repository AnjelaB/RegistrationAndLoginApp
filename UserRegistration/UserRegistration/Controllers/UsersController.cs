using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UserRegistration.DataModel;

namespace UserRegistration.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    //[ApiController]
    public class UsersController : ControllerBase
    {
        private DataAccessLayer dataAccessLayer;

        public UsersController(DataAccessLayer dataAccessLayer)
        {
            this.dataAccessLayer = dataAccessLayer;
        }
        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return this.dataAccessLayer.GetUsers();
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public User Get(int id)
        {
            return this.dataAccessLayer.GetUserById(id);
        }

        // POST: api/Users
        [HttpPost]
        public void Post([FromBody] User user)
        {
            this.dataAccessLayer.InsertUser(user);
        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.dataAccessLayer.DeleteUser(id);
        }
    }
}
