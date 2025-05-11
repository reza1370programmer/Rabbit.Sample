
using RabbitMQ.Client;

namespace Producer.RabbitService
{
    public class RabbitService : IRabbitService
    {
        private string ExchangeName = "MyExchange";
        private ConnectionFactory ConnectionFactory { get; set; }
        public IConnection? Connection { get; set; }
        public IChannel? Channel { get; set; }
        public  RabbitService()
        {
             ConnectionFactory = new ConnectionFactory() { HostName = "localhost" };
             
        }
        public async Task RabbitConsumer()
        {
            
        }

        public async Task RabbitProducer(string Body, string RoutingKey, string QueueName)
        {
            Connection = await ConnectionFactory.CreateConnectionAsync();
            Channel = await Connection.CreateChannelAsync();
        }
    }
}
