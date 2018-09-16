using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class RequestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public IEnumerable<Request> GetRequests()
        {
            if(User.IsInRole(UserTypes.DB_ADMIN))
            {
                return _context.Requests;
            }
            else
            {
                return _context.Requests.Where(e => e.Username == User.Identity.Name);
            }
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequest([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var request = await _context.Requests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            else
            {
                request.User = _context.Users.Find(request.Username);
                request.RequestActions = _context.RequestActions.Where(e => e.RequestId == request.RequestId).ToList();
            }

            return Ok(request);
        }

        // PUT: api/Requests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest([FromRoute] string id, [FromBody] Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.RequestId)
            {
                return BadRequest();
            }

            if(!UsertExists(request.Username))
            {
                return BadRequest("User does not exist.");
            }

            if (!RequestTypeExists(request.RequestTypeId))
            {
                return BadRequest("Request Type does not exist.");
            }

            if(!RequestStates.IsValidState(request.State))
            {
                return BadRequest("Request State does not exist.");
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                var smtpClient = new SmtpClient
                {
                    Host = "smtp.gmail.com", // set your SMTP server name here
                    Port = 587, // Port 
                    EnableSsl = true,
                    Credentials = new NetworkCredential("noyek.mail@gmail.com", "noy3k.mail_svc")
                };

                using (var message = new MailMessage("noyek.mail@gmail.com", _context.Users.Find(request.Username).Email)
                {
                    Subject = string.Format("Status of the Request {0} has changed to {1}", request.RequestId, request.State),
                    Body = string.Format("Status of the Request {0} has changed to {1}", request.RequestId, request.State)
                })
                {
                    await smtpClient.SendMailAsync(message);
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/Requests
        [HttpPost]
        public async Task<IActionResult> PostRequest([FromBody] Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!UsertExists(request.Username))
            {
                return BadRequest("User does not exist.");
            }

            if (!RequestTypeExists(request.RequestTypeId))
            {
                return BadRequest("Request Type does not exist.");
            }

            request.RequestId = GetNextRequestId();
            request.State = RequestStates.DB_REQUESTED;

            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            _context.RequestActions.Add(new RequestAction()
            {
                Action = "Request Created",
                Comment = request.Description,
                RequestId  = request.RequestId,
                Username = request.Username

            });

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.RequestId }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return Ok(request);
        }

        private bool RequestExists(string id)
        {
            return _context.Requests.Any(e => e.RequestId == id);
        }

        private bool UsertExists(string id)
        {
            return _context.Users.Any(e => e.Username == id);
        }
        private bool RequestTypeExists(string id)
        {
            return _context.RequestType.Any(e => e.RequestTypeId == id);
        }

        private string GetNextRequestId()
        {
            return string.Format("REQ-{0}", _context.GetNextRequestSequence().ToString());
        }
    }
}