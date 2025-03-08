using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Server.Endpoints
{
    [ApiController]
    [Route("api/bonsai")]
    public class BonsaiController : ControllerBase
    {
        private readonly BonsaiContext _context;

        public BonsaiController(BonsaiContext context)
        {
            _context = context;
        }

        // GET: api/bonsai/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Bonsai>> GetBonsai(Guid id)
        {
            var bonsai = await _context.Bonsais.FindAsync(id);
            if (bonsai == null)
            {
                return NotFound();
            }

            return Ok(bonsai);
        }


        // POST: api/bonsai
        [HttpPost]
        public async Task<ActionResult<Bonsai>> CreateBonsai([FromBody] Bonsai newBonsai)
        {
            _context.Bonsais.Add(newBonsai);
            await _context.SaveChangesAsync();

            // This returns a 201 response and points to the GET endpoint for the new Bonsai.
            return CreatedAtAction(nameof(GetBonsai), new { id = newBonsai.Id }, newBonsai);
        }

        // Optionally, you could add a GET to retrieve all Bonsais.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bonsai>>> GetBonsais()
        {
            var bonsais = await _context.Bonsais.ToListAsync();
            return Ok(bonsais);
        }
    }
}