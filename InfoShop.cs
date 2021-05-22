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
        // 정보 가격
        public const int MIDDLE = 5000;
        public const int ADVANCE = 12000;

        // 현재 보유 금액
        public ulong money = 0;

        // 정보 구매 가능 횟수
        public int Count { get; private set; }

        public event EventHandler Changed;

        public InfoShop()
        {
            InitializeComponent();
        }

        private void InfoShop_Load(object sender, EventArgs e)
        {
            this.AdvanceInfo_button.Text = string.Format("{0:#,###}", ADVANCE);
            this.MiddleInfo_button.Text = string.Format("{0:#,###}", MIDDLE);
        }

        public void SetEventNews(Event e)
        {
            this.News_label.Text = e.EventTitle;
        }

        public void SetMyMoney(ulong money)
        {
            this.money = money;
            this.Mymoney_label.Text = string.Format("{0:#,###}", money);
        }

        public void SetBuyCount()
        {
            this.Count = 2;
        }


        // Button Events

        private void OK_button_Click(object sender, EventArgs e)
        {
            Hide();
        }
        
        private void MiddleInfo_button_Click(object sender, EventArgs e)
        {
            if (this.Count > 0)
            {
                if (money >= MIDDLE)
                {
                    money -= MIDDLE;

                    //  정보 제공해주는 창 구현 필요
                    MessageBox.Show("테스트 중급 정보");

                    SetMyMoney(money);
                    if (Changed != null)
                    {
                        Changed(this, new EventArgs());
                    }
                    this.Count--;
                }
                else MessageBox.Show("보유 금액이 부족합니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show("구매 횟수를 모두 사용했습니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void AdvanceInfo_button_Click(object sender, EventArgs e)
        {
            if (this.Count > 0)
            {
                if (money >= ADVANCE)
                {
                    money -= ADVANCE;

                    // 정보 제공해주는 창 구현 필요
                    MessageBox.Show("테스트 고급 정보");

                    SetMyMoney(money);
                    if (Changed != null)
                    {
                        Changed(this, new EventArgs());
                    }
                }
                else MessageBox.Show("보유 금액이 부족합니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show("구매 횟수를 모두 사용했습니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void InfoShop_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Hide();
                e.Cancel = true;
            }
        }
    }
}
