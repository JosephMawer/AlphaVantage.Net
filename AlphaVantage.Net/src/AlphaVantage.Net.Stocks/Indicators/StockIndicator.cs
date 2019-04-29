using System;
using System.Collections.Generic;

namespace AlphaVantage.Net.Stocks.Indicators
{
    public class IndicatorPoint
    {
        public DateTime? Time { get; set; }

        public decimal Value { get; set; }
    }
    /// <summary>
    /// POCO for tehcnical indicators...
    /// </summary>
    public class StockIndicator
    {
        /// <summary>
        /// Meta Data about the technical indicator
        /// </summary>
        public StockIndicatorMetaData MetaData { get; set; }

        /// <summary>
        /// Indicator data
        /// </summary>
        public ICollection<IndicatorPoint> DataPoints { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public StockIndicator()
        {
            MetaData = new StockIndicatorMetaData();
        }
    }
    public class StockIndicatorMetaData
    {
        public string Symbol { get; set; }
        public string Indicator { get; set; }
        public DateTime LastRefreshed { get; set; }
        public string Interval { get; set; }
        public string TimePeriod { get; set; }
        public string SeriesType { get; set; }
        public string TimeZone { get; set; }

    }
}
