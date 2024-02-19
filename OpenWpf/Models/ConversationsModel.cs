namespace OpenWpf.Models
{
    using System.Collections.Generic;

    public class ConversationsModel
    {
        public List<ConversationModel> Conversations { get; set; }
        public ConversationsModel() => Conversations = new List<ConversationModel>();
    }
}