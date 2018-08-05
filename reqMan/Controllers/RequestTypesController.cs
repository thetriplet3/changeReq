using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reqMan.Data;
using reqMan.Models;

namespace reqMan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RequestTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RequestTypes
        [HttpGet]
        public IEnumerable<RequestType> GetRequestType()
        {
            return _context.RequestType;
        }

        // GET: api/RequestTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequestType([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestType = await _context.RequestType.FindAsync(id);

            if (requestType == null)
            {
                return NotFound();
            }

            return Ok(requestType);
        }

        // PUT: api/RequestTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestType([FromRoute] string id, [FromBody] RequestType requestType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requestType.RequestTypeId)
            {
                return BadRequest();
            }

            _context.Entry(requestType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestTypeExists(id))
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

        // POST: api/RequestTypes
        [HttpPost]
        public async Task<IActionResult> PostRequestType([FromBody] RequestType requestType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.RequestType.Add(requestType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequestType", new { id = requestType.RequestTypeId }, requestType);
        }

        // DELETE: api/RequestTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestType([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestType = await _context.RequestType.FindAsync(id);
            if (requestType == null)
            {
                return NotFound();
            }

            _context.RequestType.Remove(requestType);
            await _context.SaveChangesAsync();

            return Ok(requestType);
        }

        private bool RequestTypeExists(string id)
        {
            return _context.RequestType.Any(e => e.RequestTypeId == id);
        }
    }
}