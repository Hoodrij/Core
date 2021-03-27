using System;
using Core.Tools;
using Core.Tools.Observables;

namespace Core.Units.Model
{
    public abstract class Model
    {
        protected Model()
        {
            Injector.Instance.Add(this);
            Injector.Instance.Populate(this);
        }
    }
    
    public abstract class Model<T> : Model
    {
        private readonly Observable<T> value = new Observable<T>();
        public Event<T> ChangedEvent => value.ChangedEvent;

        public void Set(T t)
        {
            value.Set(t);
        }

        public void Listen(Action<T> action, object target = null)
        {
            value.Listen(action, target);
        }

        public void Listen(Action action, object target = null)
        {
            value.Listen(t => action(), target);
        }

        public void Unsubscribe(object owner)
        {
            value.Unsubscribe(owner);
        }

        public static implicit operator T(Model<T> model) => model.value;
        public static implicit operator string(Model<T> model) => model.value.ToString();

        public override string ToString()
        {
            return value.ToString();
        }
    }
}