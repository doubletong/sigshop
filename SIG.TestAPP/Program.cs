using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SIG.TestAPP
{
    class Program
    {
        static void Main(string[] args)
        {
            //var response = Task.Run(() => MainAsync()).Result;

            //Console.WriteLine(response);

            //Console.ReadLine();
            Console.WriteLine("Operating System Detaiils");
            OperatingSystem os = Environment.OSVersion;
            Console.WriteLine("OS Version: " + os.Version.ToString());
            Console.WriteLine("OS Platoform: " + os.Platform.ToString());
            Console.WriteLine("OS SP: " + os.ServicePack.ToString());
            Console.WriteLine("OS Version String: " + os.VersionString.ToString());
            Console.WriteLine();
            Console.ReadLine();
        }

        //public static async Task<string> MainAsync()
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        httpClient.BaseAddress = new Uri("http://localhost:8000");

        //        var accessToken = await GetAccessToken();

        //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        //        var response = await httpClient.GetAsync("/api/Product");

        //        return $"Status Code: {response.StatusCode}\nContent: {await response.Content.ReadAsStringAsync()}";
        //    }
        //}

        //public static async Task<string> GetAccessToken()
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        httpClient.BaseAddress = new Uri("http://localhost:5000");

        //        var httpContent = new StringContent("{Username: \"mosalla@gmail.com\", Password: \"123654\"}", Encoding.UTF8, "application/json");

        //        var response = await httpClient.PostAsync("/Token/Generate", httpContent);

        //        return await response.Content.ReadAsStringAsync();
        //    }
        //}
    }
}

