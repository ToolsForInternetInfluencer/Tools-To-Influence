using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using PublicRelations.SocialMedia.Youtube.Utility;
using System.Net.NetworkInformation;

namespace PublicRelations.SocialMedia.Youtube
{
    public partial class MyYoutubeUploadVideo : Form
    {
        public MyYoutubeUploadVideo()
        {
            InitializeComponent();
        }
        String filePathData = "";
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(filePathData)) {
                    
                    errUploadYoutubeVideo.SetError(btnBrowse, "Description should not be left blank!");
                    return;
                }
                 Run().Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var error  in ex.InnerExceptions)
                {
                    Console.WriteLine("Error: " + error.Message);
                }
            }
        }
        private async Task Run()
        {
            UserCredential credential;
            String ClinetJsonPath = "M:\\PR_TOOL_PROJECT\\SampleProjectForApi\\YoutubeSamples\\client_secrets.json.json";
            Console.WriteLine(ClinetJsonPath);

            using (var stream = new FileStream(ClinetJsonPath, FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    // This OAuth 2.0 access scope allows an application to upload files to the
                    // authenticated user's YouTube channel, but doesn't allow other types of access.
                    new[] { YouTubeService.Scope.YoutubeUpload },
                    "user",
                    CancellationToken.None
                );
            }

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
            });

            var video = new Video();
            video.Snippet = new VideoSnippet();
            video.Snippet.Title =  tbTitle.Text;
            video.Snippet.Description = tbDesc.Text;
            video.Snippet.Tags = new string[] { "tag1", "tag2" };
            video.Snippet.CategoryId = "22"; // See https://developers.google.com/youtube/v3/docs/videoCategories/list
            video.Status = new VideoStatus();
            video.Status.PrivacyStatus = cbPrivacyStatus.Text.ToString(); // or "private" or "public"
            var filePath = filePathData; // Replace with path to actual movie file.

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;
                videosInsertRequest.ResponseReceived += videosInsertRequest_ResponseReceived;
                //videosInsertRequest.Upload();
               await videosInsertRequest.UploadAsync();
            }
        }

        void videosInsertRequest_ProgressChanged(Google.Apis.Upload.IUploadProgress progress)
        {
            switch (progress.Status)
            {
                case UploadStatus.Uploading:
                    Console.WriteLine("{0} bytes sent.", progress.BytesSent);
                    break;

                case UploadStatus.Failed:
                    Console.WriteLine("An error prevented the upload from completing.\n{0}", progress.Exception);
                    break;
            }
        }

        void videosInsertRequest_ResponseReceived(Video video)
        {
            Console.WriteLine("Video id '{0}' was successfully uploaded.", video.Id);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            // dlg.ShowDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {

                filePathData = dlg.FileName;
            }
               
        }

        private void MyYoutubeUploadVideo_Load(object sender, EventArgs e)
        {
            cbCategory.DataSource = YoutubeCategory.CatgoryList();
            cbCategory.ValueMember = "Id";
            cbCategory.DisplayMember = "Name";
        }

        private void txtInputValidation(object sender, EventArgs e)
        {
           string textBoxSender = ((TextBox)sender).Name;

          
                switch (textBoxSender)
                {
                    case "tbTitle":
                    errUploadYoutubeVideo.SetError(((TextBox)sender), "Title should not be left blank!");
                    tbTitle.Focus();
                     break;
                    case "tbDesc":
                    errUploadYoutubeVideo.SetError(((TextBox)sender), "Description should not be left blank!");
                    tbDesc.Focus();
                        break;
                    case "tbTags":
                    tbTags.Focus();
                    errUploadYoutubeVideo.SetError(((TextBox)sender), "Tags should not be left blank!");
                    break;
                }
            

             
        }

         
    }
}
