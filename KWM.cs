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
            return new Event();
        }

        /*
         * 이벤트 업데이트
         * 이벤트를 업데이트 하고, 업데이트 된 이벤트를 바탕으로 Stock을 갱신함
         */
        void UpdateEvent()
        {
            List<Event> newEvents = new List<Event>();

            foreach (Event e in CurrentEvents)
            {
                foreach (Event ie in e.influenceEvent)
                {
                    newEvents.Add(ie);
                }
            }

            Random random = new Random();
            int targetEventSize = DefaultEventSize + random.Next(0, DefaultRandomEventSize + 1);

            // 이벤트 랜덤 생성
            if (newEvents.Count < targetEventSize)
            {
                for (int i = newEvents.Count; i <= targetEventSize; ++i)
                {
                    newEvents.Add(GetRandomEvent());
                }
            }

            events.Add(newEvents);

            // Stock 등락률 업데이트
            foreach (Event e in newEvents)
            {
                e.StockUpdate();
            }

            UpdateStock();
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

    }
}
