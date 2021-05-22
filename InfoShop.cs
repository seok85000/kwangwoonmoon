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

        // 정보 구매 가능 횟수
        public int Count { get; private set; }


        public InfoShop()
        {
            InitializeComponent();

            KWM.Instance.MoneyChanged += UpdateMoneyText;
        }

        private void InfoShop_Load(object sender, EventArgs e)
        {
            UpdateMoneyText();

            this.AdvanceInfo_button.Text = string.Format("{0:#,###}", ADVANCE);
            this.MiddleInfo_button.Text = string.Format("{0:#,###}", MIDDLE);
        }

        public void SetEventNews(Event e)
        {
            this.News_label.Text = e.EventTitle;
        }

        void UpdateMoneyText()
        {
            Mymoney_label.Text = string.Format("{0:#,###}", KWM.Instance.CurrentMoney);
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
                if (KWM.Instance.UseMoney(MIDDLE))
                {
                    //  정보 제공해주는 창 구현 필요
                    MessageBox.Show("테스트 중급 정보");
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
                if (KWM.Instance.UseMoney(ADVANCE))
                {
                    // 정보 제공해주는 창 구현 필요
                    MessageBox.Show("테스트 고급 정보");
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
