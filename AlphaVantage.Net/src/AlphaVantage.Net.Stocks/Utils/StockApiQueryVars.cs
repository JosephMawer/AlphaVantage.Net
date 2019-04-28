namespace AlphaVantage.Net.Stocks.Utils
{
    internal static class StockApiQueryVars
    {
        public const string Symbol = "symbol";
        
        public const string BatchSymbols = "symbols";
        
        public const string IntradayInterval = "interval";

        public const string OutputSize = "outputsize";

        // adding indicator constants here for now


        /// <summary>
        /// Time interval between two consecutive data points in the time series. 
        /// The following values are supported: 1min, 5min, 15min, 30min, 60min, daily, weekly, monthly
        /// </summary>
        public const string IndicatorInterval = "interval";

        /// <summary>
        /// Number of data points used to calculate each moving average value. 
        /// Positive integers are accepted (e.g., time_period=60, time_period=200)
        /// </summary>
        public const string TimePeriod = "time_period";

        /// <summary>
        /// The desired price type in the time series. Four types are supported: close, open, high, low
        /// </summary>
        public const string SeriesType = "series_type";
    }
}