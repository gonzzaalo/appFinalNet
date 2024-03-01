using appFinalNet.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace appFinalNet.Repositories
{
    internal class RepositoryProducto
    {
        string urlApi = "https://appfinal-dbd7.restdb.io/rest/final"; HttpClient client = new HttpClient();

        public RepositoryProducto()
        {
            //Configuramos que trabajara con respuesta JSON
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("apikey", "65dcd1e1d34bb06ebe8c2f70");
        }

        public async Task<ObservableCollection<Producto>> GetAllAsync()
        {
            try
            {
                var response = await client.GetStringAsync(urlApi);
                return
                    JsonConvert.DeserializeObject<ObservableCollection<Producto>>(response);
            }
            catch(Exception error)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Hubo un error" +error.Message, "OK");
                return null;
            }
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var response = await client.DeleteAsync($"{urlApi}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddAsync(Producto producto)
        {
            var resonse = await client.PostAsync(urlApi, new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json"));
            return resonse.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Producto producto)
        {
            var resonse = await client.PutAsync($"{urlApi}/{producto._id}",
                new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json"));
            return resonse.IsSuccessStatusCode;
        }
    }
}
