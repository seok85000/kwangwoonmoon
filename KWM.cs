using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace kwangwoonmoon
{
    public partial class KWM : Form
    {
        public static KWM Instance = null;

        public Action MoneyChanged;

        public int Turn { get; private set; } = 0;
        // 게임이 종료되는 마지막 턴
        public const int LASTTURN = 50;

        List<Stock> stocks = new List<Stock>();

        public static int DefaultEventSize = 3;
        public static int DefaultRandomEventSize = 2;
        List<List<Event>> events = new List<List<Event>>();
        List<Event> CurrentEvents
        {
            get
            {
                if (Turn > 0 && Turn <= events.Count)
                    return events[Turn - 1];
                else
                    return null;
            }
        }

        EventNInfo eventNInfo = null;

        InfoShop infoShop = null;

        public ulong CurrentMoney { get; private set; } = 123456789;


        public KWM()
        {
            InitializeComponent();

            if (Instance == null)
            {
                Instance = this;
            }

            MoneyChanged += UpdateMoneyText;
        }
        void my_lisviewUpdate()
        {
            mystock_listview.View = View.Details;

            mystock_listview.Columns.Add(StockColumType.StockName.ToString(), "종목명");
            /*mystock_listview.Columns[mystock_listview.Columns.Count - 1].Width = -1;*/
            mystock_listview.Columns.Add(StockColumType.StockRatio.ToString(), "수익률");
            mystock_listview.Columns.Add(StockColumType.StockQuantity.ToString(), "보유 수량");

            mystock_listview.Columns.Add(StockColumType.StockPrice.ToString(), "현재가");

            mystock_listview.Columns.Add(StockColumType.StockBuyPrice.ToString(), "매입가");
            mystock_listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        private void KWM_Load(object sender, EventArgs e)
        {
            NextTurn();
            my_lisviewUpdate();

            // 라벨 Text 값 초기화
            this.finish_label.Text = "/ " + LASTTURN.ToString();
            this.mymoney_label.Text = String.Format("{0:#,###}", CurrentMoney);
            this.total_amount_textbox.Text = "0";
        }


        // 다음 턴으로 넘어감
        void NextTurn()
        {
            // 이벤트 업데이트
            UpdateEvent();

            ++Turn;

            // 업데이트 된 Turn 을 label 에 적용
            if (Turn < 10) gameturn_label.Text = "0" + Turn.ToString();
            else if (Turn > 50) gameturn_label.Text = LASTTURN.ToString();
            else gameturn_label.Text = Turn.ToString();

            // Turn 이 변화함에 따라 Child Form 에도 영향
            if (infoShop == null) return;
            else infoShop.SetBuyCount();
        }



        // Event

        Event GetRandomEvent()
        {
            //return new Event();
            Event e = new Event();
            e.EventTitle = "테스트 이벤트";
            e.EventDescription = "이벤트 설명";
            return e;
        }

        /*
         * 이벤트 업데이트
         * 이벤트를 업데이트 하고, 업데이트 된 이벤트를 바탕으로 Stock을 갱신함
         */
        void UpdateEvent()
        {
            List<Event> newEvents = new List<Event>();

            if (CurrentEvents != null)
            {
                foreach (Event e in CurrentEvents)
                {
                    foreach (Event ie in e.influenceEvent)
                    {
                        newEvents.Add(ie);
                    }
                }
            }

            Random random = new Random();
            int targetEventSize = DefaultEventSize + random.Next(0, DefaultRandomEventSize + 1);

            // 이벤트 랜덤 생성
            for (int i = newEvents.Count; i < targetEventSize; ++i)
            {
                newEvents.Add(GetRandomEvent());
            }

            events.Add(newEvents);
            SetEventToEventNInfo();

            // Stock 등락률 업데이트
            foreach (Event e in newEvents)
            {
                e.StockUpdate();
            }

            UpdateStock();
        }

        // EventNInfo 폼의 ListView에 이벤트 설정
        void SetEventToEventNInfo()
        {
            if (eventNInfo == null) return;

            eventNInfo.SetEventListView(CurrentEvents);
        }


        // Stock

        // Stock 등락률 갱신
        void UpdateStock()
        {
            foreach (Stock stock in stocks)
            {
                stock.UpdateStockRatio();
          

            }


        }



        //mystock_listview 리스트 뷰 컬럼 정의
        void SetEventListView(List<Stock> stocks)
        {
            if (stocks == null) return;

            mystock_listview.Items.Clear();
            foreach (Stock s in stocks)
            {
                ListViewItem item = mystock_listview.Items.Add(new ListViewItem());
                item.Name = StockColumType.StockName.ToString();
                item.Text = s.StockName;

                var ratio = item.SubItems.Add(new ListViewItem.ListViewSubItem());
                ratio.Name = StockColumType.StockRatio.ToString();
                ratio.Text = s.StockRatio.ToString();


                var quantity = item.SubItems.Add(new ListViewItem.ListViewSubItem());
                quantity.Name = StockColumType.StockQuantity.ToString();
                quantity.Text = s.StockQuantity.ToString();

                var price = item.SubItems.Add(new ListViewItem.ListViewSubItem());
                price.Name = StockColumType.StockPrice.ToString();
                price.Text = s.StockPrice.ToString();

                var nowprice = item.SubItems.Add(new ListViewItem.ListViewSubItem());
                nowprice.Name = StockColumType.StockBuyPrice.ToString();
                nowprice.Text = s.StockBuyPrice.ToString();
            }

            //eventListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        //inputbox 비우기
        private void ClearInputControl()
        {
            total_amount_textbox.Text = string.Empty;
            price_textbox.Text = string.Empty;
        }

        //mystock_listview 선택 함수
        private void mystock_listview_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool selected = mystock_listview.SelectedItems.Count > 0;
            total_amount_textbox.Enabled = price_textbox.Enabled = selected;
            if (selected == false)
            {
                ClearInputControl();
                return;
            }
            ListViewItem lvi = mystock_listview.SelectedItems[0];
            lb_Selected.Text = lvi.SubItems[0].Text;
            price_textbox.Text = string.Format("{0:#,###}", lvi.SubItems[3].Text);
            total_amount_textbox.Text = lvi.SubItems[2].Text;
        }





        // Shop

        // InfoShop 폼의 NewsLabel에 이벤트 설정
        void SetToInfoShop()
        {
            if (infoShop == null) return;

            infoShop.SetEventNews(CurrentEvents[CurrentEvents.Count - 1]);
            infoShop.SetBuyCount();
        }

        void UpdateMoneyText()
        {
            mymoney_label.Text = string.Format("{0:#,###}", CurrentMoney);
        }

        private void info_shop_button_Click(object sender, EventArgs e)
        {
            if (infoShop == null)
            {
                infoShop = new InfoShop();
                infoShop.Owner = this;
            }

            SetToInfoShop();

            infoShop.Show();
            infoShop.Focus();
        }


        // Money

        public bool UseMoney(ulong money)
        {
            if (CurrentMoney >= money)
            {
                CurrentMoney -= money;

                MoneyChanged.Invoke();

                return true;
            }
            return false;
        }


        // Button Events

        private void news_button_Click(object sender, EventArgs e)
        {
            if (eventNInfo == null)
            {
                eventNInfo = new EventNInfo();
                eventNInfo.Owner = this;

                SetEventToEventNInfo();
            }

            eventNInfo.Show();
            eventNInfo.Focus();
        }

        private void plus_button_Click(object sender, EventArgs e)
        {
            UseMoney(30);
            total_amount_textbox.Text = (Convert.ToInt32(total_amount_textbox.Text) + 1).ToString();
            // 수량에 증가에 따른 총액 업데이트 필요
        }

        private void minus_button_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(total_amount_textbox.Text) <= 0) total_amount_textbox.Text = "0";
            else total_amount_textbox.Text = (Convert.ToInt32(total_amount_textbox.Text) - 1).ToString();
            // 수량에 감소에 따른 총액 업데이트 필요
        }

        private void number_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void sell_button_Click(object sender, EventArgs e)
        {
            bool selected = mystock_listview.SelectedItems.Count > 0;
            if (selected == false)
            {
                MessageBox.Show("종목을 선택해 주세요", "선택오류", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }

            else
            {
                ListViewItem lvi = mystock_listview.SelectedItems[0];
                string strRe = mymoney_label.Text.Replace(",", "");

                ulong stockWantQuantity = Convert.ToUInt64(total_amount_textbox.Text);
                ulong StockNowQuantity = Convert.ToUInt64(lvi.SubItems[2].Text, CultureInfo.InvariantCulture);
                if (stockWantQuantity > StockNowQuantity)
                {
                    MessageBox.Show("판매 가능 수량을 초과하였습니다.", "판매 수량 초과", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                else
                {

                    ulong StockNowPrice = Convert.ToUInt64(lvi.SubItems[3].Text, CultureInfo.InvariantCulture);
                    ulong StockBuyPrice = Convert.ToUInt64(lvi.SubItems[4].Text, CultureInfo.InvariantCulture);
                    ulong nowbenefit = stockWantQuantity * StockNowPrice;
                    ulong YourBenefit = stockWantQuantity * StockBuyPrice;
                    ulong NowBalance = Convert.ToUInt64(strRe);
                    ulong TotalBenefit = nowbenefit - YourBenefit;
                    ulong Afterbalance = NowBalance + TotalBenefit;
                    CurrentMoney = Convert.ToUInt64(Afterbalance);
                    mymoney_label.Text = CurrentMoney.ToString();
                    ulong AfterQuantatiy = StockNowQuantity - stockWantQuantity;
                    lvi.SubItems[2].Text = AfterQuantatiy.ToString();
                    ClearInputControl();
                    if (AfterQuantatiy <= 0)
                    {
                        mystock_listview.Items.Remove(lvi);
                    }
                    UpdateMoneyText();
                }

            }
        }
    }
}
