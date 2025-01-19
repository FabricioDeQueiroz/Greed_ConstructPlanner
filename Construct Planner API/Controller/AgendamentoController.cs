using Construct_Planner_API.Data;
using Construct_Planner_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Construct_Planner_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpPost("calcular")]
        public async Task<IActionResult> CalcularAgendamento([FromBody] CalcularAgendamentoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var obras = await _context.Obras.ToListAsync();
            var agendamentos = new List<Agendamento>();
            var dataAtual = request.DataInicial.ToUniversalTime();

            // Ordena as obras por deadline
            obras = obras.OrderBy(o => o.Deadline).ToList();

            foreach (var obra in obras)
            {
                var agendamento = new Agendamento
                {
                    ObraId = obra.Id,
                    DataInicioProjeto = dataAtual.ToUniversalTime(),
                    DataInicioFormas = dataAtual.AddDays(obra.Project).ToUniversalTime(),
                    DataInicioConcretagem = dataAtual.AddDays(obra.Project + obra.Mold).ToUniversalTime(),
                    DataInicioTransporte = dataAtual.AddDays(obra.Project + obra.Mold + obra.Concreting).ToUniversalTime(),
                    DataInicioMontagem = dataAtual.AddDays(obra.Project + obra.Mold + obra.Concreting + obra.Transport).ToUniversalTime(),
                    DataFimMontagem = dataAtual.AddDays(obra.Project + obra.Mold + obra.Concreting + obra.Transport + obra.Mounting).ToUniversalTime()
                };

                // Calcula o Max Lateness (atraso em dias)
                var atraso = (agendamento.DataFimMontagem - obra.Deadline.ToUniversalTime()).Days;
                agendamento.MaxLateness = atraso > 0 ? atraso : 0;

                agendamentos.Add(agendamento);
                
                // Atualiza a data atual para a próxima obra começar após o término da atual
                dataAtual = agendamento.DataFimMontagem;
            }

            // Remove agendamentos anteriores
            _context.Agendamentos.RemoveRange(await _context.Agendamentos.ToListAsync());
            
            // Adiciona os novos agendamentos
            await _context.Agendamentos.AddRangeAsync(agendamentos);
            await _context.SaveChangesAsync();

            // Retorna os agendamentos ordenados por Max Lateness
            return Ok(agendamentos.OrderByDescending(a => a.MaxLateness));
        }

        [HttpGet("listar")]
        public async Task<IActionResult> ListarAgendamentos()
        {
            var agendamentos = await _context.Agendamentos
                .Include(a => a.Obra)
                .ToListAsync();

            // Calcula o Max Lateness para cada agendamento
            foreach (var agendamento in agendamentos)
            {
                if (agendamento.Obra != null)
                {
                    var atraso = (agendamento.DataFimMontagem - agendamento.Obra.Deadline.ToUniversalTime()).Days;
                    agendamento.MaxLateness = atraso > 0 ? atraso : 0;
                }
            }

            // Retorna os agendamentos ordenados por Max Lateness
            return Ok(agendamentos.OrderByDescending(a => a.MaxLateness));
        }
    }
}
