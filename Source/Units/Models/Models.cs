using System;
using System.Collections.Generic;
using System.Linq;
using Core.Tools;

namespace Core.Models
{
    public class Models : Unit
    {
        private Dictionary<Type, Model> models;

        public Models(IEnumerable<Model> setup)
        {
            models = setup.ToDictionary(model => model.GetType());
            foreach (Model model in models.Values)
            {
                Injector.Instance.Add(model);
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
    }
}