namespace Forex.Services.Contracts
{
    public interface ICommand
    {
        void Execute(string[] parameters);
    }
}
