using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AlphaVantage.Net.Stocks.BatchQuotes;
using AlphaVantage.Net.Stocks.Indicators;
using AlphaVantage.Net.Stocks.Parsing.Exceptions;
using AlphaVantage.Net.Stocks.Parsing.JsonTokens;
using AlphaVantage.Net.Stocks.Parsing.JsonTokens.Technicals;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace AlphaVantage.Net.Stocks.Parsing
{
    public class IndicatorParser
    {
        public StockIndicator ParseIndicator(JObject jObject)
        {
            var properties = jObject.Children().Select(ch => (JProperty)ch).ToArray();

            var metadataJson = properties.FirstOrDefault(p => p.Name == TechnicalMetaDataJsonTokens.MetaDataHeader);
            var indicatorJson = properties.FirstOrDefault(p => p.Name.Contains(IndicatorJsonToken.IndicatorHeader));

            if (metadataJson == null || indicatorJson == null)
                throw new StocksParsingException("Unable to parse technical indicators json");

            var result = new StockIndicator();
            EnrichWithMetadata(metadataJson, result);
            result.DataPoints = GetTechnicalIndicatorDataPoints(indicatorJson);

            #region old code
            ////input.Substring(input.IndexOf('.') + 1);
            //var substring = jObject.Substring(jObject.IndexOf(@"""Technical") + 1);
            ////"Technical Analysis: SMA\": 
            //substring = substring.Remove(0, 26);
            //substring = substring.Remove(substring.Length - 2);
            //var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(substring);
            //var stockInd = obj as Indicators.IndicatorPoint;
            //return stockInd;
            #endregion

            return result;
        }

        private ICollection<IndicatorPoint> GetTechnicalIndicatorDataPoints(JProperty indicatorJson)
        {
            var result = new List<IndicatorPoint>();

            var indicatorContent = indicatorJson.Children().Single();

            foreach (var dataPointJson in indicatorContent)
            {
                var dataPoint = new IndicatorPoint();
                var dataPointJsonProperty = dataPointJson as JProperty;
                if (dataPointJsonProperty == null)
                    throw new StocksParsingException("Unable to parse time-series");
                dataPoint.Time = DateTime.Parse(dataPointJsonProperty.Name);
                var dataPointContent = dataPointJsonProperty.Single();
                foreach (var field in dataPointContent)
                {
                    var property = (JProperty)field;
                    dataPoint.Value = Decimal.Parse(property.Value.ToString());
                    //contentDict.Add(property.Name, property.Value.ToString());
                }

                result.Add(dataPoint);
            }

            return result;
        }


        private void EnrichWithMetadata([NotNull] JProperty metadataJson, [NotNull] StockIndicator indicator)
        {
            var metadatas = metadataJson.Children().Single();

            foreach (var metadataItem in metadatas)
            {
                var metadataProperty = (JProperty)metadataItem;
                var metadataItemName = metadataProperty.Name;
                var metadataItemValue = metadataProperty.Value.ToString();

                if (metadataItemName.Contains(TechnicalMetaDataJsonTokens.SymbolToken))
                {
                    indicator.MetaData.Symbol = metadataItemValue;
                }
                else if (metadataItemName.Contains(TechnicalMetaDataJsonTokens.IndicatorToken))
                {
                    indicator.MetaData.Indicator = metadataItemValue;
                }
                else if (metadataItemName.Contains(TechnicalMetaDataJsonTokens.RefreshTimeToken))
                {
                    indicator.MetaData.LastRefreshed = DateTime.Parse(metadataItemValue);
                }
                else if (metadataItemName.Contains(TechnicalMetaDataJsonTokens.IntervalToken))
                {
                    indicator.MetaData.Interval = metadataItemValue;
                }
                else if (metadataItemName.Contains(TechnicalMetaDataJsonTokens.PeriodToken))
                {
                    indicator.MetaData.TimePeriod = metadataItemValue;
                }
                else if (metadataItemName.Contains(TechnicalMetaDataJsonTokens.SeriesToken))
                {
                    indicator.MetaData.SeriesType = metadataItemValue;
                }
                else if (metadataItemName.Contains(TechnicalMetaDataJsonTokens.TimeZoneToken))
                {
                    indicator.MetaData.TimeZone = metadataItemValue;
                }
            }
        }

    }
}
