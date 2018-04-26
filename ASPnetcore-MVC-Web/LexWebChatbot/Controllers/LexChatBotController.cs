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
    }
}