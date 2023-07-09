using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domains;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<ApplicationUser> usermanager, ITokenRepository tokenRepository)
        {
            _usermanager = usermanager;
            _tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequestDTO registerRequestDTO)
        {
            var applicationUser = new ApplicationUser()
            {
                Name = registerRequestDTO.Name,
                Email = registerRequestDTO.Username,
                UserName = registerRequestDTO.Username
            };
            var identityResult = await _usermanager.CreateAsync(applicationUser, registerRequestDTO.Password);

            if (identityResult.Succeeded)
            {
                if(registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await _usermanager.AddToRolesAsync(applicationUser, registerRequestDTO.Roles);

                }
                if (identityResult.Succeeded)
                {
                    return Ok("Registration Successful. Please Login");
                }

            }

            return BadRequest("Something went wrong. Please try again");

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await _usermanager.FindByEmailAsync(loginRequestDTO.Username);

            if(user != null)
            {
                bool isFound = await _usermanager.CheckPasswordAsync(user, loginRequestDTO.Password);

                if (isFound)
                {
                    //Generate a Token
                    var roles = await _usermanager.GetRolesAsync(user);

                    if(roles != null)
                    {
                        var jwtToken = _tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDTO()
                        {
                            Email = user.Email,
                            Name = user.Name,
                            JwtToken = jwtToken
                        };
                        return Ok(response);

                    }

                    
                }

                
            }

            return BadRequest("Username or password incorrect");
        }
    }
}
