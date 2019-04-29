using AlphaVantage.Net.Core;
using AlphaVantage.Net.Stocks.Indicators;
using AlphaVantage.Net.Stocks.Parsing;
using AlphaVantage.Net.Stocks.TimeSeries;
using AlphaVantage.Net.Stocks.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaVantage.Net.Stocks
{
    /// <summary>
    /// Technical indicator values are updated realtime: the latest data point is derived from the current trading day of a 
    /// given equity or currency exchange pair.
    /// </summary>
    public class AlphaVantageIndicatorClient
    {
        private readonly string _apiKey;
        private readonly AlphaVantageCoreClient _coreClient;
        private readonly StockDataParser _parser;
        private readonly IndicatorParser _parserIndicator;
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="apiKey"></param>
        public AlphaVantageIndicatorClient(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentNullException(nameof(apiKey));

            _apiKey = apiKey;
            _coreClient = new AlphaVantageCoreClient();
            _parser = new StockDataParser();
            _parserIndicator = new IndicatorParser();
        }


        /// <summary>
        /// This API call returns the simple moving average (SMA) values. 
        /// See also: https://www.investopedia.com/articles/technical/052201.asp and 
        /// mathematical reference.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="size"></param>
        /// <param name="timePeriod"></param>
        /// <param name="seriesType"></param>
        /// <returns></returns>
        public async Task<StockIndicator> GetSMAAsync(
            string symbol,
            IndicatorSize size,
            int timePeriod,
            IndicatorSeriesType seriesType)
        {
            if (timePeriod < 1) throw new ArgumentException("Time period must be a positive value");

            var query = new Dictionary<string, string>()
            {
                {StockApiQueryVars.Symbol, symbol},
                {StockApiQueryVars.IndicatorInterval, size.ConvertToString()},
                {StockApiQueryVars.TimePeriod, timePeriod.ToString()},
                {StockApiQueryVars.SeriesType, seriesType.ToString()}
            };

            var function = ApiFunction.SMA;

            return await RequestTimeSeriesAsync(function, query);
        }

        private async Task<StockIndicator> RequestTimeSeriesAsync(
            ApiFunction function,
            Dictionary<string, string> query)
        {
            var jObject = await _coreClient.RequestApiAsync(_apiKey, function, query);
            var indicator = _parserIndicator.ParseIndicator(jObject);
            return indicator;
            //var timeSeries = _parser.ParseTimeSeries(jObject);
            //return timeSeries;
        }
    }
}
