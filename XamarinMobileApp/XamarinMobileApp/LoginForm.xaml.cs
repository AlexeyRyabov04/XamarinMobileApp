using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.Preferences;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMobileApp.Services;

namespace XamarinMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginForm : ContentPage
    {
        public LoginForm()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }
        
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            if (passwordEntry.Text == "")
            {
                await DisplayAlert ("Предупреждение", "Введите пароль!", "Ok");
            }
            else
            {
                try
                {
                    await AuthService.Login(usernameEntry.Text, passwordEntry.Text);
                    ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Android.App.Application.Context);
                    ISharedPreferencesEditor editor = prefs.Edit();
                    editor.PutString("UserId", usernameEntry.Text);
                    editor.Apply();
                    await Navigation.PushAsync(new MainForm());
                }
                catch (Exception ex)
                {
                    await DisplayAlert ("Предупреждение", "Проверьте данные для входа", "Ok");
                }
            }
        }

        private async void OnRegisterTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterForm());
        }
    }
}