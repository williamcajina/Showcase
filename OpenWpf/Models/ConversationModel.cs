namespace OpenWpf.Models
{
    using System;
    using System.Collections.Generic;

    public class ConversationModel
    {
        public string ConversationName { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<MessageModel> Messages { get; set; }
        public ConversationModel()
        {
            Messages = new List<MessageModel>();
            CreatedAt = DateTime.Now;
            ConversationName = "";
        }

        public ConversationModel(ConversationModel source)
        {
            ConversationName = source.ConversationName;
            CreatedAt = source.CreatedAt;
            Messages = new List<MessageModel>();
            foreach (MessageModel message in source.Messages)
            {
                Messages.Add(new MessageModel(message));
            }
        }
    }
}