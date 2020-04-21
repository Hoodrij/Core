using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Assets;
using Core.Tools.ExtensionMethods;
using UnityEngine;

namespace Core.Fader
{
    public class Fader : Module
    {
        [inject] IAssets Assets;
        
        private readonly Queue<Func<Task>> actions = new Queue<Func<Task>>();
        private IFaderView view;

        public Fader()
        {
            SetView(Assets.Spawn<IFaderView>("BaseFaderView", true));

            Worker();
        }

        private async void Worker()
        {
            while (true)
            {
                await new WaitForEndOfFrame();

                if (actions.IsEmpty()) continue;

                if (view != null) await view.Show();

                Func<Task> action = actions.Dequeue();
                await action();
                await TryHideView();
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
        
        private async Task TryHideView()
        {
            if (view == null) return;
            if (actions.IsEmpty()) await view.Hide();
        }
    }
}