namespace OpenWpf.Models
{
    using OpenAI_API.Models;

    public class MessageParametersModel
    {
        public double FrequencyPenalty { get; set; }

        public int MaxTokens { get; set; }

        public string? MessageInput { get; set; }

        public Model? Model { get; set; }

        public int NumberOfChoices { get; set; }
        public double PresencePenalty { get; set; }
        public double Temperature { get; set; }
    }
}