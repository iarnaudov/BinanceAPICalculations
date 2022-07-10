namespace Forex.Services.Contracts
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(string commandKey);
    }
}
