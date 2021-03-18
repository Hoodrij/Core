using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Fader;
using Core.Tools;
using Core.Tools.ExtensionMethods;
using UnityAsync;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Units
{
    public class Fader : Unit, Lazy
    {
        private readonly Queue<Func<Task>> queue = new Queue<Func<Task>>();
        private IFaderView view;

        public Fader()
        {
            Flow();
        }
        
        public Fader WithSampleView()
        {
            SetView(Object.Instantiate(Resources.Load<GameObject>("SampleFader"))
                .GetComponent<IFaderView>());
            return this;
        }

        private async void Flow()
        {
            while (true)
            {
                await Wait.Until(() => queue.Any());

                if (view != null)
                    await view.Show();
                
                while (queue.Any())
                {
                    await queue.Dequeue()();
                }
                
                if (view != null)
                    await view.Hide();
            }
        }

        public async Task SetView(IFaderView newView)
        {
            if (view is Component oldView)
            {
                await Wait.Until(() => queue.IsEmpty());
                await view.Hide();
                oldView.gameObject.Destroy();
            }

            view = newView;
        }

        public void Enqueue(Func<Task> action)
        {
            queue.Enqueue(action);
        }
    }
}