using Forex.Services.Contracts;

namespace Forex.Services.Implementations.Commands
{
    public class SimpleMovingAverageCommand : Contracts.ICommand
    {
        private readonly ICalculationServiceProvider calculationProvider;
        public SimpleMovingAverageCommand(ICalculationServiceProvider calculationProvider)
        {
            this.calculationProvider = calculationProvider;
        }

        public void Execute(string[] parameters)
        {
            if (parameters.Length < 4)
            {
                throw new ArgumentException("Input parameters not correct.");
            }

            var symbol = parameters[0];
            var numberOfDataPoints = int.Parse(parameters[1]);
            var interval = parameters[2];
            var dateTime = DateTime.Parse(parameters[3]);

            var result = this.calculationProvider.CalculateSimpleMovingAverage(symbol, numberOfDataPoints, interval, dateTime);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The simple moving average for this period has the following values: " + string.Join(", ", result));
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
