using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Tools.ExtensionMethods;
using UnityEngine;

namespace Core
{
    public class Fader
    {
        private Queue<Func<Task>> actions = new Queue<Func<Task>>();
        private IFaderView view;

        public Fader()
        {
            SetView(Game.Assets.Spawn<IFaderView>("BaseFaderView", true));

            Worker();
        }

        private async void Worker()
        {
            while (true)
            {
                await new WaitForEndOfFrame();

                if (actions.IsEmpty()) continue;

                if (view != null) await view.WaitForShown();

                var action = actions.Dequeue();
                await action();
                TryHideView();
            }
        }

        public void SetView(IFaderView view)
        {
            this.view = view;
        }

        public void AddAction(Func<Task> action)
        {
            actions.Enqueue(action);
        }

        private void TryHideView()
        {
            if (view == null) return;
            if (actions.IsEmpty()) view.Hide();
        }
    }
}