namespace ZRES.Commands
{
    using ZRES.ViewModels;
    using Boxed.AspNetCore;

    public interface IGetCarPageCommand : IAsyncCommand<PageOptions>
    {
    }
}
