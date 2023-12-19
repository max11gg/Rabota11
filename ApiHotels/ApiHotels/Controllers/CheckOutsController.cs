using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiHotels.Models;

namespace ApiHotels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckOutsController : ControllerBase
    {
        private readonly HotelContext _context;

        public CheckOutsController(HotelContext context)
        {
            _context = context;
        }

        // GET: api/CheckOuts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheckOut>>> GetCheckOuts()
        {
          if (_context.CheckOuts == null)
          {
              return NotFound();
          }
            return await _context.CheckOuts.ToListAsync();
        }

        // GET: api/CheckOuts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CheckOut>> GetCheckOut(int id)
        {
          if (_context.CheckOuts == null)
          {
              return NotFound();
          }
            var checkOut = await _context.CheckOuts.FindAsync(id);

            if (checkOut == null)
            {
                return NotFound();
            }

            return checkOut;
        }

        // PUT: api/CheckOuts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCheckOut(int id, CheckOut checkOut)
        {
            if (id != checkOut.IdCheckOut)
            {
                return BadRequest();
            }

            _context.Entry(checkOut).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckOutExists(id))
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

        // POST: api/CheckOuts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CheckOut>> PostCheckOut(CheckOut checkOut)
        {
          if (_context.CheckOuts == null)
          {
              return Problem("Entity set 'HotelContext.CheckOuts'  is null.");
          }
            _context.CheckOuts.Add(checkOut);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCheckOut", new { id = checkOut.IdCheckOut }, checkOut);
        }

        // DELETE: api/CheckOuts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheckOut(int id)
        {
            if (_context.CheckOuts == null)
            {
                return NotFound();
            }
            var checkOut = await _context.CheckOuts.FindAsync(id);
            if (checkOut == null)
            {
                return NotFound();
            }

            _context.CheckOuts.Remove(checkOut);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/CheckOuts/ByUser/5
        [HttpGet("ByUser/{userId}")]
        public async Task<ActionResult<IEnumerable<CheckOut>>> GetCheckOutsByUser(int userId)
        {
            if (_context.CheckOuts == null)
            {
                return NotFound();
            }

            var checkOuts = await _context.CheckOuts
                .Where(co => co.UserId == userId)
                .ToListAsync();

            if (checkOuts == null || checkOuts.Count == 0)
            {
                return NotFound();
            }

            return checkOuts;
        }


        private bool CheckOutExists(int id)
        {
            return (_context.CheckOuts?.Any(e => e.IdCheckOut == id)).GetValueOrDefault();
        }
    }
}
