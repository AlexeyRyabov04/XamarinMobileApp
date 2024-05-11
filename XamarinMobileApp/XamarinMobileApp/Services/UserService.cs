using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Content;
using Android.Preferences;
using Firebase.Database;
using Firebase.Database.Query;
using XamarinMobileApp.Models;

namespace XamarinMobileApp.Services
{
    public class UserService
    {
        private readonly FirebaseClient firebaseClient;
        private string _key;
        
        public UserService()
        {
            firebaseClient = new FirebaseClient("https://mobileapp-38ff9-default-rtdb.europe-west1.firebasedatabase.app");
        }

        public async Task<Account> GetUser()
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Android.App.Application.Context);
            var email = prefs.GetString("UserId", "");
            var users = await firebaseClient
                .Child("users")
                .OnceAsync<Account>();
            var user = users.FirstOrDefault(u => u.Object.Email == email);
            _key = user.Key;
            return user.Object;
        }

        public async Task<bool> CheckInFavorites(int id)
        {
            var user = await GetUser();
            return user.Favourites.Contains(id);
        }
        public async Task<List<int>> GetFeaturesId()
        {
            var user = await GetUser();
            return user.Favourites;
        }
        
        public async Task AddFavorite(int id)
        {
            var user = await GetUser();
            user.Favourites.Add(id);
            await firebaseClient
                .Child("users")
                .Child(_key)
                .PutAsync(user);
        }

        public async Task DeleteFavorite(int id)
        {
            var user = await GetUser();
            user.Favourites.Remove(id);
            await firebaseClient
                .Child("users")
                .Child(_key)
                .PutAsync(user);
        }

        public async Task SaveChanges(string name, string surname, string nickname, string phone)
        {
            var user = await GetUser();
            user.Nick = nickname;
            user.Name = name;
            user.Surname = surname;
            user.Phone = phone;
            await firebaseClient
                .Child("users")
                .Child(_key)
                .PutAsync(user);
        }
    }
}