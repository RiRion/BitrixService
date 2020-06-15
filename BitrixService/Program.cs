using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BitrixService
{
    class Program
    {
        static void Main(string[] args)
        {
            // var baseUri = "http://alldirect.ru";
            // var prefix = "/my_tools/product.php";
            // var authPrefix = "/auth";
            // var pass = 452896;
            // var cookie = "_ym_visorc_53474710=w; BITRIX_SM_SALE_UID=96a80b0aa8542590f4ad61dc7483a71b; PHPSESSID=OctysC5QQdP2fewABEsUFuciF2KhErR0; BITRIX_SM_SOUND_LOGIN_PLAYED=Y; _ym_isad=2; BITRIX_CONVERSION_CONTEXT_s1=%7B%22ID%22%3A6%2C%22EXPIRE%22%3A1591649940%2C%22UNIQUE%22%3A%5B%22conversion_visit_day%22%5D%7D; BX_USER_ID=60873e52564017c18acbfcfbeb5339a5; _ym_d=1591444631; _ym_uid=1591444631631852530; star_notification_window=freq";
            //
            // var request = new HttpRequestMessage();
            // request.RequestUri = new Uri(baseUri + prefix);
            // request.Method = HttpMethod.Get;
            // // request.Headers.Add("Cookie", cookie);
            // request.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            //     
            // //Request with use Delete method.
            // if (request.Method == HttpMethod.Delete) request.Content = new StringContent(JsonConvert.SerializeObject(new Person() { Pass = pass, ArId = new[] { 199808 } }));
            //
            // //Request with use Post method.
            // if (request.Method == HttpMethod.Post)
            // {
            //     string products;
            //     using (var reader = new StreamReader("/Users/antonmikulcik/Documents/Projects/PHP/Models/TestTXT/product.txt"))
            //     {
            //         products = reader.ReadToEnd();
            //     }
            //     request.Content = new FormUrlEncodedContent(new[]
            //     {
            //     new KeyValuePair<string, string>("Pass", pass.ToString()),
            //     new KeyValuePair<string, string>("str", products)
            //     });
            // }
            Auth();
            //SendRequest(request);
        }
        static async void SendRequest(HttpRequestMessage request)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.SendAsync(request)
                    .Result
                    .Content
                    .ReadAsStringAsync()
                    .GetAwaiter()
                    .GetResult();

                Console.WriteLine(response);
            }
        }

        static void Auth()
        {
            var baseUri = "http://alldirect.ru";
            var authPrefix = "/my_tools/auth.php";
            
            var userAuth = new Dictionary<string, string>()
            {
                {"USER_LOGIN", "admin"},
                {"USER_PASSWORD", "Lb0717511930"},
                {"AUTH_FORM", "Y"},
                {"TYPE", "AUTH"}
            };
            var handler = new HttpClientHandler();
            var cookieContainer = new CookieContainer();
            handler.CookieContainer = cookieContainer;
            using (var client = new HttpClient(handler))
            {
                var uri = new Uri(baseUri + authPrefix);
                var content = new FormUrlEncodedContent(userAuth);
                var response = client.PostAsync(new Uri(baseUri), content).GetAwaiter().GetResult();
                var cookies = cookieContainer.GetCookies(uri);
                foreach (Cookie cookie in cookies)
                {
                    Console.WriteLine(cookie.ToString());
                }
                //Console.WriteLine(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
        }
    }
}
