namespace Forex.Services.Contracts
{
    public interface ICalculationServiceProvider
    {
        decimal CalculateLastDayAvgPrice(string symbol);

        List<decimal> CalculateSimpleMovingAverage(string symbol, int numberOfDataPoints, string interval, DateTime startDateTime);
    }
}
