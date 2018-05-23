using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LexWebChatbot.DataService;
using LexWebChatbot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LexWebChatbot.Controllers
{
    public class LexChatBotController : Controller
    {
        private readonly IAWSLexService awsLexSvc;
        private ISession lexUserSession;
        private Dictionary<string, string> lexSessionData;
        private List<ChatBotMessage> chatBotMessages;
        private string botMsgKey = "BotMessages",
                       botAttribsKey = "LexSessionData",
                       userSessionID = String.Empty;

        public LexChatBotController(IAWSLexService awsLexService)
        {
            awsLexSvc = awsLexService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChatView(List<ChatBotMessage> messages)
        {
            return View(messages);
        }

        public IActionResult ClearBot()
        {
            lexUserSession = HttpContext.Session;

            lexUserSession.Clear();
            chatBotMessages = new List<ChatBotMessage>();
            lexSessionData = new Dictionary<string, string>();

            lexUserSession.Set<List<ChatBotMessage>>(botMsgKey, chatBotMessages);
            lexUserSession.Set<Dictionary<string, string>>(botAttribsKey, lexSessionData);

            awsLexSvc.Dispose();
            return View("ChatView", chatBotMessages);

        }

        public async Task<IActionResult> ProcessChatMessage(string userMsg)
        {
            lexUserSession = HttpContext.Session;
            userSessionID = lexUserSession.Id;

            chatBotMessages = lexUserSession.Get<List<ChatBotMessage>>(botMsgKey) ?? new List<ChatBotMessage>();
            lexSessionData = lexUserSession.Get<Dictionary<string, string>>(botAttribsKey) ?? new Dictionary<string, string>();

            //No messages, return current view
            if (String.IsNullOrEmpty(userMsg)) return View("ChatView", chatBotMessages);

            //We got a message for Lex
            chatBotMessages.Add(new ChatBotMessage()
            { MessageType = BotMessageType.UserMessage, ChatMessage = userMsg });

            //Post to page first? 

            //Strongly type this variable
            var lexResponse = await awsLexSvc.ChatByTextToLex(userSessionID, lexSessionData, userMsg);

            lexSessionData = lexResponse.SessionAttributes;
            chatBotMessages.Add(new ChatBotMessage()
            { MessageType = BotMessageType.LexMessage, ChatMessage = lexResponse.Message});

            lexUserSession.Set<List<ChatBotMessage>>(botMsgKey, chatBotMessages);
            lexUserSession.Set<Dictionary<string, string>>(botAttribsKey, lexSessionData);

            return View("ChatView", chatBotMessages);

        }
    }
}