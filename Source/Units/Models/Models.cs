using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Abstract;
using Core.GameSetup;
using Core.Tools;
using Core.Tools.ExtensionMethods;
using JetBrains.Annotations;

namespace Core
{
    public class Models : Unit
    {
        private Dictionary<Type, Model> models;

        public Models(IGameSetup setup)
        {
            models = setup.Models().ToDictionary(model => model.GetType());
            foreach (Model model in models.Values)
            {
                Injector.Instance.Add(model);
            }
        }

        protected override void OnStart()
        {
            "[Models] [OnStart]".log();

            foreach (Model model in models.Values)
            {
                model.Reset();
            }
        }

        public T Get<T>() where T : Model
        {
            return (T) models[typeof(T)];
        }

        public void Populate(object o)
        {
            Injector.Instance.Populate(o);
        }

        internal void Reset()
        {
            foreach (Model model in models.Values)
            {
                model.Reset();
            }
        }
    }
}