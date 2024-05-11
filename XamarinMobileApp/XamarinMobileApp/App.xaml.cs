using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Preferences;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMobileApp.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace XamarinMobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Android.App.Application.Context);
            bool isLoggedin = !string.IsNullOrEmpty(prefs.GetString("UserId", ""));
            if (isLoggedin)
            {
                MainPage = new NavigationPage(new MainForm());
            }
            else
            {
                MainPage = new NavigationPage(new LoginForm());
            }

        }
        protected override void OnStart()
        {
          
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}