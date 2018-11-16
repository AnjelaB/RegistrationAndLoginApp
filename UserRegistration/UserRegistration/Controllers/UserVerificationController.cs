using Microsoft.AspNetCore.Mvc;
using UserRegistration.DataModel;

namespace UserRegistration.Controllers
{
    [Produces("application/json")]
    [Route("api/userverification")]
    //[ApiController]
    public class UserVerificationController : ControllerBase
    {
        private DataAccessLayer dataAccessLayer;
        public UserVerificationController(DataAccessLayer dataAccessLayer)
        {
            this.dataAccessLayer = dataAccessLayer;
        }
        // GET: api/UserVerification/5
        [HttpGet("{login}")]
        public User Get(string login)
        {
            return dataAccessLayer.GetUserByLogin(login);
        }

    }
}
