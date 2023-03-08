using Confluent.Kafka;
using EmailApp.Model;
using System.Diagnostics;
using System.Text.Json;
using static Confluent.Kafka.ConfigPropertyNames;

namespace EmailApp.Services
{
    public class ConsumerService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = "gid-consumers",
                BootstrapServers = "localhost:9092"
            };

            using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
            {
                consumer.Subscribe("email");
                while (true)
                {
                    var email = consumer.Consume();

                    Console.WriteLine($"Deneme: {email.Message.Value}");
                    InfoEmail infoEmail = JsonSerializer.Deserialize<InfoEmail>(email.Message.Value);
                    Console.WriteLine($"Email Title: {infoEmail.Title} , Email Receiver: {infoEmail.ReceiverMail}");

                }
            }
            return Task.CompletedTask;

            //throw new NotImplementedException();
        }

    }
}
