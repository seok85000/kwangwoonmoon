using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kwangwoonmoon
{
    public class Event
    {
        // 제목
        public string EventTitle;

        public int InfluenceStockSize = 0;

        // 영향을 주는 종목
        public List<Stock> influenceStock = new List<Stock>();

        // 영향을 주는 이벤트
        public List<Event> influenceEvent = new List<Event>();

        public List<bool> IsPositiveEvent = new List<bool>();

        // 랜덤 요소
        public const float InfluenceRandomPower = 1.5f;


        public Event(string eventTitle)
        {
            EventTitle = eventTitle;
        }

        public Event(Event ev)
        {
            EventTitle = ev.EventTitle;
            InfluenceStockSize = ev.InfluenceStockSize;

            IsPositiveEvent = ev.IsPositiveEvent.ToList();

            foreach (var infEvent in ev.influenceEvent)
            {
                influenceEvent.Add(new Event(infEvent));
            }
        }

        public string Title
        {
            get
            {
                if (InfluenceStockSize == 0) return EventTitle;
                else
                {
                    string[] stockTitle = new string[InfluenceStockSize];
                    for (int i = 0; i < InfluenceStockSize; i++)
                    {
                        stockTitle[i] = influenceStock[i].StockName;
                    }
                    return string.Format(EventTitle, stockTitle);
                }
            }
        }

        public bool AddInfluenceStock(Stock stock)
        {
            if (influenceStock.Contains(stock)) return false;

            influenceStock.Add(stock);
            if (influenceStock.Count > InfluenceStockSize) InfluenceStockSize = influenceStock.Count;

            foreach (var infEvent in influenceEvent)
            {
                if (InfluenceStockSize <= infEvent.InfluenceStockSize)
                    infEvent.AddInfluenceStock(stock);
            }

            return true;
        }

        public void AddInfluenceEvent(Event ev)
        {
            influenceEvent.Add(ev);
        }

        public void UpdateInfluenceEvent()
        {
            foreach (var infEvent in influenceEvent)
            {
                for (int i = InfluenceStockSize; i < infEvent.InfluenceStockSize;)
                {
                    if (infEvent.AddInfluenceStock(KWM.Instance.GetRandomStock()))
                    {
                        i++;
                    }
                }
            }
        }


        float GetInfluencePower(bool isPositive)
        {
            return (isPositive) ? ((float)KWM.random.NextDouble()) * 100f : -(float)KWM.random.NextDouble() * 30f;
        }


        // 영향을 주는 종목의 등락률을 갱신한다.
        public void StockUpdate()
        {
            Random random = new Random();
            if (InfluenceStockSize == 0)
            {
                foreach (Stock stock in KWM.Instance.GetStocks())
                {
                    stock.AddStockRatio(GetInfluencePower(IsPositiveEvent[0]) * (float)random.NextDouble() * InfluenceRandomPower);
                }
            }
            else
            {
                for(int i = 0; i < InfluenceStockSize; i++)
                {
                    influenceStock[i].AddStockRatio(GetInfluencePower(IsPositiveEvent[i]) * (float)random.NextDouble() * InfluenceRandomPower);
                }
            }
        }
    }
}
