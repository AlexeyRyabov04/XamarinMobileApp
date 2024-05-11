using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMobileApp.Models;
using XamarinMobileApp.Services;

namespace XamarinMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeaturedForm : ContentPage
    {
        private ListView moviesListView;
        public FeaturedForm()
        {
            InitializeComponent();
            moviesListView = new ListView();
            moviesListView.ItemTemplate = new DataTemplate(() =>
            {
                var textCell = new TextCell();
                textCell.SetBinding(TextCell.TextProperty, "Title");
                textCell.SetBinding(TextCell.DetailProperty, "Producer");
                textCell.Tapped += async (sender, e) =>
                {
                    var selectedMovie = (Movie)textCell.BindingContext;
                    Console.WriteLine(selectedMovie.Title);
                    var movieInfoPopup = new MovieInfoForm(selectedMovie);
                    await Navigation.PushAsync(movieInfoPopup);
                    moviesListView.SelectedItem = null;
                };
                return textCell;
            });
            Content = new StackLayout
            {
                Children = { moviesListView }
            };
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var userService = new UserService();
            var features = await userService.GetFeaturesId();
            var firebaseService = new MovieService();
            var movies = await firebaseService.GetFavorites(features);
            moviesListView.ItemsSource = movies;
        }
    }
}