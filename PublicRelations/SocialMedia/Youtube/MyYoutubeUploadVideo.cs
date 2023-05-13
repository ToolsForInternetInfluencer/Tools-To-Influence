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
using System.Data.SqlClient;

namespace PublicRelations.SocialMedia.Youtube
{
    public partial class MyYoutubeUploadVideo : Form
    {
        bool isErrorOccured = false;
        string connetionString;
        SqlConnection cnn;
        string settingJsonPath = "";
        public MyYoutubeUploadVideo()
        {
            InitializeComponent();
            connetionString = @"Server=.\SqlExpress01; Database= SocialMedia; Integrated Security=True;";
            cnn = new SqlConnection(connetionString);
            SqlCommand command = new SqlCommand("Select * from SocialSetting where SettingName=@SettingName", cnn);
            command.Parameters.AddWithValue("@SettingName", "YoutubeJsonPath");
            cnn.Open();
            // int result = command.ExecuteNonQuery();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    // Console.WriteLine(String.Format("{0}", reader["SettingName"]));
                    settingJsonPath = String.Format("{0}", reader["SettingValue"]);
                }
            }

            cnn.Close();
        }
        String filePathData = "";
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (isErrorOccured) {
                    return;
                }
                if (String.IsNullOrEmpty(filePathData)) {
                    isErrorOccured = true;
                    errUploadYoutubeVideo.SetError(btnBrowse, "Description should not be left blank!");
                    return;
                }
                 Run().Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var error  in ex.InnerExceptions)
                {
                    uploadYoutubeStatus.Text = "Error " + error.Message;
                }
            }
        }
        private async Task Run()
        {
            UserCredential credential;
            //get from database
            //String ClinetJsonPath = "M:\\PR_TOOL_PROJECT\\SampleProjectForApi\\YoutubeSamples\\client_secrets.json.json";
            Console.WriteLine(ClinetJsonPath);

            using (var stream = new FileStream(settingJsonPath, FileMode.Open, FileAccess.Read))
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
            //save this infomration  in database
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
                    uploadYoutubeStatus.Text = " bytes sent." + progress.BytesSent;
                    break;

                case UploadStatus.Failed:
                    uploadYoutubeStatus.Text = "An error prevented the upload from completing.\n" + progress.Exception;
                    break;
            }
        }

        void videosInsertRequest_ResponseReceived(Video video)
        {
             
            uploadYoutubeStatus.Text = "Video  was successfully uploaded." + video.Id.ToString(); 
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
                    if (String.IsNullOrEmpty(((TextBox)sender).Text))
                    {
                        errUploadYoutubeVideo.SetError(((TextBox)sender), "Title should not be left blank!");
                        isErrorOccured = true;
                        tbTitle.Focus();
                    }
                    else {
                        errUploadYoutubeVideo.Clear();
                        isErrorOccured = false;
                    }
                   
                     break;
                    case "tbDesc":

                    if (String.IsNullOrEmpty(((TextBox)sender).Text))
                    {
                        errUploadYoutubeVideo.SetError(((TextBox)sender), "Description should not be left blank!");
                        isErrorOccured = true;
                        tbDesc.Focus();
                    }
                    else {
                        errUploadYoutubeVideo.Clear();
                        isErrorOccured = false;
                    }
                        break;
                    case "tbTags":

                    if (String.IsNullOrEmpty(((TextBox)sender).Text))
                    {
                        tbTags.Focus();
                        isErrorOccured = true;
                        errUploadYoutubeVideo.SetError(((TextBox)sender), "Tags should not be left blank!");
                    }
                    else
                    {
                        errUploadYoutubeVideo.Clear();
                        isErrorOccured = false;
                    }

                    break;
                }
            

             
        }

         
    }
}
