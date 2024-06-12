using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{

		private readonly ECommerceContext _eCommerceContext;
		public LoginController(ECommerceContext eCommerceContext)
        {
			_eCommerceContext = eCommerceContext;   
        }

		[HttpPost]

		public IActionResult Login(UserDto userDto)
		{

			if (_eCommerceContext.Users == null)
			{
				return NotFound();
			}


			 Users users = _eCommerceContext.Users.FirstOrDefault(x=>x.UserName==userDto.UserName);

			if(users==null)
			{
				return NotFound("User not exist");
			}
			else
			{	if(users.Password!.Equals(userDto.Password))
				{
					return Ok(userDto);
				}
			else
				return NotFound("Password is Incorrect");
			}

		}

		[HttpPost("Registration")]

		public async Task<ActionResult<UserDto>> Registration(UserDto userDto)
		{ 
			if(userDto == null)
			{
				NotFound();
			}
			var user = new Users
			{
				UserName = userDto.UserName,
				Password = userDto.Password,
				Email = userDto.Email,
				Role = "user",
				IsActive = 1
			};
			  _eCommerceContext.Users.AddAsync(user);

		await _eCommerceContext.SaveChangesAsync();

			return Ok(user);

		}



    }
}
