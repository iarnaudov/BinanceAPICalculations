using Forex.Services.Models;
using CandleStickInterval = Binance.Spot.Models.Interval;

namespace Forex.Services.Contracts
{
    public interface IMarketDataProvider
    {
        Task<IEnumerable<CandleStick>> GetSymbolCandleStickData(Symbol symbol, CandleStickInterval interval, long? startTime, long? endTime, int? limit);
    }
}
