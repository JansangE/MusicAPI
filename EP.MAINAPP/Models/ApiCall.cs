using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EP.DOMAIN;

namespace EP.MAINAPP.Models
{
    public class ApiCall
    {
        private string _URL;
        private HttpClient _httpClient;
        private string _Class;


        public ApiCall(string Url)
        {
            _URL = Url;
            _httpClient = new HttpClient();
        }

        public string URL
        {
            get => _URL;
        }


        public async Task<List<Piece>> GetAllPieces()
        {
            List<Piece> pieces = new List<Piece>();
            HttpResponseMessage response = await _httpClient.GetAsync($"{_URL}/pieces");
            if (response.IsSuccessStatusCode)
            {
                pieces = await response.Content.ReadAsAsync<List<Piece>>();
            }
            else
            {
                throw new Exception("API DOES NOT RESPOND!");
            }

            return pieces;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
