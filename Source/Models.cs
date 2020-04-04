using System;
using System.Collections.Generic;
using Core.Abstract;
using Core.Tools;

namespace Core
{
    public class Models
    {
        private Injector injector;
        private Dictionary<Type, IModel> map;

        public Models()
        {
            injector = new Injector();
            map = new Dictionary<Type, IModel>();
        }

        private void Add(IModel model)
        {
            map.Add(model.GetType(), model);
            injector.Add(model);
        }

        public void Add(IEnumerable<IModel> models)
        {
            foreach (var model in models)
            {
                if (model == null) continue;
                Add(model);
            }
        }

        public T Get<T>() where T : IModel
        {
            return (T) map[typeof(T)];
        }

        public void Populate(object obj)
        {
            injector.Inject(obj);
        }
    }
}