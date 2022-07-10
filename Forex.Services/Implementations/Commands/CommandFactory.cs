using Forex.Services.Contracts;

namespace Forex.Services.Implementations.Commands
{
    public class CommandFactory : ICommandFactory
    {
        private readonly ICalculationServiceProvider calculationProvider;
        public CommandFactory(ICalculationServiceProvider calculationProvider)
        {
            this.calculationProvider = calculationProvider;
        }

        public ICommand CreateCommand(string commandKey)
        {
            if (commandKey == "24h")
            {
                return new DailyCommand(this.calculationProvider);
            }
            else if (commandKey == "sma")
            {
                return new SimpleMovingAverageCommand(this.calculationProvider);
            }
            else
            {
                throw new Exception("Command key not recognized");
            }
        }
    }
}
