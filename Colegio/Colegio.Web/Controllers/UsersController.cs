using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Colegio.Datos;
using Colegio.Web.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Colegio.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SchoolContext _context;
        private readonly IConfiguration _config;
        public UsersController(SchoolContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/Users/Get
        [HttpGet("getUsers")]
        public async Task<ActionResult<IEnumerable<UsersViewModel>>> GetUsers()
        {
            var users = await _context.Users.Include(x => x.Role).Include(x => x.DocumentType).ToListAsync();
            return Ok(users.Select(u => new UsersViewModel
            {
                Id = u.Id,
                Firstname = u.Firstname,
                Lastname = u.Lastname,
                Email = u.Email,
                DocumentType = u.DocumentType.Name,
                DocumentNumber = u.DocumentNumber,
                UserType = u.Role.Name
            }));
        }

        // GET: api/Users/Get/1
        [HttpGet("getUser/{id}")]
        [Authorize]
        public async Task<ActionResult<UsersViewModel>> GetUserById(int id)
        {
            var user = await _context.Users.ToListAsync();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
