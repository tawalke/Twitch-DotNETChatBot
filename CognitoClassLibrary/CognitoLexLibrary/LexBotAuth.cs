using System;
using Amazon.CognitoIdentity;

namespace CognitoLexLibrary
{
    public class LexBotAuth
    {
        private CognitoAWSCredentials awsCredentials;
        private Amazon.RegionEndpoint RegionEndpoint;
        string cognitoPoolID;
        public LexBotAuth(string poolID,Amazon.RegionEndpoint regionEndpoint )
        {
            this.cognitoPoolID = poolID;
            this.RegionEndpoint = regionEndpoint;
        }

        public CognitoAWSCredentials AuthenticateService()
        {
            awsCredentials = new CognitoAWSCredentials
                ( this.cognitoPoolID, 
                  this.RegionEndpoint
                );

            return awsCredentials;
        }
    }
}
