using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHttpUtilsFourTwoNine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string retryAfterResponseHeader = "Initial Value";
            // Change the below to the correct url and port for the service to test against.
            // We want the service to return a 429 with a response header Retry-After
            string URL = "https://localhost:5001/hello";

            try
            {
                Hello helloRequest = new Hello() { Name = "Elvis" };

                URL.PostStringToUrl(helloRequest.ToJson<Hello>(), responseFilter: res =>
                {
                    // this never gets executed if the service called returns a 429 response.
                    retryAfterResponseHeader = res.GetHeader("Retry-After");
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception encountered: {ex.Message}");
            }

            Console.WriteLine("Retry-After: " + retryAfterResponseHeader);

            Console.ReadKey();
        }
    }

    public class Hello
    {
        public string Name { get; set; }
    }
}
