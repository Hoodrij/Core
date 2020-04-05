using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Abstract;
using Core.Tools;

namespace Core
{
    public class Models
    {
        private Injector injector;
        private Dictionary<Type, Model> models;

        public Models()
        {
            injector = new Injector();
            models = new Dictionary<Type, Model>();
        }

        public void Add(Model model)
        {
            models.Add(model.GetType(), model);
            injector.Add(model);
        }

        public void Add(IEnumerable<Model> models)
        {
            foreach (var model in models)
            {
                if (model == null) continue;
                Add(model);
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