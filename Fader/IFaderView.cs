using System.Collections;

namespace Core
{
	public interface IFaderView
	{
		IEnumerator WaitForShown();
		void Hide();
	}
}