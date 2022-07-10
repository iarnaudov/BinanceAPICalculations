using Forex.Data;
using Forex.Data.Models;
using Forex.Services.Contracts;
using Forex.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using CandleStickInterval = Binance.Spot.Models.Interval;

namespace Forex.Services.Implementations
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private ForexDbContext dbContext;
        private IMarketDataProvider marketDataProvider;
        private int HISTORY_LIMIT_POINTS = 100;

        public DatabaseInitializer(ForexDbContext dbContext, IMarketDataProvider marketDataProvider)
        {
            this.dbContext = dbContext;
            this.marketDataProvider = marketDataProvider;
        }

        public async Task Initialize()
        {
            await this.dbContext.Database.MigrateAsync();
            await this.Seed();
        }

        public async Task Seed()
        {
            if (!this.dbContext.PriceData.Any() && !this.dbContext.Symbols.Any())
            {
                await this.SeedSymbols();
                await this.SeedSymbolsData();
            }
        }

        private async Task SeedSymbols()
        {
            var symbols = new List<Symbol>()
                {
                    new Symbol()
                    {
                        Id = 1,
                        SymbolName = "BTCUSDT"
                    },
                    new Symbol()
                    {
                        Id = 2,
                        SymbolName = "ADAUSDT"
                    },
                    new Symbol()
                    {
                        Id = 3,
                        SymbolName = "ETHUSDT"
                    },
                };
            await this.dbContext.Symbols.AddRangeAsync(symbols);
            await this.dbContext.SaveChangesAsync();
        }

        private async Task SeedSymbolsData()
        {
            // Get Raw Data
            var tasks = new List<Task<IEnumerable<Services.Models.CandleStick>>>();
            var symbols = this.dbContext.Symbols.ToList();
            var intervals = new CandleStickInterval[] { CandleStickInterval.ONE_MINUTE, CandleStickInterval.FIVE_MINUTE, CandleStickInterval.THIRTY_MINUTE, CandleStickInterval.ONE_DAY, CandleStickInterval.ONE_WEEK };
            foreach (var symbol in symbols)
            {
                foreach (var interval in intervals)
                {
                    tasks.Add(this.marketDataProvider.GetSymbolCandleStickData(symbol.ToServiceDTO(), interval, null, null, HISTORY_LIMIT_POINTS));
                }
            }

            // Run in paralel - measured as 4 times faster.
            await Task.WhenAll(tasks);

            // Transform into DB model
            List<CandleStick> results = new List<CandleStick>();
            foreach (var task in tasks)
            {
                var result = task.Result.AsQueryable().Select(DTOTransformationHelper.ToDataModelCandleStick).ToList();
                results.AddRange(result);
            }

            // Save to Database
            await this.dbContext.PriceData.AddRangeAsync(results);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
