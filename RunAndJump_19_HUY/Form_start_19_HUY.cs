using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunAndJump_19_HUY
{
    public partial class Form_start_19_HUY : Form
    {
        public Form_start_19_HUY()
        {
            InitializeComponent();
            picBtnPlay_19_HUY.Parent = picBr_19_HUY;
            picBtnExit_19_HUY.Parent = picBr_19_HUY;

            picpPlayer_19_HUY.Parent = picBr_19_HUY;

            picRock2_19_HUY.Parent = picBr_19_HUY;
            picIntro_19_HUY.Parent = picBr_19_HUY;
            label1.Parent = picIntro_19_HUY;
            
            
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("Font\\Pixel Game.otf");
            label1.Font = new Font(pfc.Families[0], 40, FontStyle.Bold);
            label1.Location = new Point(30, 70);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Form_start_19_HUY_Load(object sender, EventArgs e)
        {

        }

        private void picIntro_19_HUY_Click(object sender, EventArgs e)
        {

        }

        private void picBtnExit_19_HUY_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn có chắc muốn thoát ","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
                Close();
        }

        private void picBtnPlay_19_HUY_Click(object sender, EventArgs e)
        {
            // Tạo một instance của form mới
            FormMain_19_HUY formNew = new FormMain_19_HUY();

            // Hiển thị form mới dưới dạng hộp thoại modal
            
            DialogResult result = formNew.ShowDialog();

            // Kiểm tra kết quả trả về khi form mới đóng lại
            if (result == DialogResult.Cancel)
            {
                this.Close();
            }
            else
            {
                
                formNew.Show();

            }
            this.Hide();
        }
    }
}
