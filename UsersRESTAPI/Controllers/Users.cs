using Microsoft.AspNetCore.Mvc;
using UsersRESTAPI.Models;
using UsersRESTAPI.Repository;

namespace UsersRESTAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Users : Controller
    {
        IUsersRepo user;
        public Users(IUsersRepo _user)
        {
            user = _user;
        }

        [HttpGet]
        public JsonResult GetAll()
        {

            return new JsonResult(user.GetUsers());
        }

        [HttpGet]
        [Route("{id}")]        
        public JsonResult GetMeHim(uint id)
            {

                return new JsonResult(user.GetById(id));
            }

        [HttpDelete]
        [Route("{id}")]
        public JsonResult Delete(uint id)
        {
            return new JsonResult(user.Delete(id));
        }

        [HttpPost]
        public JsonResult Post([FromBody] UserModel UsModel)
        {
            return new JsonResult(user.Add(UsModel));
        }

        [HttpPut]
        [Route("{id}")]
        public JsonResult Update([FromBody] UserModel userModel, uint id)
        {
            return new JsonResult(user.Update(userModel,id));
        }

    }
}
