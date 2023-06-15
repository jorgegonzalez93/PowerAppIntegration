namespace Migration.Domain.Domain.DTOs
{
    public class Message
    {
        public Guid Id { get; set; } = default!;
        public object MessageInfo { get; set; } = default!;

        public Message(object messageInfo)
        {
            MessageInfo = messageInfo;
        }

        public Message()
        {

        }
    }
}
