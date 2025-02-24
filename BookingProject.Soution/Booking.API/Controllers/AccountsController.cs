using Booking.API.Dtos;
using Booking.Core.Entities;
using Booking.Core.Identity;
using Booking.Core.Services;
using Booking.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotesProjects.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Booking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly StoreContext _context;

        public AccountsController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            StoreContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("login")] // api/Accounts/login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized(result);

            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user, _userManager)
            });
        }

        [HttpPost("register")] //api/Accounts/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new AppUser()
            {
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                Age = registerDto.Age,
                UserName = registerDto.Email.Split("@")[0],
                Email = registerDto.Email
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest();

            return Ok(new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateToken(user, _userManager)
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user, _userManager)
            });
        }





        // Search Endpoint For user 
        [HttpGet("search-residentials")]
        public ActionResult<IEnumerable<string>> SearchResidentials(
      string rooms = null,
      string location = null,
      string area = null,
      string beds = null,
      string baths = null,
      decimal? price = null)
        {
            var query = _context.Residentials.AsQueryable();

            if (!string.IsNullOrEmpty(rooms))
            {
                query = query.Where(r => r.Rooms.Trim().ToLower().Contains(rooms.Trim().ToLower()));
            }
            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(r => r.Location.Trim().ToLower().Contains(location.Trim().ToLower()));
            }
            if (!string.IsNullOrEmpty(area))
            {
                query = query.Where(r => r.Area.Trim().ToLower().Contains(area.Trim().ToLower()));
            }
            if (!string.IsNullOrEmpty(beds))
            {
                query = query.Where(r => r.Beds.Trim().ToLower().Contains(beds.Trim().ToLower()));
            }
            if (!string.IsNullOrEmpty(baths))
            {
                query = query.Where(r => r.Baths.Trim().ToLower().Contains(baths.Trim().ToLower()));
            }
            if (price.HasValue)
            {
                query = query.Where(r => r.Price == price.Value);
            }

            var results = query.ToList();

            if (!results.Any())
            {
                return NotFound("No residentials found matching the criteria.");
            }
            return query.Select(r => r.ResidentialName).Distinct().ToList();
        }

























































    }
    
}



