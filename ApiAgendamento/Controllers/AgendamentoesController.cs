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

        [HttpGet("filtro")]
        public async Task<ActionResult<IEnumerable<Agendamento>>> GetAgendamentosPorData(DateTime? inicio, DateTime? fim)
        {
            var query = _context.Agendamentos.AsNoTracking().AsQueryable();

            if (inicio.HasValue)
                query = query.Where(x => x.DatahorarioInicio >= inicio.Value);

            if (fim.HasValue)
                query = query.Where(x => x.DatahorarioInicio <= fim.Value);

            // Já ordena no banco, evitando processamento no C# (OrderBy)
            return await query.OrderBy(x => x.DatahorarioInicio).ToListAsync();
        }

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

            if (id != agendamento.Id)
            {
                return BadRequest("IDs não conferem.");
            }


            var conflito = await _context.Agendamentos
                .FirstOrDefaultAsync(a =>
                    agendamento.DatahorarioInicio < a.DataHoraFim &&
                    agendamento.DataHoraFim > a.DatahorarioInicio
                );

            if (conflito != null)
            {

                return BadRequest($"Horário em conflito com a reserva '{conflito.Titulo}' (das {conflito.DatahorarioInicio:HH:mm} às {conflito.DataHoraFim:HH:mm}).");
            }

 
            _context.Entry(agendamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();


                string msgLog = $"EDITADO: ID {id} | Novo Título: {agendamento.Titulo} | Nova Data: {agendamento.DatahorarioInicio}";
                GerarLog(msgLog);

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

            return NoContent();
        }

        // POST: api/Agendamentoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Agendamento>> PostAgendamento(Agendamento agendamento)
        {
            var conflito = await _context.Agendamentos
                 .FirstOrDefaultAsync(a =>
                     agendamento.DatahorarioInicio < a.DataHoraFim &&
                     agendamento.DataHoraFim > a.DatahorarioInicio
                 );

            if (conflito != null)
            {
                return BadRequest($"Horário em conflito com a reserva {conflito.Titulo} das ({conflito.DatahorarioInicio:HH:mm} às {conflito.DataHoraFim}).");
            }

            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();

            string msgLog = $"CRIADO: Novo Agendamento ID {agendamento.Id} | Título: {agendamento.Titulo} | Início: {agendamento.DatahorarioInicio}";
            GerarLog(msgLog);

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

        private void GerarLog(string mensagem)
        {
            try
            {
                string pastaBase = AppDomain.CurrentDomain.BaseDirectory;
                string pastaLogs = Path.Combine(pastaBase, "Logs");

                if (!Directory.Exists(pastaLogs))
                {
                    Directory.CreateDirectory(pastaLogs);
                }

                string arquivoLog = Path.Combine(pastaLogs, "log_agendamentos.txt");


                string logFormatado = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] - {mensagem}{Environment.NewLine}";

                System.IO.File.AppendAllText(arquivoLog, logFormatado);
            }
            catch
            {
            }
        }

    }
}
