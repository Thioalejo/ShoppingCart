using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static Android.Provider.SyncStateContract;

namespace ShoppingCart.Service
{
    public class ComsumirSer
    {
        HttpClient client;

        public ComsumirSer()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

       // public async Task<List<Apoyo>> RefreshDataAsync()
		//{
          //  var RestUrl = "http://developer.xamarin.com:8081/api/todoitems{0}";
            //var uri = new Uri(string.Format(Constants.InterfaceConsts, string.Empty));
			  
			//var response = await client.GetAsync(uri);
            //if (response.IsSuccessStatusCode)
            //{
              //  var content = await response.Content.ReadAsStringAsync();
              //  var Items = JsonConvert.DeserializeObject<List<Apoyo>>(content);

            //}

//		}
    }
}
