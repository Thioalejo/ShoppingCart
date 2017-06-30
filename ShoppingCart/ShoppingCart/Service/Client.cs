using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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


        public static async Task<Productos> Agregar(int id, string nombre, int codigo, double precio, string local)
		{
			Productos producto = new Productos()
			{
				ID = id,
				Nombre = nombre,
				Codigo = codigo,
				Precio = precio,
                Local=local
			};

			HttpClient client = new HttpClient();
            var response = await client.PostAsync("http://10.215.152.16/api/Productos", new StringContent(JsonConvert.SerializeObject(producto),
																										 Encoding.UTF8, "application/json"));

			return JsonConvert.DeserializeObject<Productos>(await response.Content.ReadAsStringAsync());
		}

	}
    }
