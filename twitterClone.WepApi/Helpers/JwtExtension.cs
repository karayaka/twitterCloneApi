using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace twitterClone.WepApi.Helpers
{
    public static class JwtExtension
    {
        public static void AddAplicationError(this HttpResponseHeaders httpResponse, string message)
        {
            httpResponse.AddAplicationError(message);
            httpResponse.Add("Access-Control-Allow-Origin", "*");
            httpResponse.Add("Access-Control-Expose-Header", "Application-Error");
        }
    }
}
