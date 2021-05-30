using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace kwangwoonmoon
{
    public partial class KWM : Form
    {
        public static KWM Instance = null;
        public static Random random = new Random();

        public Action MoneyChanged;

        public int Turn { get; private set; } = 0;
        // 게임이 종료되는 마지막 턴
        public const int LASTTURN = 50;

        List<Stock> stocks = new List<Stock>();
        List<TransactionInfo> transactionList = new List<TransactionInfo>();

        public static int DefaultEventSize = 3;
        public static int DefaultRandomEventSize = 2;
        List<Event> ReferenceEvents = new List<Event>();
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

        public long CurrentMoney { get; private set; } = 123456789;


        public KWM()
        {
            InitializeComponent();

            if (Instance == null)
            {
                Instance = this;
            }

            MoneyChanged += UpdateMoneyText;
        }

        void InitStockList()
        {
            // For Test
            Stock stock = new Stock("삼성전자", 10000);
            Stock stock2 = new Stock("SK하이닉스", 122500);
            stocks.Add(stock);
            stocks.Add(stock2);
            stocks.Add(new Stock("주식1", 78924));
            stocks.Add(new Stock("주식2", 22036));
            stocks.Add(new Stock("주식3", 4550));
            stocks.Add(new Stock("주식4", 598));
            transactionList.Add(new TransactionInfo(stock, 19000, 15));
            // --------------------
        }

        void InitEventList()
        {
            XmlDocument eventXml = new XmlDocument();
            eventXml.Load(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("kwangwoonmoon.Event_List.xml"));
            XmlNode eventNodes = eventXml.SelectSingleNode("eventlist");
            foreach (XmlNode eventNode in eventNodes.SelectNodes("event"))
            {
                ReferenceEvents.Add(GetEventFromXml(eventNode));
            }
        }

        Event GetEventFromXml(XmlNode eventNode)
        {
            Event ev = new Event(eventNode.SelectSingleNode("title").InnerText);
            ev.InfluenceStockSize = int.Parse(eventNode.Attributes["size"].Value);

            foreach (string typeStr in eventNode.Attributes["type"].Value.Split(','))
            {
                ev.IsPositiveEvent.Add(typeStr == "up");
            }

            XmlNode affectNode = eventNode.SelectSingleNode("affectevent");
            if (affectNode != null)
            {
                foreach (XmlNode afNode in affectNode.SelectNodes("event"))
                {
                    ev.AddInfluenceEvent(GetEventFromXml(afNode));
                }
            }

            return ev;
        }

        void InitStockListView()
        {
            stock_listview.View = View.Details;

            stock_listview.Columns.Add(StockColumType.StockName.ToString(), "종목명", 200, HorizontalAlignment.Right, 0);
            stock_listview.Columns.Add(StockColumType.StockPrice.ToString(), "현재가", 100, HorizontalAlignment.Right, 0);
            stock_listview.Columns.Add(StockColumType.StockRatio.ToString(), "등락률", 100, HorizontalAlignment.Right, 0);
        }

        void InitTransactionListView()
        {
            mystock_listview.View = View.Details;

            mystock_listview.Columns.Add(TransactionListColumnType.StockName.ToString(), "종목명", 200, HorizontalAlignment.Right, 0);
            mystock_listview.Columns.Add(TransactionListColumnType.AverageBuyingPrice.ToString(), "매수평균가", 100, HorizontalAlignment.Right, 0);
            mystock_listview.Columns.Add(TransactionListColumnType.StockQuantity.ToString(), "보유 수량", 100, HorizontalAlignment.Right, 0);
            mystock_listview.Columns.Add(TransactionListColumnType.EvaluationAmount.ToString(), "평가금액", 100, HorizontalAlignment.Right, 0);
            mystock_listview.Columns.Add(TransactionListColumnType.ValuationProfitNLoss.ToString(), "평가손익", 100, HorizontalAlignment.Right, 0);
            mystock_listview.Columns.Add(TransactionListColumnType.ProfitRatio.ToString(), "수익률", 100, HorizontalAlignment.Right, 0);
        }


        private void KWM_Load(object sender, EventArgs e)
        {
            InitStockList();
            InitEventList();
            InitStockListView();
            InitTransactionListView();

            SetStockListView();

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

            SetEventToEventNInfo();
            SetTransactionListView();

            // 업데이트 된 Turn 을 label 에 적용
            if (Turn < 10) gameturn_label.Text = "0" + Turn.ToString();
            else if (Turn > 50) gameturn_label.Text = LASTTURN.ToString();
            else gameturn_label.Text = Turn.ToString();

            // Turn 이 변화함에 따라 Child Form 에도 영향
            infoShop?.SetBuyCount();
        }



        // Event

        Event GetRandomEvent()
        {
            Event ev = new Event(ReferenceEvents[random.Next(0, ReferenceEvents.Count)]);
            for (int i = 0; i < ev.InfluenceStockSize;)
            {
                Stock stock = GetRandomStock();
                if (!ev.influenceStock.Contains(stock))
                {
                    ev.AddInfluenceStock(stock);
                    i++;
                }
            }
            ev.UpdateInfluenceEvent();

            return ev;
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
                    e.StockUpdate(); // Stock 등락률 갱신

                    foreach (Event ie in e.influenceEvent)
                    {
                        newEvents.Add(ie);
                    }
                }
            }

            int targetEventSize = DefaultEventSize + random.Next(0, DefaultRandomEventSize + 1);

            // 이벤트 랜덤 생성
            for (int i = newEvents.Count; i < targetEventSize; ++i)
            {
                newEvents.Add(GetRandomEvent());
            }

            events.Add(newEvents);

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

        public List<Stock> GetStocks()
        {
            return stocks;
        }

        public Stock GetRandomStock()
        {
            return stocks[random.Next(0, stocks.Count)];
        }



        //stock_listview 리스트 뷰 컬럼 정의
        void SetStockListView()
        {
            stock_listview.Items.Clear();
            foreach (Stock s in stocks)
            {
                ListViewItem item = stock_listview.Items.Add(new ListViewItem());
                item.Name = StockColumType.StockName.ToString();
                item.Text = s.StockName;
                
                var price = item.SubItems.Add(new ListViewItem.ListViewSubItem());
                price.Name = StockColumType.StockPrice.ToString();
                price.Text = s.StockPrice.ToString("N0");

                var ratio = item.SubItems.Add(new ListViewItem.ListViewSubItem());
                ratio.Name = StockColumType.StockRatio.ToString();
                ratio.Text = s.StockRatio.ToString("N2") + "%";

                item.Tag = s;
                s.ReferenceStock = item;
            }
        }

        void SetTransactionListView()
        {
            mystock_listview.Items.Clear();
            foreach (TransactionInfo info in transactionList)
            {
                ListViewItem item = mystock_listview.Items.Add(new ListViewItem());
                item.Name = TransactionListColumnType.StockName.ToString();
                item.Text = info.StockName;

                var avgPrice = item.SubItems.Add(new ListViewItem.ListViewSubItem());
                avgPrice.Name = TransactionListColumnType.AverageBuyingPrice.ToString();
                avgPrice.Text = info.AverageBuyingPrice.ToString("N0");

                var quantity = item.SubItems.Add(new ListViewItem.ListViewSubItem());
                quantity.Name = TransactionListColumnType.StockQuantity.ToString();
                quantity.Text = info.StockQuantity.ToString("N0");

                var evaluationAmount = item.SubItems.Add(new ListViewItem.ListViewSubItem());
                evaluationAmount.Name = TransactionListColumnType.EvaluationAmount.ToString();
                evaluationAmount.Text = info.EvaluationAmount.ToString("N0");

                var valuationProfitNLoss = item.SubItems.Add(new ListViewItem.ListViewSubItem());
                valuationProfitNLoss.Name = TransactionListColumnType.ValuationProfitNLoss.ToString();
                valuationProfitNLoss.Text = info.ValuationProfitNLoss.ToString("N0");

                var ratio = item.SubItems.Add(new ListViewItem.ListViewSubItem());
                ratio.Name = TransactionListColumnType.ProfitRatio.ToString();
                ratio.Text = info.ProfitRatio.ToString("N2") + "%";

                item.Tag = info;
            }
        }

        //inputbox 비우기
        private void ClearInputControl()
        {
            lb_Selected.Text = string.Empty;
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
            TransactionInfo info = (TransactionInfo)lvi.Tag;
            lb_Selected.Text = info.StockName;
            price_textbox.Text = string.Format("{0:#,###}", info.CurrentStockPrice);
            total_amount_textbox.Text = info.StockQuantity.ToString();
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

        public bool UseMoney(long money)
        {
            if (CurrentMoney >= money)
            {
                CurrentMoney -= money;

                MoneyChanged.Invoke();

                return true;
            }
            return false;
        }

        public void AddMoney(long money)
        {
            CurrentMoney += money;

            MoneyChanged.Invoke();
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


        private void number_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        //Sell Button
        private void sell_button_Click(object sender, EventArgs e)
        {
            bool selected = mystock_listview.SelectedItems.Count > 0;
            if (selected == false)
            {
                MessageBox.Show("종목을 선택해 주세요", "종목 선택 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            else
            {
                ListViewItem lvi = mystock_listview.SelectedItems[0];
                TransactionInfo info = (TransactionInfo)lvi.Tag;

                int stockWantQuantity = Convert.ToInt32(total_amount_textbox.Text);

                long totalPrice = (info.CurrentStockPrice * stockWantQuantity);

                if (stockWantQuantity > info.StockQuantity)
                {
                    MessageBox.Show("판매 가능 수량을 초과하였습니다.", "판매 수량 초과", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    //For DoubleCheck MessageBox
                    DialogResult DoubleCheck = MessageBox.Show("종목명:"+info.StockName + "\n\n현재가:"+ String.Format("{0:#,0}", info.CurrentStockPrice) +"원\n\n수량:"+stockWantQuantity+ "\n\n총액:" + String.Format("{0:#,0}", totalPrice) +"원\n\n매도주문 하시겠습니까?",
                     "매도 주문 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    switch (DoubleCheck)
                    {
                        case DialogResult.Yes:
                            long totalBenefit = info.AverageBuyingPrice * stockWantQuantity;
                            AddMoney(totalBenefit);

                            info.DecreaseStockQuantity(stockWantQuantity);
                            lvi.SubItems[TransactionListColumnType.StockQuantity.ToString()].Text = info.StockQuantity.ToString();
                            if (info.StockQuantity <= 0)
                            {
                                ClearInputControl();
                                mystock_listview.Items.Remove(lvi);
                                transactionList.Remove(info);
                            }

                            break;

                        case DialogResult.No:
                            break;

                    }
                   
                }
            }
        }

        // Buy Button
        private void buy_button_Click(object sender, EventArgs e)
        {



            bool selected = stock_listview.SelectedItems.Count > 0;
            if (selected == false)
            {
                MessageBox.Show("종목을 선택해 주세요.", "종목 선택 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            else
            {

                // stock_listView 처리
                ListViewItem lviyet = stock_listview.SelectedItems[0];
                Stock stock = (Stock)lviyet.Tag;
                // mystock_listView 처리
                ListViewItem lvi = mystock_listview.Items[0];
                TransactionInfo info = (TransactionInfo)lvi.Tag;



                int stockWantQuantity = Convert.ToInt32(total_amount_textbox.Text);
                long totalPrice = Convert.ToUInt32(price_textbox.Text.Replace(",", "")) * stockWantQuantity;



                //For DoubleCheck MessageBox
                DialogResult DoubleCheckBuy = MessageBox.Show("종목명:" + stock.StockName + "\n\n현재가:" + String.Format( "{0:#,0}",stock.StockPrice) + "원\n\n수량:" + stockWantQuantity + "\n\n총액:" + String.Format("{0:#,0}", totalPrice) + "원\n\n매수주문 하시겠습니까?",
                 "매수 주문 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                switch (DoubleCheckBuy)
                {
                    case DialogResult.Yes:

                        if (totalPrice > CurrentMoney)
                        {
                            MessageBox.Show("현재잔고가 부족합니다.", "잔고 부족", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {


                            UseMoney(totalPrice);


                            if (info.StockName == stock.StockName)
                            {//  마이 스탁 리스트뷰 Quantitiy만 증가

                                info.AddTransaction(info.CurrentStockPrice, stockWantQuantity);
                                SetTransactionListView();
                                ClearInputControl();


                            }
                            else
                            {
                                TransactionInfo transaction = new TransactionInfo(stock, stock.StockPrice, stockWantQuantity);
                                transactionList.Add(transaction);
                                SetTransactionListView();
                                ClearInputControl();
                            }
                        }

                         break;

                    case DialogResult.No:

                        break;

                      
                }
            }
        }



        private void stock_listview_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool selected = stock_listview.SelectedItems.Count > 0;
            total_amount_textbox.Enabled = price_textbox.Enabled = selected;
            if (selected == false)
            {
                ClearInputControl();
                return;
            }
            ListViewItem lvi = stock_listview.SelectedItems[0];
            Stock info = (Stock)lvi.Tag;
            lb_Selected.Text = info.StockName;
            total_amount_textbox.Text = "0";
            price_textbox.Text = string.Format("{0:#,###}", info.StockPrice);
        }

        private void plus_button_Click(object sender, EventArgs e)
        {
            if (total_amount_textbox.Text.Length == 0) total_amount_textbox.Text = "0";

            total_amount_textbox.Text = (Convert.ToInt32(total_amount_textbox.Text) + 1).ToString();
            // 수량에 증가에 따른 총액 업데이트 필요
        }

        private void minus_button_Click(object sender, EventArgs e)
        {
            if (total_amount_textbox.Text.Length == 0) total_amount_textbox.Text = "0";

            if (Convert.ToInt32(total_amount_textbox.Text) <= 0) total_amount_textbox.Text = "0";
            else total_amount_textbox.Text = (Convert.ToInt32(total_amount_textbox.Text) - 1).ToString();
            // 수량에 감소에 따른 총액 업데이트 필요
        }

        private void nextTurn_button_Click(object sender, EventArgs e)
        {
            NextTurn();
        }
    }
}
