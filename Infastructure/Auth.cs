using GogoKit;
using GogoKit.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace ShowMeTheTickets.Infastructure
{
    public class Auth
    {
        private const string ClientIdentifier = "TaRJnBcw1ZvYOXENCtj5";
        private const string ClientSecret = "ixGDUqRA5coOHf3FQysjd704BPptwbk6zZreELW2aCYSmIT8XJ9ngvN1MuKV";

        private static readonly string[] Scopes = { "read:user" };
        public readonly IViagogoClient _viagogoClient;

        // HomeController Constructor
        public Auth()
        {
            _viagogoClient = new ViagogoClient(
                                new ProductHeaderValue("ShowMeTheTickets"),
                                ClientIdentifier,
                                ClientSecret
                                );
        }

        // Async Initialize Token
        public async Task<OAuth2Token> InitializeToken()
        {
            var token = await _viagogoClient.OAuth2.GetClientAccessTokenAsync(Scopes);
            await _viagogoClient.TokenStore.SetTokenAsync(token);
            return token;
        }

        // Async task to renew Token
        public async Task<OAuth2Token> RenewToken(OAuth2Token oldToken)
        {
            var token = await _viagogoClient.OAuth2.RefreshAccessTokenAsync(oldToken);

            return token;
        }
    }
}