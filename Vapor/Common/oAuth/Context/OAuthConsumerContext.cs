using System;

using Vapor.Common.oAuth.Impl;

namespace Vapor.Common.oAuth.Context
{
    internal class OAuthConsumerContext
    {
        public readonly String ConsumerKey;
        public readonly String ConsumerSecret;
        public readonly SignatureTypes SignatureMethod = SignatureTypes.HMACSHA1;

        public OAuthConsumerContext(String consumerKey, String consumerSecret)
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
        }
    }
}
