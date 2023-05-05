
using PublicRelations.SocialMedia.Youtube;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PublicRelations
{
    public partial class PRMDI : Form
    {
        public PRMDI()
        {
            InitializeComponent();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
           // Search.Searching();
            
        }

        private void uplaodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var OpenForm = Application.OpenForms.Cast<Form>();
            var isOpen = OpenForm.Any(q => q.Name == "MyYoutubeUploadVideo");

            if (!isOpen)
            {
                MyYoutubeUploadVideo youtubeUplaod = new MyYoutubeUploadVideo();
                youtubeUplaod.MdiParent = this;
                youtubeUplaod.Show();
            }

        }
    }
}
