using System;
using System.Text;
using RabbitMQ.Client;

namespace Send
{
    class Send
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.HostName="localhost";
            using(var connection = factory.CreateConnection())
            {
                using(var chanel = connection.CreateModel())
                {
                    chanel.QueueDeclare(queue:"Hello",
                                        durable:false,
                                        exclusive:false,
                                        autoDelete:false,
                                        arguments:null);
                    string message = "Hello world";
                    var body = Encoding.UTF8.GetBytes(message);
                    chanel.BasicPublish(exchange:"",
                                        routingKey:"Hello",
                                        basicProperties:null,
                                        body:body);
                    System.Console.WriteLine($"Send {message}");
                }
            }
            System.Console.WriteLine("enter key to exit ..");
            Console.ReadLine();
        }
    }
}
