using PublicRelations.SocialMedia.Youtube.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PublicRelations.SocialMedia.Youtube
{
    public partial class MyYoutubeUploaded : Form
    {
        string connetionString = @"Server=.\SqlExpress01; Database= SocialMedia; Integrated Security=True;";
        SqlConnection cnn;
        List<YoutubeUpload> youtubeUploads = null;
        public MyYoutubeUploaded()
        {
            InitializeComponent();
            youtubeUploads = new List<YoutubeUpload>();
        }
        public MyYoutubeUploaded(string status)
        {
            InitializeComponent();
            youtubeUploads = new List<YoutubeUpload>();
        }

        private void MyYoutubeUploaded_Load(object sender, EventArgs e)
        {
            string sqlQuery = "Select * from YoutubeUpload";
            cnn = new SqlConnection(connetionString);
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            
            cnn.Open();
            // int result = command.ExecuteNonQuery();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        YoutubeUpload youtubeUplaod = new YoutubeUpload();  


                        Console.WriteLine(String.Format("{0}", reader["Id"]));
                        youtubeUplaod.Id = String.Format("{0}", reader["Id"]);
                        Console.WriteLine(String.Format("{0}", reader["Title"]));
                        youtubeUplaod.Title = (String.Format("{0}", reader["Title"]));
                        Console.WriteLine(String.Format("{0}", reader["Description"]));
                        youtubeUplaod.Description = String.Format("{0}", reader["Description"]);
                        Console.WriteLine(String.Format("{0}", reader["Tag"]));
                       
                        Console.WriteLine(String.Format("{0}", reader["CategoryId"]));
                        youtubeUplaod.CategoryId = String.Format("{0}", reader["CategoryId"]);
                        Console.WriteLine(String.Format("{0}", reader["Tags"]));
                        youtubeUplaod.Tags = String.Format("{0}", reader["Tags"]);
                        Console.WriteLine(String.Format("{0}", reader["CreatedDate"]));
                        youtubeUplaod.CreatedDate = String.Format("{0}", reader["CreatedDate"]);
                        Console.WriteLine(String.Format("{0}", reader["UpdatedDate"]));
                        youtubeUplaod.UpdatedDate = String.Format("{0}", reader["UpdatedDate"]);
                        Console.WriteLine(String.Format("{0}", reader["Videolink"]));
                        youtubeUplaod.VideoLink = String.Format("{0}", reader["Videolink"]);
                        Console.WriteLine(String.Format("{0}", reader["SucessUploaded"]));
                        youtubeUplaod.SucessUploaded = String.Format("{0}", reader["SucessUploaded"]);
                        Console.WriteLine(String.Format("{0}", reader["Message"]));
                        youtubeUplaod.Message = String.Format("{0}", reader["Message"]);    
                        Console.WriteLine(String.Format("{0}", reader["LocalVideoPath"]));
                        youtubeUplaod.LocalVideoPath = String.Format("{0}", reader["LocalVideoPath"]);
                        Console.WriteLine(String.Format("{0}", reader["Privacy"]));
                        youtubeUplaod.Privacy = String.Format("{0}", reader["Privacy"]);
                        youtubeUploads.Add(youtubeUplaod);

                    }
                }
            }

            youtubeUploadedData.DataSource  = youtubeUploads.ToList();
            youtubeUploadedData.Columns["Id"].Visible = false;

            cnn.Close();
        }

        private void ModifyRecord_Click(object sender, EventArgs e)
        {
            var id = youtubeUploadedData.SelectedRows[0].Cells["Id"].Value;

            MyYoutubeUpdateUpload addEditForm = new MyYoutubeUpdateUpload(Convert.ToInt32(id));
            addEditForm.MdiParent = this.MdiParent;
            addEditForm.Show();


        }
    }
}
