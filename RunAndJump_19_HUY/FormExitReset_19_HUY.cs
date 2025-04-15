using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunAndJump_19_HUY
{
    public partial class FormExitReset_19_HUY : Form
    {
        public FormExitReset_19_HUY()
        {
            InitializeComponent();
        }

        private void picBtnReset_19_Huy_Click(object sender, EventArgs e)
        {
            FormMain_19_HUY formNew_19_Huy = new FormMain_19_HUY();
            this.Hide();
            formNew_19_Huy.ShowDialog();
            this.Close();
        }

        private void picBtnClose_19_Huy_Click(object sender, EventArgs e)
        {
            DialogResult res_19_Huy = MessageBox.Show("Bạn có chắc muốn thoát ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res_19_Huy == DialogResult.Yes)
                Close();
        }
    }
}
