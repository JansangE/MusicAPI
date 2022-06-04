using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EP.DOMAIN;
using Newtonsoft.Json;

namespace EP.MAINAPP.ViewModels
{
    public class ApiCall : IDisposable
    {
        private string _URL;
        private HttpClient _httpClient;


        public ApiCall(/*string Url*/)
        {
            _URL = "http://localhost:5106";
            _httpClient = new HttpClient();
        }

        public string URL
        {
            get => _URL;
        }
        //Artist

        //Composer
        public async Task<ObservableCollection<DOMAIN.Composer>> GetAllComposer()
        {
            ObservableCollection<DOMAIN.Composer> composer = new ObservableCollection<DOMAIN.Composer>();
            HttpResponseMessage response = await _httpClient.GetAsync($"{_URL}/composers");
            if (response.IsSuccessStatusCode)
            {
                composer = await response.Content.ReadAsAsync<ObservableCollection<DOMAIN.Composer>>();
            }
            else
            {
                throw new Exception("API DOES NOT RESPOND!");
            }

            return composer;
        }

        public async Task<DOMAIN.Composer> AddComposer(DOMAIN.Composer composer)
        {
            var newComposer = new DOMAIN.Composer();

            var myContent = JsonConvert.SerializeObject(composer);

            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);

            var byeContent = new ByteArrayContent(buffer);

            byeContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            
            HttpResponseMessage response = await _httpClient.PostAsync($"{_URL}/composer", byeContent);

            if (response.IsSuccessStatusCode)
            {
                newComposer = await response.Content.ReadAsAsync<DOMAIN.Composer>();
            }
            else
            {
                throw new Exception("API DOES NOT RESPOND!");
            }

            return newComposer;
        }

        //LChart

        //Type

        //Piece
        public async Task<List<DOMAIN.Piece>> GetAllPieces()
        {
            List<DOMAIN.Piece> pieces = new List<DOMAIN.Piece>();
            HttpResponseMessage response = await _httpClient.GetAsync($"{_URL}/pieces");
            if (response.IsSuccessStatusCode)
            {
                pieces = await response.Content.ReadAsAsync<List<DOMAIN.Piece>>();
            }
            else
            {
                throw new Exception("API DOES NOT RESPOND!");
            }

            return pieces;
        }

        public async Task<List<DOMAIN.Piece>> GetAllArtists()
        {
            List<DOMAIN.Piece> pieces = new List<DOMAIN.Piece>();
            HttpResponseMessage response = await _httpClient.GetAsync($"{_URL}/artist");
            if (response.IsSuccessStatusCode)
            {
                pieces = await response.Content.ReadAsAsync<List<DOMAIN.Piece>>();
            }
            else
            {
                throw new Exception("API DOES NOT RESPOND!");
            }

            return pieces;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
