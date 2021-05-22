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
        StockQuantity,
        StockBuyPrice,
        StockRatio,
    }
    public class Stock
    {
        // 해당 Stock의 데이터를 담고있는 ListViewItem
        public System.Windows.Forms.ListViewItem ReferenceStock = null;
        public System.Windows.Forms.ListViewItem ReferenceMyStock = null;
        //초기화

        // 종목 이름
        public string StockName { get; private set; }

        // 주가
        public ulong StockPrice { get; private set; }

        // 지난 턴 주가
        float LastStockPrice { get; set; }

        // 수량
        public int StockQuantity { get; private set; }

        // 구매 가격
        public ulong StockBuyPrice { get; private set; }

        // 등락률
        public float StockRatio { get; private set; }

        //수익률
        public float StockEarningRatio { get; private set; }

        float NextStockRatio { get; set; }

        public Stock(string name, ulong price)
        {
            StockName = name;
            StockPrice = price;
            StockQuantity = 0;
        }



        public void AddStockRatio(float newRatio)
        {
            NextStockRatio += newRatio;
        }

        // StockRatio를 NextStockRatio로 갱신
        public void UpdateStockRatio()
        {
            LastStockPrice = StockPrice;
            StockPrice = (ulong)(StockPrice * NextStockRatio);
            StockRatio = NextStockRatio;
            NextStockRatio = 0;

            // ListViewItem 데이터 업데이트

        }


        public void DecreaseStockQuantity(int quantity)
        {
            StockQuantity -= quantity;
        }
    }
}
