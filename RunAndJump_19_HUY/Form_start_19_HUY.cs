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
            picBtnAbout_19_HUY.Parent = picBr_19_HUY;
            picpPlayer_19_HUY.Parent = picBr_19_HUY;

            picRock2_19_HUY.Parent = picBr_19_HUY;
            picIntrol_19_HUY.Parent = picBr_19_HUY;
            lblTilte_19_HUY.Parent = picIntrol_19_HUY;
            
            
            PrivateFontCollection pfc_19_HUY = new PrivateFontCollection();
            pfc_19_HUY.AddFontFile("Font\\Pixel Game.otf");
            lblTilte_19_HUY.Font = new Font(pfc_19_HUY.Families[0], 40, FontStyle.Bold);
            lblTilte_19_HUY.Location = new Point(30, 70);
        }


        private void picBtnExit_19_HUY_Click(object sender, EventArgs e)
        {
            DialogResult res_19_HUY = MessageBox.Show("Bạn có chắc muốn thoát ","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (res_19_HUY == DialogResult.Yes)
                Close();
        }

        private void picBtnPlay_19_HUY_Click(object sender, EventArgs e)
        {
            // Tạo một instance của form mới
            FormMain_19_HUY formNew = new FormMain_19_HUY();
            this.Hide();
            formNew.ShowDialog();
            this.Close();
        }

        private void picBtnAbout_19_HUY_Click(object sender, EventArgs e)
        {
            // Tạo một instance của form mới
            instruct_19_HUY formNew = new instruct_19_HUY();
            formNew.ShowDialog();
        }

        private void lblTilte_19_HUY_Click(object sender, EventArgs e)
        {

        }
    }
}
