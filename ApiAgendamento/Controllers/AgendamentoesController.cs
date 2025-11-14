using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAgendamento.Data;
using ApiAgendamento.Models;

namespace ApiAgendamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AgendamentoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Agendamentoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agendamento>>> GetAgendamentos()
        {
            return await _context.Agendamentos.ToListAsync();
        }

        // GET: api/Agendamentoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agendamento>> GetAgendamento(int id)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);

            if (agendamento == null)
            {
                return NotFound();
            }

            return agendamento;
        }

        // PUT: api/Agendamentoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgendamento(int id, Agendamento agendamento)
        {
            // 1. Validação padrão: o ID do URL deve ser o mesmo do objeto
            if (id != agendamento.Id)
            {
                return BadRequest("IDs não conferem.");
            }

            // --- ⬇️ VALIDAÇÃO DE CONFLITO PARA 'PUT' ⬇️ ---
            // É a mesma lógica do POST, mas com uma exceção:
            // "Procure um conflito com qualquer agendamento QUE NÃO SEJA EU MESMO"

            var conflito = await _context.Agendamentos
                .FirstOrDefaultAsync(a =>
                    agendamento.DatahorarioInicio < a.DataHoraFim && // Novo Início < Fim Existente
                    agendamento.DataHoraFim > a.DatahorarioInicio &&   // Novo Fim > Início Existente
                    a.Id != agendamento.Id // <-- A EXCEÇÃO: Ignorar eu mesmo
                );

            if (conflito != null)
            {
                // Encontrou um conflito com outro agendamento
                return BadRequest($"Horário em conflito com a reserva '{conflito.Titulo}' (das {conflito.DatahorarioInicio:HH:mm} às {conflito.DataHoraFim:HH:mm}).");
            }

            // --- ⬆️ FIM DA VALIDAÇÃO ⬆️ ---

            // 2. Se não achou conflito, podemos atualizar no banco
            _context.Entry(agendamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Agendamentos.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // 3. Retorna "Sem Conteúdo", o padrão de sucesso para PUT
            return NoContent();
        }

        // POST: api/Agendamentoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Agendamento>> PostAgendamento(Agendamento agendamento)
        {
            var conflito = await _context.Agendamentos
                .FirstOrDefaultAsync(a => a.DatahorarioInicio < a.DataHoraFim && a.DataHoraFim > a.DatahorarioInicio);

            if(conflito != null)
            {
                return BadRequest($"Horário em conflito com a reserva {conflito.Titulo} das ({conflito.DatahorarioInicio:HH:mm} às {conflito.DataHoraFim}).");
            }

            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAgendamento", new {id = agendamento.Id}, agendamento);

        }

        // DELETE: api/Agendamentoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgendamento(int id)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }

            _context.Agendamentos.Remove(agendamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AgendamentoExists(int id)
        {
            return _context.Agendamentos.Any(e => e.Id == id);
        }
    }
}
