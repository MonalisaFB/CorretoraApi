
using Dados;
using ImoveisApi;
using ImoveisApi.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace ImoveisApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //injeçao de dependencia - usará apenas esse obj em todo o projeto
            builder.Services.AddSingleton<IImovelRepositorio, ImovelMemoria>();

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;

            });

            builder.Services.AddControllers(options =>
            {
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
           //documentacao
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseMiddleware<Erros>();
          
            app.MapControllers();
            app.Run();
        }
    }
}
