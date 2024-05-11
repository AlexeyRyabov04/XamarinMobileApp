using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.Preferences;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using XamarinMobileApp.Services;
using Button = Xamarin.Forms.Button;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace XamarinMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainForm : TabbedPage
    {
        public MainForm()
        {
            NavigationPage.SetHasBackButton(this, false);
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            ContentPage accountPage = new AccountForm();
            ContentPage homePage = new HomeForm();
            ContentPage featuredPage = new FeaturedForm();
            Children.Add(homePage);
            Children.Add(featuredPage);
            Children.Add(accountPage);
            accountPage.Title = "Аккаунт";
            homePage.Title = "Главная";
            featuredPage.Title = "Избранное";
            ToolbarItem item = new ToolbarItem
            {
                Text = "Logout",
                Order = ToolbarItemOrder.Primary,
                Priority = 0,
            };
            item.Clicked += Logout;
            ToolbarItems.Add(item);
        }

        private async void Logout(object sender, EventArgs e)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Android.App.Application.Context);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.Clear();
            editor.Apply();
            await Navigation.PushAsync(new LoginForm());
        }
    }
}