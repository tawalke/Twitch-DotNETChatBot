using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexWebChatbot.Models
{
    public class ChatBotMessage
    {
        public int ID { get; set; }
        public BotMessageType MessageType { get; set; }
        public string ChatMessage { get; set; }
    }

    public enum BotMessageType
    {
        UserMessage,
        LexMessage
    }
}
