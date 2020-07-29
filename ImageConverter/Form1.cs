using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace test
{
    public partial class Form1 : Form
    {
        private readonly ImageFormat[] ImageFmt =
        {
            ImageFormat.Jpeg,
            ImageFormat.Png,
            ImageFormat.Bmp,
            ImageFormat.Gif
        };

        private string BasePath = "";

        public Form1()
        {
            InitializeComponent();
            this.comboBox1.Items.AddRange(this.ImageFmt);
            this.comboBox1.SelectedIndex = 0;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            bool isImage = false;
            List<string> checkFmt = new List<string>();
            string[] path = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach(var i in this.ImageFmt)
            {
                checkFmt.Add(i.ToString());
            }
            checkFmt.Add("jpg");

            foreach( var fmt in checkFmt)
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
                this.textBox1.Text = Path.GetExtension(path[0]);
                this.BasePath = path[0];
                this.button1.Enabled = true;
            }
            else 
            {
                MessageBox.Show("この形式は現在非対応です。");
            }

        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!this.checkBox1.Checked)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = Path.GetFileNameWithoutExtension(this.BasePath);
                sfd.DefaultExt = this.comboBox1.SelectedItem.ToString().ToLower();
                sfd.InitialDirectory = Path.GetDirectoryName(this.BasePath);
                sfd.Filter = "JEPGファイル|*.jpg;*.jpeg|PNGファイル|*.png|GIFファイル|*.gif|BMPファイル|*.bmp";
                sfd.Title = "名前を付けて保存";

                if(DialogResult.OK == sfd.ShowDialog())
                {
                    this.pictureBox1.Image.Save(sfd.FileName, (ImageFormat)this.comboBox1.SelectedItem);
                    sfd.Dispose();
                }
            }
            else 
            {
                string path = Path.Combine(Path.GetDirectoryName(this.BasePath), Path.GetFileNameWithoutExtension(this.BasePath)) + "." + this.comboBox1.SelectedItem.ToString().ToLower();
                this.pictureBox1.Image.Save(path, (ImageFormat)this.comboBox1.SelectedItem);
            }
        }
    }
}
