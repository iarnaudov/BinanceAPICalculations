using System.Linq.Expressions;

namespace Forex.Services.Helpers
{
    public static class DTOTransformationHelper
    {
        public static readonly Expression<Func<Services.Models.CandleStick, Data.Models.CandleStick>> ToDataModelCandleStick = entity => new Data.Models.CandleStick()
        {
           SymbolId = entity.SymbolId,
           CloseTime = entity.CloseTime,
           Interval = entity.Interval.ToString(),
           Price = entity.Price,
        };

        public static Services.Models.Symbol ToServiceDTO(this Data.Models.Symbol symbol)
        {
            return new Services.Models.Symbol() { Id = symbol.Id, SymbolName = symbol.SymbolName };
        }
    }
}
