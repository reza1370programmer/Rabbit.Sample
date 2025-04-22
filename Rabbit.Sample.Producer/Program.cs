using RabbitMQ.Client;
using System.Text;

var ConnectionFactory = new ConnectionFactory()
{
    HostName = "localhost"
};
var Connection = await ConnectionFactory.CreateConnectionAsync();
var Channel = await Connection.CreateChannelAsync();


string message = "hi reza from producer";
var body = Encoding.UTF8.GetBytes(message);

string ExchangeName = "reza_exchange_direct";
string RoutingKey = "reza_info";
await Channel.ExchangeDeclareAsync(ExchangeName, ExchangeType.Direct);

await Channel.BasicPublishAsync(ExchangeName, RoutingKey, body);
Console.WriteLine("message sent");
Console.ReadKey();

//basic usage of rabbit includes declaring queue and using default exchange and specifying queue for exchange and consumer