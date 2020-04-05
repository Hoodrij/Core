using System.Threading.Tasks;

namespace Core
{
    public interface IFaderView
    {
        Task WaitForShown();
        Task Hide();
    }
}