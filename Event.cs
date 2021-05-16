using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kwangwoonmoon
{
    class Event
    {
        // 제목
        public string EventTitle { get; private set; }

        // 정보
        public string EventDescription { get; private set; }

        // 영향을 주는 종목
        public List<Stock> influenceStock = null;

        // 영향을 주는 이벤트
        public List<Event> influenceEvent = null;

        // 영향을 주는 정도
        public float InfluencePower { get; private set; }
        // 랜덤 요소
        public const float InfluenceRandomPower = 1.5f;


        // 영향을 주는 종목의 등락률을 갱신한다.
        public void StockUpdate()
        {
            Random random = new Random();
            foreach (Stock stock in influenceStock)
            {
                stock.AddStockRatio(InfluencePower * (float)random.NextDouble() * InfluenceRandomPower);
            }
        }
    }
}
