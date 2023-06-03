using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicRelations.SocialMedia.Youtube.Utility
{
    public class YoutubeUpload
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string Tags { get; set; }
        public DateTime CreatedDate { get; set; } 
        public DateTime UpdatedDate { get;set; } 
        public string VideoLink { get;set; }
        public string SucessUploaded { get; set; }

        public string Message { get;set; }
        public string Privacy  { get; set; }


    }
}
