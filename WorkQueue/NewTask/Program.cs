using System;
using System.Text;
using RabbitMQ.Client;

namespace NewTask
{
    class NewTask
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory(){HostName="localhost"};
            using(var connection = factory.CreateConnection())
            {
                using(var chanel = connection.CreateModel())
                {
                    chanel.QueueDeclare(queue:"task_queue",
                                        durable:true,
                                        exclusive:false,
                                        autoDelete:false,
                                        arguments:null);
                    var message = GetMessage(args);
                    var body = Encoding.UTF8.GetBytes(message);
                    var properties = chanel.CreateBasicProperties();
                    properties.Persistent = true;
                    chanel.BasicPublish(exchange:"",
                                        routingKey:"task_queue",
                                        basicProperties:null,
                                        body:body);
                    System.Console.WriteLine($"send {message}");

                }
            }
            System.Console.WriteLine("enter to exit");
            System.Console.ReadLine();
        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ",args):"Hello World");
        }
    }
}
