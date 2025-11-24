
using ApiAgendamento.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiAgendamento
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHostedService<AgendamentoLimpezaService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // 2. (NOVO) Definir o nome do nosso arquivo de banco de dados
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=agendamentos.db";

            // 3. (NOVO) Registrar o DbContext no sistema
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString));

            // ---
            builder.Services.AddControllers();
            // ...

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
