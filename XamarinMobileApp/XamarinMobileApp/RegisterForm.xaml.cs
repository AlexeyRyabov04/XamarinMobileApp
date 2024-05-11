using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.Preferences;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMobileApp.Services;

namespace XamarinMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterForm : ContentPage
    {
        public RegisterForm()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            if (passwordEntry.Text == "" || passwordRetype.Text == "")
            {
                await DisplayAlert ("Предупреждение", "Введите пароль!", "Ok");
                return;
            }

            if (passwordEntry.Text != passwordRetype.Text)
            {
                await DisplayAlert ("Предупреждение", "Пароли не совпадают!", "Ok");
            }
            else
            {
                try
                {
                    await AuthService.Register(usernameEntry.Text, passwordEntry.Text);
                    ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Android.App.Application.Context);
                    ISharedPreferencesEditor editor = prefs.Edit();
                    editor.PutString("UserId", usernameEntry.Text);
                    editor.Apply();
                    await Navigation.PushAsync(new MainForm());
                }
                catch (Exception ex)
                {
                    await DisplayAlert ("Предупреждение", "Проверьте данные для входа. Возможно, " +
                                                          "пользователь уже зарегистрирован", "Ok");
                }
            }
        }

        private async void OnLoginTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginForm());
        }
    }
}