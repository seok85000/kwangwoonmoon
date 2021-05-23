﻿using System;
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
        TotalPrice,
        ProfitRatio,
    }

    public class TransactionInfo
    {
        public Stock targetStock;

        public ulong AverageBuyingPrice { get; private set; }
        public int StockQuantity { get; private set; }

        public TransactionInfo(Stock targetStock, ulong stockPrice, int stockQuantity)
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

        public ulong CurrentStockPrice
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
                return (AverageBuyingPrice / targetStock.StockPrice) * 100f;
            }
        }

        public ulong TotalPrice
        {
            get
            {
                return AverageBuyingPrice * (ulong)StockQuantity;
            }
        }

        public void AddTransaction(ulong price, int quantity)
        {
            AverageBuyingPrice = (TotalPrice + price * (ulong)quantity) / (ulong)(StockQuantity + quantity);
            StockQuantity += quantity;
        }

        public void DecreaseStockQuantity(int quantity)
        {
            StockQuantity -= quantity;
        }

    }
}
