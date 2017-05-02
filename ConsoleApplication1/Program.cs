using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            string authInfo = "user:pass";
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", authInfo);

            client.BaseAddress = new Uri("http://localhost:27019/");
            HttpResponseMessage response = client.GetAsync("api/values/").Result;
            if (response.IsSuccessStatusCode)
            {
                var reader = new StreamReader(response.Content.ReadAsStreamAsync().Result, ASCIIEncoding.ASCII);
                string responseText = reader.ReadToEnd();
                Console.WriteLine("success: " + responseText);
            }
            else
            {
                Console.WriteLine(response.IsSuccessStatusCode + " FAILURE");
            }
            Console.Read();

        }
    }
}
