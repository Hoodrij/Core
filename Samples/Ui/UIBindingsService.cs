using Core.Abstract;
using Core.Samples.GameState;
using Core.Ui;

namespace Core.Samples.Ui
{
	public class UIBindingsService : Service
	{
		[inject] GameStateModel stateModel;
		[inject] UIModel uiModel;

		public UIBindingsService()
		{
			Map(GameState.GameState.Menu, uiModel.SampleView);
		}
		
		private void Map(GameState.GameState state, UIInfo info)
		{
			UIView viewCached = null;
			stateModel.FSM.ListenEnter(state, () =>
			{
				info.Open(view => viewCached = view);
			});

			stateModel.FSM.ListenExit(state, () =>
			{
				viewCached.Close();
			});
		}
	}
}