using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Json;

namespace ExchangeRate.Resources
{
 
    public class api
    {
        public api()
        {
         
        }

        public async Task<JsonValue> RefreshDataAsync()
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(
                new Uri("http://www.apilayer.net/api/live?access_key=d33112a4414924f8492e2653309dbba6"));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    return jsonDoc;
                }
            }
            
        }

        //public const String ACCESS_KEY = "d33112a4414924f8492e2653309dbba6";
        //public const String BASE_URL = "http://apilayer.net/api/";
        //public const String ENDPOINT = "live";
    }

}