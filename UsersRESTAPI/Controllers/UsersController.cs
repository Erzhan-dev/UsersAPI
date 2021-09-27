using Microsoft.AspNetCore.Mvc;
using UsersRESTAPI.Models;
using UsersRESTAPI.Repository;
using System.Threading.Tasks;
using System;
using UsersRESTAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace UsersRESTAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        readonly IUsersRepo user;
        public UsersController(IUsersRepo _user) => user = _user;

        [Authorize]
        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            return new JsonResult(await user.GetUsers());
        }
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<JsonResult> GetMeHim(uint id)
        {
            return new JsonResult(await user.GetById(id));
        }
        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(uint id)
        {
            string message = "Success";
            bool flag = true;

            try
            {
                await user.Delete(id);
            }
            catch (Exception ex)
            {
                flag = false;
                message = ex.Message;
            }
            if (!flag)
                return BadRequest(message);
            else
                return Ok(message);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserModel UsModel)
        {
            string message = "Success";
            bool flag = true;
            if (!DataValidation.IsLin(UsModel.Lin))
                message = "Invalid lin";
            else if (!DataValidation.IsName(UsModel.FirstName))
                message = "Invalid First Name";
            else if (!DataValidation.IsName(UsModel.LastName))
                message = "Invalid Last Name";
            else
            {
                try
                {
                    await user.Add(UsModel);
                }
                catch (Exception ex)
                {
                    flag = false;
                    message = ex.Message;
                }
                if (!flag)
                    return BadRequest(message);
                else
                    return Ok(message);
            }
            return BadRequest(message);
        }
        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody] UserModel userModel, uint id)
        {

            string message = "Success";
            bool flag = true;
            if (!DataValidation.IsLin(userModel.Lin))
                message = "Invalid lin";
            else if (!DataValidation.IsName(userModel.FirstName))
                message = "Invalid First Name";
            else if (!DataValidation.IsName(userModel.LastName))
                message = "Invalid Last Name";
            else
            {
                try
                {
                    await user.Update(userModel, id);
                }
                catch (Exception ex)
                {
                    flag = false;
                    message = ex.Message;
                }
                if (!flag)
                    return BadRequest(message);
                else
                    return Ok(message);
            }
            return BadRequest(message);

        }

        [Authorize]
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserModelForAuth model)
        {

            return Ok();
        }
    }
}
