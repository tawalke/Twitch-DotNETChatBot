using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lex.Model;
using Amazon.Lex;
using Amazon.CognitoIdentity;
using Microsoft.Extensions.Options;

namespace LexWebChatbot.DataService
{
    public class AWSLexService : IAWSLexService
    {
        private AWSOptions _awsOptions;
        private Dictionary<string, string> _lexSessionAttributes;
        private AmazonLexClient awsLexClient;
        private CognitoAWSCredentials awsCredentials;
        public AWSLexService(IOptions<AWSOptions> awsOptions)
        {
            _awsOptions = awsOptions.Value;
            //InitLexService here or with a function
            InitializeLex();  

        }

        protected void InitializeLex() //-> Pull the Cognito code out and use the CognitoLexLibrary we created
        {
            Amazon.RegionEndpoint svcRegionEndpoint;
            svcRegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(_awsOptions.LexBotRegion);

            awsCredentials = new CognitoAWSCredentials(
                                    _awsOptions.CognitoPoolID,
                                    svcRegionEndpoint);

            // Init lex
            awsLexClient = new AmazonLexClient(awsCredentials, svcRegionEndpoint);
                                

        }

        public async Task<PostTextResponse> ChatByTextToLex(string lexSessionID, string messageToSend)
        {
            PostTextResponse lexTextResponse;
            PostTextRequest lexTextRequest = new PostTextRequest()
            {
                BotAlias = _awsOptions.LexBotAlias,
                BotName = _awsOptions.LexBotName,
                InputText = messageToSend,
                UserId = lexSessionID,
                SessionAttributes = _lexSessionAttributes
            };

            try
            {
                lexTextResponse = await awsLexClient.PostTextAsync(lexTextRequest);
            }
            catch(Exception ex)
            {
                throw new BadRequestException(ex);
            }

            return lexTextResponse;
        }

        public async Task<PostTextResponse> ChatByTextToLex(string lexSessionID, Dictionary<string, string> lexSessionAttributes, string messageToSend)
        {
            _lexSessionAttributes = lexSessionAttributes;
            return await ChatByTextToLex(lexSessionID, messageToSend);
        }

        public Task<PostContentResponse> ChatByVoiceToLex(string lexSessionID, Stream voiceMessageToSend)
        {
            throw new NotImplementedException();
        }

        public Task<PostContentResponse> ChatByVoiceToLex(string lexSessionID, Dictionary<string, string> lexSessionAttributes, Stream voiceMessageToSend)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AWSLexService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
