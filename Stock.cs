using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kwangwoonmoon
{
    class Stock
    {
        // 해당 Stock의 데이터를 담고있는 ListViewItem
        public System.Windows.Forms.ListViewItem ReferenceItem;

        // 종목 이름
        public string StockName { get; private set; }

        // 주가
        public float StockPrice { get; private set; }

        // 지난 턴 주가
        float LastStockPrice { get; set; }

        // 등락률
        public float StockRatio { get; private set; }
        float NextStockRatio { get; set; }



        public void AddStockRatio(float newRatio)
        {
            NextStockRatio += newRatio;
        }

        // StockRatio를 NextStockRatio로 갱신
        public void UpdateStockRatio()
        {
            LastStockPrice = StockPrice;
            StockPrice *= NextStockRatio;
            StockRatio = NextStockRatio;
            NextStockRatio = 0;

            // ListViewItem 데이터 업데이트

        }
    }
}
