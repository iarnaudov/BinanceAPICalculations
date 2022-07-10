using Forex.Data;
using Forex.Data.Models;
using Forex.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using CandleStickInterval = Binance.Spot.Models.Interval;

namespace Forex.Services.Implementations
{
    public class CalculationServiceProvider : ICalculationServiceProvider
    {
        private readonly ForexDbContext dbContext;
        private readonly IMemoryCache memoryCache;
        private readonly string SYMBOLS_DATA_KEY = "Symbols";

        public CalculationServiceProvider(ForexDbContext dbContext, IMemoryCache memoryCache)
        {
            this.dbContext = dbContext;
            this.memoryCache = memoryCache;
        }
        public decimal CalculateLastDayAvgPrice(string symbol)
        {
            DateTime dayBefore = DateTime.UtcNow.AddDays(-1);
            long dayBeforeUnixTime = ((DateTimeOffset)dayBefore).ToUnixTimeMilliseconds();

            if (!string.IsNullOrEmpty(symbol))
            {
                var symbolObject = this.GetSymbolData(symbol);
                var lastDayCandleSticks = this.dbContext.PriceData.Where(p =>
                                                             p.SymbolId == symbolObject.Id &&
                                                             p.CloseTime >= dayBeforeUnixTime &&
                                                             p.Interval == CandleStickInterval.THIRTY_MINUTE.ToString());


                var averagePrice = lastDayCandleSticks.Select(p => p.Price).Average();
                return averagePrice;
            } 
            else
            {
                throw new ArgumentException("Please pass a valid symbol");
            }
        }

        public List<decimal> CalculateSimpleMovingAverage(string symbol, int numberOfDataPoints, string interval, DateTime startDateTime)
        {
            if (!string.IsNullOrEmpty(symbol))
            {
                var symbolObject = this.GetSymbolData(symbol);
                long startTime = ((DateTimeOffset)startDateTime).ToUnixTimeMilliseconds();
                var priceData = this.dbContext.PriceData
                                            .Where(p => p.SymbolId == symbolObject.Id &&
                                                        p.CloseTime >= startTime &&
                                                        p.Interval == interval)
                                            .Select(p => p.Price)
                                            .ToArray();

                var smaCalculator = new SimpleMovingAverageCalculator(numberOfDataPoints);
                var result = smaCalculator.Calculate(priceData);
                return result;
            }
            else
            {
                throw new ArgumentException("Please pass a valid symbol");
            }
        }

        private Symbol GetSymbolData(string symbolName)
        {
            Symbol symbolObject;
            string symbolKey = SYMBOLS_DATA_KEY + $"_{symbolName}";
            if (!memoryCache.TryGetValue(symbolKey, out symbolObject))
            {
                symbolObject = this.dbContext.Symbols.FirstOrDefault(s => s.SymbolName == symbolName);

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(10));
                if (symbolObject != null)
                {
                    memoryCache.Set(symbolKey, symbolObject, cacheEntryOptions);
                }
                else
                {
                    throw new Exception("The requested symbol do not exists in the database");
                }
            }

            return symbolObject;
        }
    }
}
