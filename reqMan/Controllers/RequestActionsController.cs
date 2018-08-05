using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reqMan.Data;
using reqMan.Models;
using reqMan.Models.Enumerations;

namespace reqMan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestActionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RequestActionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RequestActions
        [HttpGet]
        public IEnumerable<RequestAction> GetRequestActions()
        {
            return _context.RequestActions;
        }

        // GET: api/RequestActions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequestAction([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestAction = await _context.RequestActions.FindAsync(id);

            if (requestAction == null)
            {
                return NotFound();
            }

            return Ok(requestAction);
        }

        // PUT: api/RequestActions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestAction([FromRoute] string id, [FromBody] RequestAction requestAction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requestAction.RequestId)
            {
                return BadRequest();
            }

            _context.Entry(requestAction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestActionExists(id))
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

        // POST: api/RequestActions
        [HttpPost]
        public async Task<IActionResult> PostRequestAction([FromBody] RequestAction requestAction)
        {
            string sRequestStatus = null;
            Request request;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!UsertExists(requestAction.Username))
            {
                return BadRequest("User does not exist.");
            }

            if (!RequestExists(requestAction.RequestId))
            {
                return BadRequest("Request does not exist.");
            }

            if (!RequestActions.IsValidAction(requestAction.Action))
            {
                return BadRequest("Request Action does not exist.");
            }

            if(requestAction.Action == RequestActions.DB_APPROVE)
            {
                sRequestStatus = RequestStates.DB_APPROVED;
            }
            else if (requestAction.Action == RequestActions.DB_REJECT)
            {
                sRequestStatus = RequestStates.DB_REJECED;
            }

            _context.RequestActions.Add(requestAction);

            request = await _context.Requests.FindAsync(requestAction.RequestId);

            if(sRequestStatus != null)
            {
                request.State = sRequestStatus;
                _context.Requests.Update(request);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequestAction", new { id = requestAction.RequestId }, requestAction);
        }

        // DELETE: api/RequestActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestAction([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestAction = await _context.RequestActions.FindAsync(id);
            if (requestAction == null)
            {
                return NotFound();
            }

            _context.RequestActions.Remove(requestAction);
            await _context.SaveChangesAsync();

            return Ok(requestAction);
        }

        private bool RequestActionExists(string id)
        {
            return _context.RequestActions.Any(e => e.RequestId == id);
        }

        private bool UsertExists(string id)
        {
            return _context.Users.Any(e => e.Username == id);
        }
        private bool RequestExists(string id)
        {
            return _context.Requests.Any(e => e.RequestId == id);
        }
    }
}