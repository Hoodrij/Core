using System;

namespace Core.Ui
{
	[AttributeUsage(AttributeTargets.Class)]
	public class UIRootCloseParamsAttribute : Attribute
	{
		public Type[] RootsToClose;

		public UIRootCloseParamsAttribute(params Type[] close)
		{
			RootsToClose = close;
		}
	}
}