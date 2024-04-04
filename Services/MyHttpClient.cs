using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json; // Asegúrate de tener instalado el paquete NuGet Newtonsoft.Json
using System.Collections.Generic;

namespace BlazorApp.Services
{
    public class MyHttpClient
    {
        private readonly HttpClient _httpClient;
        private const string apiUrl = "http://ip172-18-0-104-co7cn8qim2rg00c0hfd0-32001.direct.labs.play-with-docker.com/gifstenor";

        public MyHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GifItem>> GetSomeDataAsync()
        {
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            var jsonObject = JsonConvert.DeserializeObject<RootObject>(responseData);
            return jsonObject.Gifs;
        }
    }

    public class RootObject
    {
        public List<GifItem> Gifs { get; set; }
    }

    public class GifItem
    {
        public string Title { get; set; }
        public List<MediaItem> Media { get; set; }
    }

    public class MediaItem
    {
        public Gif Gif { get; set; }
    }

    public class Gif
    {
        public string Url { get; set; }
    }
}
