using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


var ConnectionFactory = new ConnectionFactory()
{
    HostName = "localhost"
};
var Connection = await ConnectionFactory.CreateConnectionAsync();
var Channel = await Connection.CreateChannelAsync();

await Channel.QueueDeclareAsync("MyQueue", false, false, false, null);
var ConsumerEvent = new AsyncEventingBasicConsumer(Channel);
ConsumerEvent.ReceivedAsync += (s, e) =>
{
    var body = e.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"message: {message}");
    return Task.CompletedTask;
};
await Channel.BasicConsumeAsync("MyQueue", true, ConsumerEvent);

Console.WriteLine("consumer finished");
Console.ReadKey();