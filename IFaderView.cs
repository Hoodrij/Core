namespace Core
{
	public interface IFaderView
	{
		bool IsShown { get; }
		void ShowView();
		void HideView();
	}
}