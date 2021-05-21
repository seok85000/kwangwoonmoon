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
    public partial class KWM : Form
    {
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

        public ulong CurrentMoney = 123456789;


        public KWM()
        {
            InitializeComponent();
        }
        private void KWM_Load(object sender, EventArgs e)
        {
            NextTurn();

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


        // Shop

        // InfoShop 폼의 NewsLabel에 이벤트 설정
        void SetToInfoShop()
        {
            if (infoShop == null) return;

            infoShop.SetEventNews(CurrentEvents[CurrentEvents.Count - 1]);
            infoShop.SetMyMoney(CurrentMoney);
            infoShop.SetBuyCount();
        }

        // InfoShop 에서 정보 구매시 보유 금액 변경
        public void SetMoney(object obj, EventArgs e)
        {
            InfoShop shop = obj as InfoShop;
            this.mymoney_label.Text = string.Format("{0:#,###}", shop.money);
        }

        private void info_shop_button_Click(object sender, EventArgs e)
        {
            if (infoShop == null)
            {
                infoShop = new InfoShop();
                infoShop.Owner = this;
                infoShop.Changed += new EventHandler(SetMoney);
            }

            SetToInfoShop();

            infoShop.Show();
            infoShop.Focus();
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
            total_amount_textbox.Text = (Convert.ToInt32(total_amount_textbox.Text) + 1).ToString();
            // 수량에 증가에 따른 총액 업데이트 필요
        }

        private void minus_button_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(total_amount_textbox.Text) <= 0) total_amount_textbox.Text = "0";
            else total_amount_textbox.Text = (Convert.ToInt32(total_amount_textbox.Text) - 1).ToString();
            // 수량에 감소에 따른 총액 업데이트 필요
        }
    }
}
