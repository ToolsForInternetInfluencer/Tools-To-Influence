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
using Newtonsoft.Json.Linq;

namespace PublicRelations.SocialMedia.Youtube
{
    public partial class MyYoutubeUploadVideo : Form
    {
        bool isErrorOccured = false;
        string connetionString = @"Server=.\SqlExpress01; Database= SocialMedia; Integrated Security=True;";
        SqlConnection cnn;
        string settingJsonPath = "";
        YoutubeUpload newtest = new YoutubeUpload();
        public MyYoutubeUploadVideo()
        {
            InitializeComponent();
           
            youtubeUploadStatus.Text = "Youtube Upload Form loaded";
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
                if (isErrorOccured)
                {
                    return;
                }
                if (String.IsNullOrEmpty(filePathData))
                {
                    isErrorOccured = true;
                    errUploadYoutubeVideo.SetError(btnBrowse, "Description should not be left blank!");
                    return;
                }
               
                Run().Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var error in ex.InnerExceptions)
                {
                    uploadYoutubeStatus.Text = "Error " + error.Message;
                }
            }
            finally {
              
               
                
                this.Close();
            }
        }
        private async Task Run()
        {
            UserCredential credential;
            uploadYoutubeStatus.Invoke((MethodInvoker)(() => uploadYoutubeStatus.Text = "Video  was successfully uploaded." ));
            //get from database
            //String ClinetJsonPath = "M:\\PR_TOOL_PROJECT\\SampleProjectForApi\\YoutubeSamples\\client_secrets.json.json";
            Console.WriteLine(settingJsonPath);

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
            newtest.Title = tbTitle.Text;
            newtest.Description = tbDesc.Text;
            newtest.Tags = tbTags.Text;
            newtest.CategoryId = cbCategory.Text;
            newtest.Privacy = cbPrivacyStatus.Text.ToString();

            video.Snippet.Title =  tbTitle.Text;
            video.Snippet.Description = tbDesc.Text;
            // video.Snippet.Tags = new string[] { "tag1", "tag2" };
            if (lsttag.Items.Count > 0)
            { 
                video.Snippet.Tags = lsttag.Items.Cast<string>().ToArray(); 
            }
            else {
                video.Snippet.Tags = new string[] { "public Video" };
            }
            
            video.Snippet.CategoryId = cbCategory.SelectedValue.ToString(); // See https://developers.google.com/youtube/v3/docs/videoCategories/list
            video.Status = new VideoStatus();
            video.Status.PrivacyStatus = cbPrivacyStatus.Text.ToString(); // or "private" or "public"
            var filePath = filePathData; // Replace with path to actual movie file.

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;
                videosInsertRequest.ResponseReceived += videosInsertRequest_ResponseReceived;
                // videosInsertRequest.Upload();
               
               await videosInsertRequest.UploadAsync();
            }
        }

        void videosInsertRequest_ProgressChanged(Google.Apis.Upload.IUploadProgress progress)
        {
            switch (progress.Status)
            {
                case UploadStatus.Uploading:
                    youtubeUploadStatus.Text = " bytes sent." + progress.BytesSent;
                    
                    break;

                case UploadStatus.Failed:

                    youtubeUploadStatus.Text = "An error prevented the upload from completing.\n" + progress.Exception;
                    insertData(youtubeUploadStatus.Text, "0", "Failed");
                    break;
            }
        }

        void videosInsertRequest_ResponseReceived(Video video)
        {
            String videoId = video.Id.ToString();
            //  uploadYoutubeStatus.Invoke((MethodInvoker)(() => uploadYoutubeStatus.Text = "Video  was successfully uploaded." + videoId));
            youtubeUploadStatus.Text = "Video  was successfully uploaded." + videoId;
            
            insertData("Sucess", "1", video.Id.ToString());
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
            cbPrivacyStatus.DataSource = YoutubeCategory.PrivacyStatusList();
            cbPrivacyStatus.ValueMember = "Value";
            cbPrivacyStatus.DisplayMember = "Name";
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

        private void insertData(String message, String sucessstatus, String videoLink) {
            try
            {
                string query = "INSERT INTO YoutubeUpload (Title, Description, Tags,CategoryId,CreatedDate,UpdatedDate,Videolink,SucessUploaded,Message,LocalVideoPath,Privacy)" +
                    " VALUES(@Title, @Description, @Tags,@CategoryId,@CreatedDate,@UpdatedDate,@Videolink,@SucessUploaded,@Message,@LocalVideoPath,@Privacy)";
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                SqlCommand cmd = new SqlCommand(query, cnn);
             
                newtest.Title = tbTitle.Text;
                newtest.Description = tbDesc.Text;
                newtest.Tags = tbTags.Text;
                newtest.CategoryId = lblCategory.Text;
                newtest.Privacy = lblPrivacystatus.Text;  
                cmd.Parameters.AddWithValue("@Title", newtest.Title);
                cmd.Parameters.AddWithValue("@Description", newtest.Description);

               
                if (lsttag.Items.Count > 0)
                {
                    List<String>  tagValue = lsttag.Items.Cast<string>().ToList();
                    StringBuilder strValue = new StringBuilder();   
                    for (int i = 0; i < tagValue.Count; i++)
                    {
                        strValue.Append(tagValue[i]);
                    }


                        cmd.Parameters.AddWithValue("@Tags", strValue.ToString());
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Tags", "public Video");
                }
                cmd.Parameters.AddWithValue("@CategoryId", newtest.CategoryId);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@Videolink", videoLink);
                cmd.Parameters.AddWithValue("@SucessUploaded", sucessstatus);
                cmd.Parameters.AddWithValue("@Message", message);
                cmd.Parameters.AddWithValue("@LocalVideoPath", filePathData);
                cmd.Parameters.AddWithValue("@Privacy", newtest.Privacy);
                
                cmd.ExecuteNonQuery();
                MyYoutubeUploaded myyoutubeUplaod = new MyYoutubeUploaded();
                myyoutubeUplaod.Show();
            }
            finally {
                MyYoutubeUploaded myyoutubeUplaod  = new MyYoutubeUploaded();
                myyoutubeUplaod.Show();

                cnn.Close(); 

            }    


        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblmovetolist_Click(object sender, EventArgs e)
        {
            lsttag.Items.Add(tbTags.Text);
        }

        private void cbCategory_Leave(object sender, EventArgs e)
        {
            lblCategory.Text = cbCategory.SelectedValue.ToString();
        }

        private void lblPrivacystatus_Leave(object sender, EventArgs e)
        {
            lblPrivacystatus.Text = cbPrivacyStatus.SelectedValue.ToString();
        }

        private void cbPrivacyStatus_Leave(object sender, EventArgs e)
        {
            lblPrivacystatus.Text = cbPrivacyStatus.SelectedValue.ToString();
        }
    }
    
}
