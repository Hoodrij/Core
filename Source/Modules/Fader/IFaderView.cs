using System.Threading.Tasks;

namespace Core.Fader
{
    public interface IFaderView
    {
        Task Show();
        Task Hide();
    }
}