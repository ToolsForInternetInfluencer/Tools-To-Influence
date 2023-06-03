using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicRelations.SocialMedia.Youtube.Utility
{
    
    internal class YoutubeCategory
    {
        public class Category { 
          public int Id { get; set; }
            public string Value { get; set; }   
          public string Name { get; set; }

            public Category(int Id, string Name)
            {
                this.Id = Id;
                this.Name = Name;
                
            }
            public Category(String value, string Name)
            {
                this.Value = value;
                this.Name = Name;

            }
        }
        public static List<Category> CatgoryList() {
            List<Category> categories = new List<Category>
            {
                
                    new YoutubeCategory.Category(1, "Film & Animation"),
                    new YoutubeCategory.Category(2, "Autos & Vehicles"),
                     new YoutubeCategory.Category(10, "Music"),
                      new YoutubeCategory.Category(15, "Pets & Animals"),
                      new YoutubeCategory.Category(17, "Sports"),
                       new YoutubeCategory.Category(18, "Short Movies"),
                        new YoutubeCategory.Category(19, "Travel & Events"),
                        new YoutubeCategory.Category(20, "Gaming"),
                        new YoutubeCategory.Category(21, "Videoblogging"),
                        new YoutubeCategory.Category(22, "People & Blogs"),
                        new YoutubeCategory.Category(23, "Comedy"),
                        new YoutubeCategory.Category(24, "Entertainment"),
                        new YoutubeCategory.Category(25, "News & Politics"),
                        new YoutubeCategory.Category(25, "News & Politics"),
                        new YoutubeCategory.Category(26, "Howto & Style"),
                        new YoutubeCategory.Category(27, "Education"),
                        new YoutubeCategory.Category(28, "Science & Technology"),
                         new YoutubeCategory.Category(29, "Nonprofits & Activism"),
                         new YoutubeCategory.Category(30, "Movies"),
                          new YoutubeCategory.Category(31, "Anime/Animation"),
                          new YoutubeCategory.Category(32, "Action/Adventure"),
                          new YoutubeCategory.Category(33, "Classics"),
                           new YoutubeCategory.Category(34, "Comedy"),
                           new YoutubeCategory.Category(35, "Documentary"),
                           new YoutubeCategory.Category(36, "Drama"),
                              new YoutubeCategory.Category(37, "Family"),
                              new YoutubeCategory.Category(38, "Foreign"),
                              new YoutubeCategory.Category(39, "Horror"),
                               new YoutubeCategory.Category(40, "Sci-Fi/Fantasy"),
                               new YoutubeCategory.Category(41, "Thriller"),
                               new YoutubeCategory.Category(42, "Shorts"),
                               new YoutubeCategory.Category(43, "Shows"),
                               new YoutubeCategory.Category(44, "Trailers")

            };

            return categories;

        }

        public static List<Category> PrivacyStatusList()
        {
            List<Category> categories = new List<Category>
            {
                new YoutubeCategory.Category("private", "private"),
                    new YoutubeCategory.Category("public", "public"),
                     new YoutubeCategory.Category("unlisted", "unlisted"),
                     

            };

            return categories;

        }
    }
}
