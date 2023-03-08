using Confluent.Kafka;
using EmailApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Threading;

namespace EmailApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ConsumerConfig _config;

        public HomeController(ConsumerConfig config)
        {
            _config= config;
        }


        [HttpGet]
        public IActionResult GetEmail()
        {
            using (var consumer = new ConsumerBuilder<Null, string>(_config).Build())
            {
                consumer.Subscribe("test");
                try
                {
                    while (true)
                    {
                        var result = consumer.Consume();
                        Console.WriteLine($"{result.Message.Value}");
                    }
                }
                catch (Exception)
                {

                    throw;
                }
              
            }

        }
    }
}
