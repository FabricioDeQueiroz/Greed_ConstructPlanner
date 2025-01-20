using Construct_Planner_API.Data;
using Construct_Planner_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Construct_Planner_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObrasController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;
            
        [HttpGet("listar/{id:int?}")]
        public async Task<IActionResult> GetObras(int? id)
        {
            if (id == null)
            {
                var obras = await _context.Obras
                    .ToListAsync();
                    
                return Ok(obras);
            }

            var obra = await _context.Obras
                .Where(a => a.Id == id)
                .ToListAsync();
                
            return Ok(obra);
        }
            
        [HttpPost("criar")]
        public async Task<IActionResult> CriarObra([FromBody] Obra obra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                
            _context.Obras.Add(obra);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetObras), new { id = obra.Id }, obra);
        }
    }
}

