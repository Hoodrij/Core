using System.Threading.Tasks;

namespace Core
{
    public interface IFaderView
    {
        Task Show();
        Task Hide();
    }
}