using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FFImageLoading;
using Firebase.Database;
using Firebase.Storage;
using Java.Lang;
using Newtonsoft.Json;
using Xamarin.Forms;
using Exception = System.Exception;
using Movie = XamarinMobileApp.Models.Movie;

namespace XamarinMobileApp.Services
{
    public class MovieService
    {
        private readonly FirebaseClient firebaseClient;

        public MovieService()
        {
            firebaseClient = new FirebaseClient("https://mobileapp-38ff9-default-rtdb.europe-west1.firebasedatabase.app");
        }
        
        public async Task<List<Movie>> GetMovies()
        {
            var movies = await firebaseClient
                .Child("movies")
                .OnceAsync<Movie>();
            return movies.Select(m => m.Object).OrderBy(m => m.Id).ToList();
        }
        
        public async Task<List<Movie>> GetFavorites(List<int> list)
        {
            var movies = await firebaseClient
                .Child("movies")
                .OnceAsync<Movie>();
            return movies.Select(m => m.Object).Where(m => list.Contains(m.Id)).ToList();
        }
        
        
        public List<string> GetImages(string folderName)
        {
            var url = "https://firebasestorage.googleapis.com/v0/b/mobileapp-38ff9.appspot.com/o/";
            List<string> urls = new List<string>();
            for (int i = 1; i < 10; i++)
            {
                using (WebClient wc = new WebClient())
                {
                    var jsonUrl = url + folderName.Replace("/", "%2F") + $"{i}.jpg";
                    string json = "";
                    try
                    {
                        json = wc.DownloadString(jsonUrl);
                    }
                    catch (Exception ex)
                    {
                        break; 
                    }
                    if (json.Contains("\"error\""))
                    {
                        break;
                    }
                    var token = json.Substring(json.LastIndexOf(':') + 1).Replace("\"", "").Trim().Replace("\n}", "");
                    var resUrl = jsonUrl + "?alt=media&token=" + token;
                    urls.Add(resUrl);
                }
            }

            return urls;
        }
    }
}