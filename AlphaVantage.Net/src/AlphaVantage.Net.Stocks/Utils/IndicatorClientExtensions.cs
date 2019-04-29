using AlphaVantage.Net.Stocks.Indicators;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Net.Stocks.Utils
{
    public static class IndicatorClientExtensions
    {
        public static string ConvertToString(this IndicatorSize sizeEnum)
        {
            switch (sizeEnum)
            {
                case IndicatorSize.OneMin:
                    return "1min";
                case IndicatorSize.FiveMin:
                    return "5min";
                case IndicatorSize.FifteenMin:
                    return "15min";
                case IndicatorSize.ThirtyMin:
                    return "30min";
                case IndicatorSize.SixtyMin:
                    return "60min";
                case IndicatorSize.Daily:
                    return "daily";
                case IndicatorSize.Weekly:
                    return "weekly";
                case IndicatorSize.Monthly:
                    return "monthly";
                default:
                    throw new NotSupportedException(sizeEnum.ToString());
            }
        }
    }
}
