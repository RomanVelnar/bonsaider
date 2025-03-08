using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;

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

            return CreatedAtAction(nameof(GetBonsai), new { id = newBonsai.Id }, newBonsai);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bonsai>>> GetBonsais()
        {
            var bonsais = await _context.Bonsais.ToListAsync();
            return Ok(bonsais);
        }
    }
}