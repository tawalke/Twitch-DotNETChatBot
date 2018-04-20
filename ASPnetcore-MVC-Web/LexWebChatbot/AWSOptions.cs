using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LexWebChatbot
{
    public class AWSOptions
    {
        public AWSOptions() { }

        public string CognitoPoolID { get; set; }
        public string LexBotName { get; set; }
        public string LexBotAlias { get; set; }
        public string LexBotRegion { get; set; }
        public string LexRole { get; set; }
    }
}
