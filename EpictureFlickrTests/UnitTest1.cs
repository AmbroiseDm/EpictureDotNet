using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;
using System.Collections.Generic;

namespace EpictureFlickrTests
{
    public class flickr_photos
    {
        public string url { get; set; }
        public flickr_photos(string _url)
        {
            url = _url;
        }
    }
    [TestClass]
    public class FlickrApiTests
    {
        List<flickr_photos> GalleryList = new List<flickr_photos>();
        Flickr flickr;
        [TestMethod]
        public void InitialisationDesTests()
        {
            const string apiKey = "8ff993d518112627f5bbec5e08a0f438";

            flickr = new Flickr(apiKey);
        }
        [TestMethod]
        public void RecuperationDePhotos()
        {
            var options = new PhotoSearchOptions
            {
                Tags = "Pokemon",
                PerPage = 20,
                Page = 1
            };
            searchPhotoAsync(options);
        }
        public async System.Threading.Tasks.Task searchPhotoAsync(PhotoSearchOptions options)
        {
            PhotoCollection photos = await flickr.PhotosSearchAsync(options);
            GalleryList.Clear();
            foreach (var photo in photos)   
            {
                GalleryList.Add(new flickr_photos(photo.Small320Url));
            }   
        }        

        [TestMethod]
        public void FinDesTests()
        {

        }
    }
}
