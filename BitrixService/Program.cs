using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BitrixService.Models;
using BitrixService.Services;
using Newtonsoft.Json;

namespace BitrixService
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ApiConfig(
                "http://alldirect.ru",
                "/my_tools/auth.php",
                "/my_tools/product.php",
                "admin",
                "Lb0717511930"
                );
            var api = new ApiService(config);
            var auth = api.Auth().GetAwaiter().GetResult();
            if (auth)
            {
                var respnse = api.GetAsync().GetAwaiter().GetResult();
                Console.WriteLine(respnse);
            }
        }
    }
}
