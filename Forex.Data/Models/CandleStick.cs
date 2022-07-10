using System.ComponentModel.DataAnnotations;
using CandleStickInterval = Binance.Spot.Models.Interval;

namespace Forex.Data.Models
{
    public class CandleStick
    {
        [Key]
        public int Id { get; set; }
        public long CloseTime { get; set; }
        public decimal Price { get; set; }
        public string Interval { get; set; }
        public int SymbolId { get; set; }
        public Symbol Symbol { get; set; }
    }
}
