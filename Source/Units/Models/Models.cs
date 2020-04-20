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
        [inject] IGameSetup Setup;
        
        private Dictionary<Type, Model> models;

        protected override void OnStart()
        {
            "[Models] [OnStart]".log();

            if (models == null || models.IsEmpty())
            {
                models = Setup.Models().ToDictionary(model => model.GetType());
                foreach (Model model in models.Values)
                {
                    Injector.Instance.Add(model);
                }
            }

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