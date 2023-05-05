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
          public string Name { get; set; }

            public Category(int Id, string Name)
            {
                this.Id = Id;
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
            /*
 1	Film & Animation
2	Autos & Vehicles
10	Music
15	Pets & Animals
17	Sports
18	Short Movies
19	Travel & Events
20	Gaming
21	Videoblogging
22	People & Blogs
23	Comedy
24	Entertainment
25	News & Politics
26	Howto & Style
27	Education
28	Science & Technology
29	Nonprofits & Activism
30	Movies
31	Anime/Animation
32	Action/Adventure
33	Classics
34	Comedy
35	Documentary
36	Drama
37	Family
38	Foreign
39	Horror
40	Sci-Fi/Fantasy
41	Thriller
42	Shorts
43	Shows
44	Trailers*/

            return categories;

        }
    }
}
