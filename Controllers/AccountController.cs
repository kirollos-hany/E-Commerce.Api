using AspNetCore.Identity.MongoDbCore.Models;
using AutoMapper;
using E_Commerce.Api.DataLayer.Database;
using E_Commerce.Api.DataLayer.Models;
using E_Commerce.Api.DTOs;
using E_Commerce.Api.SecurityLayer.Authentication;
using E_Commerce.Api.SecurityLayer.Managers;
using E_Commerce.Api.Utilities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthentication _authentication;

        private readonly IUserManager _userManager;

        private readonly IMapper _mapper;

        private readonly IDbContext _dbContext;

        public AccountController(IAuthentication authentication, IUserManager userManager, IMapper mapper, IDbContext dbContext)
        {
            _authentication = authentication;
            _userManager = userManager;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identityUser = _mapper.Map<MongoIdentityUser<string>>(registerDto);
            var userValid = await _userManager.ValidateUserAsync(identityUser);
            if (!userValid.Succeeded) return BadRequest(userValid.ToString().Replace(",", "\n"));
            var passwordValid = await _userManager.ValidatePasswordAsync(registerDto.Password);
            if (!passwordValid.Succeeded) return BadRequest(passwordValid.ToString().Replace(",", "\n"));

            var phoneNumber = _mapper.Map<PhoneNumber>(registerDto.PhoneNumber);
            var address = _mapper.Map<CustomerAddress>(registerDto.Address);

            var image = await _dbContext.Images.AsQueryable().FirstOrDefaultAsync(i => i.Id == registerDto.ImageId);

            var customer = new Customer(registerDto.FirstName, registerDto.LastName, registerDto.Email, phoneNumber, image ?? new ImageFile(Defaults.DefaultCustomerImage), registerDto.DateOfBirth, address);

            await _dbContext.Customers.InsertOneAsync(customer);

            identityUser.Id = customer.Id;
            await _userManager.CreateAsync(identityUser, registerDto.Password);
            await _userManager.AddToRoleAsync(identityUser, Constants.SystemRoles.Customer.ToString());

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(loginDto.UserName);

            if (user == default) return BadRequest(new { Message = "Login credentials incorrect" });

            var passwordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!passwordValid) return BadRequest(new { Message = "Password is incorrect" });

            return Ok(new
            {
                Token = _authentication.GenerateJwt(loginDto.UserName, await _userManager.GetUserRolesAsync(user))
            });
        }
        
        
    }
}