using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Vapor.Common.oAuth.Context;
using Vapor.Common.oAuth.Impl;
using Westwind.Web.WebApi;

namespace Vapor.Controllers
{
    public class HomeController : Controller
    {
        public string ConsumerKey = "0uukv26q3yeoqur";
        public string ConsumerSecret = "4uijwi5z739m2n7";

        public ActionResult Index()
        {
            var consumerContext = new OAuthConsumerContext(ConsumerKey, ConsumerSecret);

            // build up the oauth session
            var serviceContext = new OAuthServiceContext("https://api.dropbox.com/1/oauth/request_token",
                                                            "https://www.dropbox.com/1/oauth/authorize", Url.Action("About"),
                                                            "https://api.dropbox.com/1/oauth/access_token");

            String requestTokenUrl = OAuthUrlGenerator.GenerateRequestTokenUrl(serviceContext.RequestTokenUrl, consumerContext);
            var currentRequestToken = GetAsync<dynamic>(requestTokenUrl).Result;

            //TODO: Create Authorization URL 
            //var authURL = OAuthUrlGenerator.GenerateAuthorizationUrl("https://www.dropbox.com/1/oauth/authorize",
            //                                                         Url.Action("About"),
            //                                                         currentRequestToken.Result);
            ViewBag.Message = currentRequestToken;
            return View();
        }


        public static async Task<T> GetAsync<T>(string uri)
        {
            var client = new HttpClient();

           // var responseMessage =  client.GetAsync(uri).Result;
            //TODO: This line for await doesn't work.  Bug?!
            var responseMessage = await client.GetAsync(uri).ConfigureAwait( false );
            responseMessage.EnsureSuccessStatusCode();
            return await responseMessage.Content.ReadAsAsync<T>();
        }


        //protected HttpRequestMessage GetHttpRequestMessage<T>(T data)
        //{
        //    var mediaType = new MediaTypeHeaderValue("application/json");
        //    var jsonSerializerSettings = new JsonSerializerSettings();
        //    jsonSerializerSettings.Converters.Add(new IsoDateTimeConverter());

        //    var jsonFormatter = new JsonNetFormatter(jsonSerializerSettings);

        //    var requestMessage = new HttpRequestMessage<T>(data, mediaType, new MediaTypeFormatter[] { jsonFormatter });

        //    return requestMessage;
        //}


        public ActionResult About()
        {
            ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your quintessential contact page.";

            return View();
        }

        
    }
}
