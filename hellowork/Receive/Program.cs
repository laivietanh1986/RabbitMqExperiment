using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Receive
{
    class Receive
    {
        public static void Main (string[] args)
        {
            var factory = new ConnectionFactory(){HostName="localhost"};
            using(var connection = factory.CreateConnection())
            {
                using(var chanel = connection.CreateModel())
                {
                    chanel.QueueDeclare(queue:"Hello",
                                        exclusive:false,
                                        autoDelete:false,
                                        durable:false,
                                        arguments:null);
                    var consumer = new EventingBasicConsumer(chanel);
                    consumer.Received +=(model,ea)=>{
                        var body = ea.Body;
                        var message = UnicodeEncoding.UTF8.GetString(body);
                        System.Console.WriteLine($"Receive {message}");

                    };
                    chanel.BasicConsume(queue:"Hello",
                                        noAck:true,
                                        consumer:consumer);
                    System.Console.WriteLine("input any key to exit");
                    Console.ReadLine();
                }
            }
        }
    }
}
