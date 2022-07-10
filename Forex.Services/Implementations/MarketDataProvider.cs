using Binance.Spot;
using Forex.Services.Contracts;
using Forex.Services.Models;
using Newtonsoft.Json;
using System.Globalization;
using CandleStickInterval = Binance.Spot.Models.Interval;

namespace Forex.Services.Implementations
{
    public class MarketDataProvider : IMarketDataProvider
    {
        private readonly Market marketService;

        public MarketDataProvider()
        {
            this.marketService = new Market();
        }

        public async Task<IEnumerable<CandleStick>> GetSymbolCandleStickData(Symbol symbol, CandleStickInterval interval, long? startTime, long? endTime, int? limit)
        {
            var rawData = await this.marketService.KlineCandlestickData(symbol.SymbolName, interval, startTime, endTime, limit);
            if (!string.IsNullOrEmpty(rawData))
            {
                IEnumerable<dynamic[]> data = JsonConvert.DeserializeObject<IEnumerable<dynamic[]>>(rawData);
                if (data != null)
                {
                    // candleStickData[4] is closing price
                    return data.Select(
                        candleStickData => new CandleStick(
                            candleStickData[0],
                            decimal.Parse(candleStickData[4],
                            CultureInfo.InvariantCulture),
                            interval,
                            symbol.Id));
                }
                else
                {
                    throw new Exception("Data for this symbol cound not be retrieved.");
                }

            }
            else
            {
                throw new Exception("Data for this symbol cound not be retrieved.");
            }
        }
    }
}
