namespace ZRES.Commands
{
    using ZRES.ViewModels;
    using Boxed.AspNetCore;

    public interface IPostCarCommand : IAsyncCommand<SaveCar>
    {
    }
}
