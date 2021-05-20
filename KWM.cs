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


        public KWM()
        {
            InitializeComponent();
        }
        private void KWM_Load(object sender, EventArgs e)
        {
            NextTurn();
        }


        // 다음 턴으로 넘어감
        void NextTurn()
        {
            // 이벤트 업데이트
            UpdateEvent();

            ++Turn;
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
    }
}
