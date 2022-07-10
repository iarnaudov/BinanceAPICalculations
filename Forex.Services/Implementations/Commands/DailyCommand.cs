using Forex.Services.Contracts;

namespace Forex.Services.Implementations.Commands
{
    public class DailyCommand : Contracts.ICommand
    {
        private readonly ICalculationServiceProvider calculationProvider;
        public DailyCommand(ICalculationServiceProvider calculationProvider)
        {
            this.calculationProvider = calculationProvider;
        }

        public void Execute(string[] parameters)
        {
            if (string.IsNullOrEmpty(parameters[0]))
            {
                throw new ArgumentException("Please provide symbol value");
            }
            var result = this.calculationProvider.CalculateLastDayAvgPrice(parameters[0]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The last 24h average price is: " + result);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
