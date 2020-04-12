using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Abstract;
using Core.Tools;
using JetBrains.Annotations;

namespace Core
{
    public class Models
    {
        private readonly Injector injector;
        private readonly Dictionary<Type, Model> models;

        internal Models(IEnumerable<Model> setup)
        {
            injector = new Injector();
            models = new Dictionary<Type, Model>();
            
            Add(setup);
        }

        private void Add(IEnumerable<Model> setup)
        {
            foreach (Model model in setup)
            {
                if (model == null) continue;
                models.Add(model.GetType(), model);
                injector.Add(model);
            }
        }

        public T Get<T>() where T : Model
        {
            return (T) models[typeof(T)];
        }

        public void Populate(object obj)
        {
            injector.Inject(obj);
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