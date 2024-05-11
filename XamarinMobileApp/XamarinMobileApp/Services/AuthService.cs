using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Preferences;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Java.Util.Prefs;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Preferences = Xamarin.Essentials.Preferences;

namespace XamarinMobileApp.Services
{
    public static class AuthService
    {
        private static FirebaseAuthClient client;
        static AuthService()
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyB8R87nhBwhgQdFURdDfxZk_9ZAmImv1fU",
                AuthDomain = "mobileapp-38ff9.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            };
            client = new FirebaseAuthClient(config);
        }

        public static async Task Login(string email, string password)
        {
            var userCredential = await client.SignInWithEmailAndPasswordAsync(email, password);
        }
        
        public static async Task Register(string email, string password)
        {
            var userCredential = await client.CreateUserWithEmailAndPasswordAsync(email, password);
        }

        public static void SignOut()
        {
            client.SignOut();
        }
    }
}