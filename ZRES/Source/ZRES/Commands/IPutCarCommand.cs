namespace ZRES.Commands
{
    using ZRES.ViewModels;
    using Boxed.AspNetCore;

    public interface IPutCarCommand : IAsyncCommand<int, SaveCar>
    {
    }
}
