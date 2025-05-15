using DFrameShared;
using MagicOnion;
using MagicOnion.Server;

namespace WebApplication1.Services;

public class MyFirstService : ServiceBase<IMyFirstService>, IMyFirstService
{
    public UnaryResult<int> SumAsync(int x, int y)
    {
        return new UnaryResult<int>(x + y);
    }
}