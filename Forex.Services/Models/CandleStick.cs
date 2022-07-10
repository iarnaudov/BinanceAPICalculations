using CandleStickInterval = Binance.Spot.Models.Interval;

namespace Forex.Services.Models
{
    public class CandleStick
    {
        public long CloseTime { get; set; }
        public decimal Price { get; set; }
        public CandleStickInterval Interval { get; set; }
        public int SymbolId { get; set; }

        public CandleStick(long open, decimal price, CandleStickInterval interval, int symbolId)
        {
            this.CloseTime = open;
            this.Price = price;
            this.Interval = interval;
            this.SymbolId = symbolId;   
        }
    }
}
