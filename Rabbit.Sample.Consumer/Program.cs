using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
using var connection = await connectionFactory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

string exchangeName = "reza_exchange_direct";
string routingKey = "rezainfo";
await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct);

// Use a named queue instead of temporary one
string queueName = "reza_queue";
await channel.QueueDeclareAsync(
    queue: queueName,
    durable: true,
    exclusive: false,
    autoDelete: false);

await channel.QueueBindAsync(queueName, exchangeName, routingKey);

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");

    // Manual acknowledgment if autoAck is false
    // await channel.BasicAckAsync(ea.DeliveryTag, false);
};

await channel.BasicConsumeAsync(
    queue: queueName,
    autoAck: true,
    consumer: consumer);

Console.WriteLine(" [*] Waiting for messages. Press any key to exit");
Console.ReadKey();

//basic usage of rabbit includes declaring queue and using default exchange and specifying queue for exchange and consumer