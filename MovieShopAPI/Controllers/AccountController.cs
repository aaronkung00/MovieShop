using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Models.Request_Model;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IUserService _userService;
        public AccountController(IUserService userSevice)
        {
            _userService = userSevice;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserRegisterRequestModel userRegisterRequestModel)
        {

            if (ModelState.IsValid)
            {
                //call the  user service  REMEBER  DI
                return Ok(userRegisterRequestModel);
            }

            return BadRequest(new {message = "Please correct the input info"}); 
        }

        [HttpGet]
        [Route("{id:int}",Name = "GetUser")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserDetails(id);
            return Ok(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequestModel loginRequest)
        {
            var user = await _userService.ValidateUser(loginRequest.Email, loginRequest.Password);
            if (user == null) return Unauthorized();
            return Ok("Login Successed.") ;
        }


        /*
         
         abc1234!  Verla.Yost@gmail.com
        */
    }
}
