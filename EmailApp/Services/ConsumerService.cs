using Confluent.Kafka;
using EmailApp.Model;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;
using static Confluent.Kafka.ConfigPropertyNames;

namespace EmailApp.Services
{
    public class ApacheKafkaConsumerService : BackgroundService
    {
        private readonly string topic = "topic-mail";
        private readonly string groupId = "test_group";
        private readonly string bootstrapServers = "localhost:9092";

     
   

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = groupId,
                BootstrapServers = bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            try
            {
                using (var consumerBuilder = new ConsumerBuilder
                <Ignore, string>(config).Build())
                {
                    consumerBuilder.Subscribe(topic);
                    var cancelToken = new CancellationTokenSource();

                    try
                    {
                        while (true)
                        {
                            var consumer = consumerBuilder.Consume(cancelToken.Token);
                            var emailInfo = System.Text.Json.JsonSerializer.Deserialize
                                <InfoEmail>(consumer.Message.Value);
                            Console.WriteLine($"Email Receiver: {emailInfo.ReceiverMail} , Email Title: {emailInfo.Title} , Email Content : {emailInfo.Contents}  ");
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        consumerBuilder.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
