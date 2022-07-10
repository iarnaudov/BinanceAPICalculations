using Forex.Services.Helpers;

namespace Forex.Services.Implementations
{
    public class SimpleMovingAverageCalculator
    {
        private FixedSizeQueue fixedSizeQueue;

        public SimpleMovingAverageCalculator(int numberOfDataPoints)
        {
            if (numberOfDataPoints <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfDataPoints), "Data points must be greater than 0");
            }

            this.fixedSizeQueue = new FixedSizeQueue(numberOfDataPoints);
        }

        public List<decimal> Calculate(decimal[] values)
        {
            List<decimal> result = new List<decimal>();
            foreach (var value in values)
            {
                result.Add(this.CalculateValue(value));
            }
            return result;
        }

        private decimal CalculateValue(decimal nextInput)
        {
            this.fixedSizeQueue.Enqueue(nextInput);
            return this.fixedSizeQueue.Sum() / this.fixedSizeQueue.Size;
        }
    }
}
