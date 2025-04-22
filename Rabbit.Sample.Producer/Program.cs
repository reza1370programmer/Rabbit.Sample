using RabbitMQ.Client;
using System.Text;

var ConnectionFactory = new ConnectionFactory()
{
    HostName = "localhost"
};
var Connection = await ConnectionFactory.CreateConnectionAsync();
var Channel = await Connection.CreateChannelAsync();

await Channel.QueueDeclareAsync("MyQueue", false, false, false, null);
string message = "hi reza from producer";
var body = Encoding.UTF8.GetBytes(message);
await Channel.BasicPublishAsync("", "MyQueue", body);
Console.WriteLine("message sent");
Console.ReadKey();