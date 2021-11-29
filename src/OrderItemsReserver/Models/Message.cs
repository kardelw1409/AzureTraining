namespace OrderItemsReserver.Models
{
    public class Message
    {
        public string MessageType { get; set; }
        public OrderRequest Data { get; set; }
    }
}