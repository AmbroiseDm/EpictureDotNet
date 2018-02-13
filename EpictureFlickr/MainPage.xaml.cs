using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FlickrNet;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using System.Diagnostics;


// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace EpictureFlickr
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    /// 
    public class flickr_photos
    {
        public string url { get; set; }
        public flickr_photos(string _url)
        {
            url = _url;
        }
    }

    public sealed partial class MainPage : Page
    {
        Flickr flickr;
        public List<flickr_photos> GalleryList = new List<flickr_photos>();
        public List<flickr_photos> FavoriteList = new List<flickr_photos>();
        public flickr_photos Actual_image;

        public MainPage()
        {
            this.InitializeComponent();
            AddToFavorite.Visibility = Visibility.Collapsed;
            AddToFavorite.IsEnabled = false;
            const string apiKey = "8ff993d518112627f5bbec5e08a0f438";

            flickr = new Flickr(apiKey);
        }
        public async System.Threading.Tasks.Task searchPhotoAsync(PhotoSearchOptions options)
        {
            PhotoCollection photos = await flickr.PhotosSearchAsync(options);
            GalleryList.Clear();
            foreach (var photo in photos)
            {
                GalleryList.Add(new flickr_photos(photo.Small320Url));
            }   
            GridImage.ItemsSource = GalleryList;
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            imageSelect.Source = null;
            GridImage.ItemsSource = null;
            string text = Search_text.Text;
            var options = new PhotoSearchOptions
            {
                Tags = text,
                PerPage = 100,
                Page = 1
            };
            
            searchPhotoAsync(options);
        }
        
        private void GridImage_ItemClick(object sender, ItemClickEventArgs e)
        {
            imageSelect.Source = null;
            AddToFavorite.Visibility = Visibility.Visible;
            AddToFavorite.IsEnabled = true;
            var photo = (flickr_photos)e.ClickedItem;
            Actual_image = photo;
            imageSelect.Source = new BitmapImage(new Uri(photo.url));
            GridImage.ItemsSource = null;
        }


        public void Favorite_Click(object sender, RoutedEventArgs e)
        {
            List<flickr_photos> tmp = new List<flickr_photos>();
            foreach (var photo in FavoriteList)
            {
                tmp.Add(new flickr_photos(photo.url));
            }
            tmp.Add(Actual_image);
            FavoriteList = tmp;
            FavoriteImage.ItemsSource = FavoriteList;
            AddToFavorite.Visibility = Visibility.Collapsed;
            AddToFavorite.IsEnabled = false;
            imageSelect.Source = null;

        }
    }
}
