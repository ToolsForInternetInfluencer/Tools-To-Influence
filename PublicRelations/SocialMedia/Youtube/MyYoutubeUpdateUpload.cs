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
using static PublicRelations.SocialMedia.Youtube.Utility.YoutubeCategory;

namespace PublicRelations.SocialMedia.Youtube
{
    public partial class MyYoutubeUpdateUpload : Form
    {
        string connetionString = @"Server=.\SqlExpress01; Database= SocialMedia; Integrated Security=True;";
        SqlConnection cnn;
        string filePath = "";
        public MyYoutubeUpdateUpload()
        {
            InitializeComponent();
        }

        public MyYoutubeUpdateUpload(int id)
        {
            InitializeComponent();
            cbCategory.DataSource = YoutubeCategory.CatgoryList();
            cbCategory.ValueMember = "Id";
            cbCategory.DisplayMember = "Name";
            cbPrivacyStatus.DataSource = YoutubeCategory.PrivacyStatusList();
            cbPrivacyStatus.ValueMember = "Value";
            cbPrivacyStatus.DisplayMember = "Name";
            filePath = "";
            string sqlQuery = "Select * from YoutubeUpload where id = "+ id;
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

                        tbTitle.Text = (String.Format("{0}", reader["Title"]));
                       
                        tbDesc.Text = String.Format("{0}", reader["Description"]);

                        tbTags.Text = String.Format("{0}", reader["Tag"]);

                        
                        cbCategory.SelectedValue = String.Format("{0}", reader["CategoryId"]);

                        lsttag.Items.Add(String.Format("{0}", reader["Tags"]));

                        



                        Console.WriteLine(String.Format("{0}", reader["LocalVideoPath"]));
                        filePath = String.Format("{0}", reader["LocalVideoPath"]);
                        youtubeUplaod.LocalVideoPath = String.Format("{0}", reader["LocalVideoPath"]);
                        Console.WriteLine(String.Format("{0}", reader["Privacy"]));
                        youtubeUplaod.Privacy = String.Format("{0}", reader["Privacy"]);
                        

                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            if (!String.IsNullOrEmpty(filePath))
            {
                dlg.InitialDirectory = filePath;
            }
            else {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    filePath = dlg.FileName;
                }
            }
        }
    }
}
