using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Fader;
using Core.Tools.ExtensionMethods;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Units
{
    public class Fader : Unit
    {
        private readonly Queue<Func<Task>> actions = new Queue<Func<Task>>();
        private IFaderView view;

        public Fader()
        {
            Worker();
        }
        
        public Fader WithSampleView()
        {
            SetView(Object.Instantiate(Resources.Load<GameObject>("SampleFader"))
                .GetComponent<IFaderView>());
            return this;
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

        public async void SetView(IFaderView view)
        {
            if (this.view != null && this.view is Component oldView)
            {
                await new WaitUntil(() => actions.IsEmpty());
                await this.view.Hide();
                oldView.gameObject.Destroy();
            }
            
            if (view is Component viewComp)
                viewComp.gameObject.name = "Fader";

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