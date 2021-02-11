using Core.Tools.Observables;

namespace Core.Units.Model2
{
    public class Model2<T>
    {
        private T t;
        private Signal<T> signal = new Signal<T>();

        public void Set(T t)
        {
            this.t = t;
        }
        
        public static implicit operator T(Model2<T> model) => model.t;
    }
}