using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kwangwoonmoon
{
    class Stock
    {
        // 종목 이름
        public string StockName { get; private set; }

        // 주가
        public float StockPrice { get; private set; }

        // 지난 턴 주가
        float LastStockPrice { get; set; }

        // 등락률
        public float GetStockRange()
        {
            return StockPrice / LastStockPrice;
        }
    }
}
