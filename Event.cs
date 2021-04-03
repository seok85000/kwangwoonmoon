using System;
using System.Collections.Generic;
using System.Text;

namespace kwangwoonmoon
{
    class Event
    {
        // 제목
        public string EventTitle { get; private set; }

        // 정보
        public string EventDescription { get; private set; }

        // 영향을 주는 종목
        List<Stock> influenceStock = null;

        // 영향을 주는 이벤트
        List<Event> influenceEvent = null;

        // 영향을 주는 정도
        public float InfluencePower { get; private set; }
    }
}
