﻿using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using DTO;
using HotChocolate.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SecurityMarket.Abstraction;
using SecurityMarket.Model;

namespace SecurityMarket.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecurityController(IUserRepository userRepository, ITokenService tokenService) : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITokenService _tokenService = tokenService;

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody] UserAuthRequest userLogin)
        {
            try
            {
                var roleId = _userRepository.CheckUser(userLogin.Email, userLogin.Password);

                var user = new UserAuthRequest() { Email = userLogin.Email, UserRole = RoleIDToRole(roleId) };

                var token = tokenService.GenerateToken(user.Email, roleId.ToString());
                return Ok(token);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("AddAdmin")]
        public ActionResult AddAdmin([FromBody] UserAuthRequest userLogin)
        {
            try
            {
                _userRepository.AddUser(userLogin.Email, userLogin.Password, UserRoleType.Admin);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }


            return Ok();
        }

        private RoleType RoleIDToRole(UserRoleType roleId)
        {
            if (roleId == UserRoleType.Admin) return RoleType.Admin;

            return RoleType.User;
        }
    }
}