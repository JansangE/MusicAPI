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
        

        #region CRUD Composer

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

        public async Task<DOMAIN.Composer> GetSelectedComposer(DOMAIN.Composer composer)
        {
            DOMAIN.Composer selectedComposer = composer;

            HttpResponseMessage response = await _httpClient.GetAsync($"{_URL}/composer?id={selectedComposer.ID}");
            if (response.IsSuccessStatusCode)
            {
                composer = await response.Content.ReadAsAsync<DOMAIN.Composer>();
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
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{_URL}/composer", byteContent);

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

        public async Task<DOMAIN.Composer> UpdateComposer(DOMAIN.Composer composer)
        {
            var newComposer = new DOMAIN.Composer();
            var myContent = JsonConvert.SerializeObject(composer);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"{_URL}/composer?id={composer.ID}", byteContent);

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

        public async Task<string> DeleteComposer(DOMAIN.Composer composer)
        {
            string result = null;
            var selectedComposer = new DOMAIN.Composer();

            HttpResponseMessage response = await _httpClient.DeleteAsync($"{_URL}/composer?id={composer.ID}");

            if (response.IsSuccessStatusCode)
            {
                //selectedComposer = await response.Content.ReadAsAsync<DOMAIN.Composer>();
                result = $"{selectedComposer.NickName} is deleted from DB";
            }
            else
            {
                throw new Exception("API DOES NOT RESPOND!");
                result = "API DOES NOT RESPOND!";
            }

            return result;
        }
        #endregion

        #region CRUD Artist
        //Artist
        public async Task<ObservableCollection<DOMAIN.Artist>> GetAllArtists()
        {
            ObservableCollection<DOMAIN.Artist> artists = new ObservableCollection<DOMAIN.Artist>();
            HttpResponseMessage response = await _httpClient.GetAsync($"{_URL}/artists");
            if (response.IsSuccessStatusCode)
            {
                artists = await response.Content.ReadAsAsync<ObservableCollection<DOMAIN.Artist>>();
            }
            else
            {
                throw new Exception("API DOES NOT RESPOND!");
            }

            return artists;
        }

        public async Task<DOMAIN.Artist> GetSelectedArtist(DOMAIN.Artist artist)
        {
            DOMAIN.Artist selectedArtist = artist;

            HttpResponseMessage response = await _httpClient.GetAsync($"{_URL}/artist?id={selectedArtist.ID}");
            if (response.IsSuccessStatusCode)
            {
                artist = await response.Content.ReadAsAsync<DOMAIN.Artist>();
            }
            else
            {
                throw new Exception("API DOES NOT RESPOND!");
            }

            return artist;
        }

        public async Task<DOMAIN.Artist> AddArtist(DOMAIN.Artist artist)
        {
            var newArtist = new DOMAIN.Artist();
            var myContent = JsonConvert.SerializeObject(artist);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{_URL}/artist", byteContent);

            if (response.IsSuccessStatusCode)
            {
                newArtist = await response.Content.ReadAsAsync<DOMAIN.Artist>();
            }
            else
            {
                throw new Exception("API DOES NOT RESPOND!");
            }

            return newArtist;
        }

        public async Task<DOMAIN.Artist> UpdateArtist(DOMAIN.Artist artist)
        {
            var newArtist = new DOMAIN.Artist();
            var myContent = JsonConvert.SerializeObject(artist);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"{_URL}/artist?id={artist.ID}", byteContent);

            if (response.IsSuccessStatusCode)
            {
                newArtist = await response.Content.ReadAsAsync<DOMAIN.Artist>();
            }
            else
            {
                throw new Exception("API DOES NOT RESPOND!");
            }

            return newArtist;
        }

        public async Task<string> DeleteArtist(DOMAIN.Artist artist)
        {
            string result = null;

            HttpResponseMessage response = await _httpClient.DeleteAsync($"{_URL}/artist?id={artist.ID}");

            if (response.IsSuccessStatusCode)
            {
                //selectedComposer = await response.Content.ReadAsAsync<DOMAIN.Composer>();
                
            }
            else
            {
                throw new Exception("API DOES NOT RESPOND!");
                result = "API DOES NOT RESPOND!";
            }

            return result;
        }

        #endregion
        //LChart

        #region CRUD Type
        //Type
        public async Task<ObservableCollection<DOMAIN.Type>> GetAllTypes()
        {
            ObservableCollection<DOMAIN.Type> types = new ObservableCollection<DOMAIN.Type>();
            HttpResponseMessage response = await _httpClient.GetAsync($"{_URL}/types");
            if (response.IsSuccessStatusCode)
            {
                types = await response.Content.ReadAsAsync<ObservableCollection<DOMAIN.Type>>();
            }
            else
            {
                throw new Exception("API DOES NOT RESPOND!");
            }

            return types;
        }

        public async Task<DOMAIN.Type> GetSelectedType(DOMAIN.Type type)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_URL}/type?id={type.ID}");
            if (response.IsSuccessStatusCode)
            {
                type = await response.Content.ReadAsAsync<DOMAIN.Type>();
            }
            else
            {
                throw new Exception("API DOES NOT RESPOND!");
            }

            return type;
        }

        public async Task<DOMAIN.Type> AddType(DOMAIN.Type type)
        {
            var myContent = JsonConvert.SerializeObject(type);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{_URL}/type", byteContent);

            if (response.IsSuccessStatusCode)
            {
                type = await response.Content.ReadAsAsync<DOMAIN.Type>();
            }
            else
            {
                throw new Exception("API DOES NOT RESPOND!");
            }

            return type;
        }

        public async Task<DOMAIN.Type> UpdateType(DOMAIN.Type type)
        {
            var myContent = JsonConvert.SerializeObject(type);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"{_URL}/type?id={type.ID}", byteContent);

            if (response.IsSuccessStatusCode)
            {
                type = await response.Content.ReadAsAsync<DOMAIN.Type>();
            }
            else
            {
                throw new Exception("API DOES NOT RESPOND!");
            }

            return type;
        }

        public async Task<string> DeleteType(DOMAIN.Type type)
        {
            string result = null;

            HttpResponseMessage response = await _httpClient.DeleteAsync($"{_URL}/type?id={type.ID}");

            if (response.IsSuccessStatusCode)
            {
                //selectedComposer = await response.Content.ReadAsAsync<DOMAIN.Composer>();
                result = $"{type.NameType} is deleted from DB";
            }
            else
            {
                throw new Exception("API DOES NOT RESPOND!");
                result = "API DOES NOT RESPOND!";
            }

            return result;
        }
        #endregion


        #region CRUD Piece
        //Piece
        public async Task<ObservableCollection<DOMAIN.Piece>> GetAllPieces()
        {
            ObservableCollection<DOMAIN.Piece> pieces = new ObservableCollection<DOMAIN.Piece>();
            HttpResponseMessage response = await _httpClient.GetAsync($"{_URL}/pieces");
            if (response.IsSuccessStatusCode)
            {
                pieces = await response.Content.ReadAsAsync<ObservableCollection<DOMAIN.Piece>>();
            }
            else
            {
                throw new Exception("API DOES NOT RESPOND!");
            }

            return pieces;
        }

        public async Task<DOMAIN.Piece> GetSelectedPiece(DOMAIN.Piece piece)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_URL}/piece?id={piece.ID}");
            if (response.IsSuccessStatusCode)
            {
                piece = await response.Content.ReadAsAsync<DOMAIN.Piece>();
            }
            else
            {
                throw new Exception("API DOES NOT RESPOND!");
            }

            return piece;
        }

        public async Task<DOMAIN.Piece> AddPiece(DOMAIN.Piece piece)
        {
            var myContent = JsonConvert.SerializeObject(piece);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{_URL}/piece", byteContent);

            if (response.IsSuccessStatusCode)
            {
                piece = await response.Content.ReadAsAsync<DOMAIN.Piece>();
            }
            else
            {
                throw new Exception("API DOES NOT RESPOND!");
            }

            return piece;
        }

        public async Task<DOMAIN.Piece> UpdatePiece(DOMAIN.Piece piece)
        {
            var myContent = JsonConvert.SerializeObject(piece);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"{_URL}/piece?id={piece.ID}", byteContent);

            if (response.IsSuccessStatusCode)
            {
                piece = await response.Content.ReadAsAsync<DOMAIN.Piece>();
            }
            else
            {
                throw new Exception("API DOES NOT RESPOND!");
            }

            return piece;
        }

        public async Task<string> DeletePiece(DOMAIN.Piece piece)
        {
            string result = null;

            HttpResponseMessage response = await _httpClient.DeleteAsync($"{_URL}/piece?id={piece.ID}");

            if (response.IsSuccessStatusCode)
            {
                //selectedComposer = await response.Content.ReadAsAsync<DOMAIN.Composer>();
                result = $"{piece.NamePiece} is deleted from DB";
            }
            else
            {
                throw new Exception("API DOES NOT RESPOND!");
                result = "API DOES NOT RESPOND!";
            }

            return result;
        }
        #endregion

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
