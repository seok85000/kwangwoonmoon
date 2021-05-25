using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kwangwoonmoon
{
    public enum TransactionListColumnType
    {
        StockName = 0,
        AverageBuyingPrice,
        StockQuantity,
        EvaluationAmount,
        ValuationProfitNLoss,
        ProfitRatio,
    }

    public class TransactionInfo
    {
        public Stock targetStock;

        public long AverageBuyingPrice { get; private set; }
        public int StockQuantity { get; private set; }

        public TransactionInfo(Stock targetStock, long stockPrice, int stockQuantity)
        {
            this.targetStock = targetStock;
            AverageBuyingPrice = stockPrice;
            StockQuantity = stockQuantity;
        }

        public string StockName
        {
            get
            {
                return targetStock.StockName;
            }
        }

        public long CurrentStockPrice
        {
            get
            {
                return targetStock.StockPrice;
            }
        }

        public float ProfitRatio
        {
            get
            {
                return ((targetStock.StockPrice - AverageBuyingPrice) / (float)AverageBuyingPrice) * 100f;
            }
        }

        public long TotalPrice
        {
            get
            {
                return AverageBuyingPrice * (long)StockQuantity;
            }
        }

        public long EvaluationAmount // 평가금액
        {
            get
            {
                return CurrentStockPrice * StockQuantity;
            }
        }

        public long ValuationProfitNLoss // 평가손익
        {
            get
            {
                return (CurrentStockPrice - AverageBuyingPrice) * StockQuantity;
            }
        }

        public void AddTransaction(long price, int quantity)
        {
            AverageBuyingPrice = (TotalPrice + price * (long)quantity) / (long)(StockQuantity + quantity);
            StockQuantity += quantity;
        }

        public void DecreaseStockQuantity(int quantity)
        {
            StockQuantity -= quantity;
        }

    }
}
