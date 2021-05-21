using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kwangwoonmoon
{
    public partial class InfoShop : Form
    {
        public InfoShop()
        {
            InitializeComponent();
        }

        private void OK_button_Click(object sender, EventArgs e)
        {
            Hide();
        }
        
        private void MiddleInfo_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("보유 금액이 부족합니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void AdvanceInfo_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("보유 금액이 부족합니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }
    }
}
