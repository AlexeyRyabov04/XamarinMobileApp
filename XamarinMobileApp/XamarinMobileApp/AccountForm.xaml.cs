using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMobileApp.Services;

namespace XamarinMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountForm : ContentPage
    {
        public AccountForm()
        {
            InitializeComponent();
            LoadFields();
            
        }

        private async void LoadFields()
        {
            var userService = new UserService();
            var user = await userService.GetUser();
            nameEntry.Text = user.Name;
            surnameEntry.Text = user.Surname;
            nickEntry.Text = user.Nick;
            phoneEntry.Text = user.Phone;
        }
        private async void SaveChanges(object sender, EventArgs e)
        {
            var userService = new UserService();
            await userService.SaveChanges(nameEntry.Text, surnameEntry.Text, nickEntry.Text, phoneEntry.Text);
            await DisplayAlert ("Успешно", "Изменения сохранены", "Ok");
        }
    }
}