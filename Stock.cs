using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kwangwoonmoon
{

    public enum StockColumType
    {
        StockName,
        StockPrice,
        StockRatio,
    }

    public class Stock
    {
        // 해당 Stock의 데이터를 담고있는 ListViewItem
        public System.Windows.Forms.ListViewItem ReferenceStock = null;
        //초기화

        // 종목 이름
        public string StockName { get; private set; }

        // 주가
        public long StockPrice { get; private set; }

        // 지난 턴 주가
        float LastStockPrice { get; set; }

        // 등락률
        public float StockRatio { get; private set; }

        float NextStockRatio { get; set; }

        public Stock(string name, long price)
        {
            StockName = name;
            StockPrice = price;
        }



        public void AddStockRatio(float newRatio)
        {
            NextStockRatio += newRatio;
        }

        // StockRatio를 NextStockRatio로 갱신
        public void UpdateStockRatio()
        {
            LastStockPrice = StockPrice;
            StockPrice = (long)(StockPrice * (1f + NextStockRatio * 0.01f));
            StockRatio = NextStockRatio;
            NextStockRatio = 0;

            // ListViewItem 데이터 업데이트
            ReferenceStock.SubItems[StockColumType.StockPrice.ToString()].Text = StockPrice.ToString("N0");
            ReferenceStock.SubItems[StockColumType.StockRatio.ToString()].Text = StockRatio.ToString("N2") + "%";
        }
    }
}
