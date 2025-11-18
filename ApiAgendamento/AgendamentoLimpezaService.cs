using Microsoft.EntityFrameworkCore;

namespace ApiAgendamento
{
    public class AgendamentoLimpezaService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public AgendamentoLimpezaService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);


                if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                {
                    await LimparAgendamentosAntigos();
                }
            }
        }

        private async Task LimparAgendamentosAntigos()
        {

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider
                                    .GetRequiredService<ApiAgendamento.Data.AppDbContext>();

                var hoje = DateTime.Now;


                await dbContext.Agendamentos
                    .Where(a => a.DataHoraFim < hoje)
                    .ExecuteDeleteAsync();

            }
        }
    }
}
