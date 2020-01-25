namespace System.Collections.Generic
{
	public interface IFaderView
	{
		bool IsShown { get; }
		void ShowView();
		void HideView();
	}
}