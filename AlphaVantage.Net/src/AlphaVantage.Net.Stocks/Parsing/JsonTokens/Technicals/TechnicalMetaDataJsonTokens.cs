namespace AlphaVantage.Net.Stocks.Parsing.JsonTokens.Technicals
{
    /// <summary>
    /// Meta Data tokens for technicals
    /// </summary>
    public class TechnicalMetaDataJsonTokens
    {
        public const string MetaDataHeader = "Meta Data";

        public const string SymbolToken = "1: Symbol";
        public const string IndicatorToken = "2: Indicator";
        public const string RefreshTimeToken = "3: Last Refreshed";
        public const string IntervalToken = "4: Interval";
        public const string PeriodToken = "5: Time Period";
        public const string SeriesToken = "6: Series Type";
        public const string TimeZoneToken = "7: Time Zone";
    }
}
