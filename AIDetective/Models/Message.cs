// Models/Message.cs
namespace DetectiveAI.Models
{
    public class Message
    {
        public string Role { get; set; }
        public string Content { get; set; }
        public Message() { }

        public Message(string role, string content)
        {
            Role = role;
            Content = content;
        }
    }
}