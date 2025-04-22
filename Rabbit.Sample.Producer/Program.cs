using RabbitMQ.Client;
using System.Text;

ConnectionFactory connectionFactory = new() { HostName = "localhost" };
using var connection = await connectionFactory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

string exchangeName = "reza_exchange_direct";
string routingKey = "rezainfo";
await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct);

string message = "hi reza from producer";
var body = Encoding.UTF8.GetBytes(message);

await channel.BasicPublishAsync(
    exchange: exchangeName,
    routingKey: routingKey,
    body: body);

Console.WriteLine($" [x] Sent '{message}'");
Console.ReadKey();

//basic usage of rabbit includes declaring queue and using default exchange and specifying queue for exchange and consumer