namespace OpenWpf.Models
{
    using System;

    public class MessageModel
    {
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
        public string FinishReason { get; set; }
        public bool IsMe { get; set; }
        public MessageModel(MessageModel source)
        {
            Content = source.Content;
            CreatedAt = source.CreatedAt;
            IsMe = source.IsMe;
            FinishReason = source.FinishReason;
        }

        public MessageModel()
        {
        }

        public MessageModel(bool isMe)
        {
            CreatedAt = DateTime.Now;
            IsMe = isMe;
        }

        public MessageModel(string content, DateTime createdAt, bool isMe)
        {
            Content = content;
            CreatedAt = createdAt;
            IsMe = isMe;
        }
    }
}