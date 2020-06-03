using System;
using System.IO;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        private string[] ImageFmt =
        {
            System.Drawing.Imaging.ImageFormat.Png.ToString(),
            System.Drawing.Imaging.ImageFormat.Jpeg.ToString(),
            System.Drawing.Imaging.ImageFormat.Bmp.ToString(),
            "Jpg"
        };

        public Form1()
        {
            InitializeComponent();
            this.comboBox1.Items.AddRange(this.ImageFmt);
            this.comboBox1.SelectedIndex = 0;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] path = (string[])e.Data.GetData(DataFormats.FileDrop);
            bool isImage = false;

            foreach( var fmt in ImageFmt )
            {
                if( path[0].Contains(fmt.ToLower()) )
                {
                    isImage = true;
                    break;
                }
            }

            if (isImage) 
            {   
                this.pictureBox1.Image = System.Drawing.Image.FromFile(path[0]);
                this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                this.label3.Text = Path.GetExtension(path[0]);
            }
            else 
            {
                MessageBox.Show("\aこの形式は現在非対応です。");
            }

        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
