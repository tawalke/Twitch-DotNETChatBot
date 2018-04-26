using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lex.Model;

namespace LexWebChatbot.DataService
{
    public interface IAWSLexService : IDisposable
    {
        Task<PostTextResponse> ChatByTextToLex(string lexSessionID, string messageToSend);

        Task<PostTextResponse> ChatByTextToLex(string lexSessionID, Dictionary<string, string> lexSessionAttributes, string messageToSend);

        Task<PostContentResponse> ChatByVoiceToLex(string lexSessionID, Stream voiceMessageToSend);

        Task<PostContentResponse> ChatByVoiceToLex(string lexSessionID, Dictionary<string, string> lexSessionAttributes, Stream voiceMessageToSend);
    }
}
