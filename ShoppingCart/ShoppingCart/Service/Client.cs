using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShoppingCart.Service
{
    public static class Client
    {
        

        public async static Task<T>GetRequest<T>(this string url)
        {
				HttpClient client = new HttpClient();
				var response = await client.GetAsync(url);
				var json = await response.Content.ReadAsStringAsync();

                var JSON = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
                //System.Diagnostics.Debug.WriteLine(json);
                return JSON;


                  
				//mal hecho, usar asyn
				//var task = client.GetAsync(url);
				//task.Wait();
				//var response = task.Result;
			}
            
            
           

        }
    }
