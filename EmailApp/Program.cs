using Confluent.Kafka;
using EmailApp.Model;
using EmailApp.Services;
using Microsoft.EntityFrameworkCore;

namespace EmailApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigurationManager _configuration = builder.Configuration;

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IHostedService, ConsumerService>();

            builder.Services.AddDbContext<EmailAppContext>(opts =>
            {
                opts.UseSqlServer(_configuration.GetConnectionString("SqlServer"));
            });



            //var consumerConfig = new ConsumerConfig();
            //_configuration.Bind("consumer", consumerConfig);
            //builder.Services.AddSingleton<ConsumerConfig>(consumerConfig);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}