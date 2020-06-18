﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Colegio.Datos;
using Colegio.Datos.Models;

namespace Colegio.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypesController : ControllerBase
    {
        private readonly SchoolContext _context;

        public UserTypesController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/UserTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserType>>> GetUserType()
        {
            var listTypes = await (from usertype in _context.UserType
                                   select new { usertype.Id, usertype.UserType1 }).ToListAsync();

            return Ok(listTypes);
        }

        // GET: api/UserTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserType(int id)
        {
            //var userType = await _context.UserType.FindAsync(id);

            var userType = await (from usertype in _context.UserType
                                  select new { usertype.Id, usertype.UserType1 }
                                   ).Where(x => x.Id == id).ToListAsync();

            if (userType == null)
            {
                return NotFound();
            }

            return Ok(userType);
        }

        // PUT: api/UserTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserType(int id, UserType userType)
        {
            if (id != userType.Id)
            {
                return BadRequest();
            }

            _context.Entry(userType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserType>> PostUserType(UserType userType)
        {
            _context.UserType.Add(userType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserType", new { id = userType.Id }, userType);
        }

        // DELETE: api/UserTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserType>> DeleteUserType(int id)
        {
            var userType = await _context.UserType.FindAsync(id);
            if (userType == null)
            {
                return NotFound();
            }

            _context.UserType.Remove(userType);
            await _context.SaveChangesAsync();

            return userType;
        }

        private bool UserTypeExists(int id)
        {
            return _context.UserType.Any(e => e.Id == id);
        }
    }
}
