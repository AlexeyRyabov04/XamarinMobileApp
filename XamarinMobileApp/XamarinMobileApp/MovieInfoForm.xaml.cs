using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Firebase.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMobileApp.Models;
using XamarinMobileApp.Services;

namespace XamarinMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieInfoForm : ContentPage
    {
        public class ImageInformation
        {
            public ImageSource _Image { get; set; }
        }

        private ObservableCollection<ImageInformation> imageCollection;

        public ObservableCollection<ImageInformation> ImageCollection
        {
            get { return imageCollection; }
            set
            {
                imageCollection = value;
                OnPropertyChanged();
            }
        }
        
        public class Image
        {
            public string Url { get; set; }
        }
        ObservableCollection<Image> images = new ObservableCollection<Image>();
        private bool isInfavorites;
        public string ButtonText => IsInfavorites ? "В избранном" : "Добавить в избранное";
        public bool IsInfavorites
        {
            get { return isInfavorites; }
            set
            {
                isInfavorites = value;
                OnPropertyChanged(nameof(IsInfavorites));
                OnPropertyChanged(nameof(ButtonText)); // Сообщаем об изменении текста кнопки
            }
        }

        private Movie _movie;
        public MovieInfoForm(Movie movie)
        {
            InitializeComponent();
            _movie = movie;
            BindingContext = this;
            title.Text = movie.Title;
            producer.Text = movie.Producer;
            description.Text = movie.Description;
            Carousel.ItemsSource = images;
            LoadDataAsync();
        }

        public async void LoadDataAsync()
        {
            var userService = new UserService();
            IsInfavorites = await userService.CheckInFavorites(_movie.Id);
            var movieService = new MovieService();
            List<string> urls = new List<string>();
            await Task.Run(() => urls = movieService.GetImages($"images/{_movie.Id}/"));
            foreach (var url in urls)
            {
                images.Add(new Image(){Url = url});
            }
        }

        private async void AddToFavorites(object sender, EventArgs e)
        {
            var userService = new UserService();
            if (IsInfavorites)
            {
                await userService.DeleteFavorite(_movie.Id);
                IsInfavorites = false;
            }
            else
            {
                await userService.AddFavorite(_movie.Id);
                IsInfavorites = true;
            }
        }

    }
}