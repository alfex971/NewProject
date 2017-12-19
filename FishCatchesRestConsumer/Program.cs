using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FishCatchesRestConsumer
{
    class Program
    {
        private const string BaseUri = "http://fishcatchesrest.azurewebsites.net/";
        //private static List<Catch> catches = new List<Catch>();
        static void Main(string[] args)
        {
            var byId = GetCatchById(1);
            Console.WriteLine("GetCatchById: " + byId.Result);
            var all = GetAllCatches();
            Console.WriteLine("GetAllCatches: ");
            foreach (var catche in all.Result)
            {
                Console.WriteLine(catche);
            }
            Console.WriteLine("Delete Catch: " + DeleteCatch(1).Result);
            var catch2 = new Catch(){Id = 5,Name = "Georgi",Species = "Roach",Weight = 2.21,Location = "RoSo",Week = 23};
            Console.WriteLine("Insert Catch: " + InsertCatch(catch2).Result);
            Console.ReadKey();
        }
        private static async Task<Catch> GetCatchById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                string content = await client.GetStringAsync($"Service1.svc/catches/{id}");
                Catch catche = JsonConvert.DeserializeObject<Catch>(content);
                return catche;
            }
        }
        private static async Task<IList<Catch>> GetAllCatches()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                string content = await client.GetStringAsync($"Service1.svc/catches/");
                var catche = JsonConvert.DeserializeObject<IList<Catch>>(content);
                return catche;
            }
        }
        private static async Task<int> DeleteCatch(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                var response = await client.DeleteAsync($"Service1.svc/catches/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return 1;
                }
                return 0;
            }
        }
        private static async Task<int> InsertCatch(Catch c)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var catch1 = JsonConvert.SerializeObject(c);
                var content = new StringContent(catch1, Encoding.UTF8, "Application/json");
                var response = await client.PostAsync($"Service1.svc/catches/",content);
                if (response.IsSuccessStatusCode)
                {
                    return 1;
                }
                return 0;
            }
        }
    }
}
