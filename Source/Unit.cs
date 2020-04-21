using Core.Tools;

namespace Core
{
    public abstract class Unit
    {
        protected internal Life Life { get; private set; }

        protected Unit()
        {
            Injector.Instance.Add(this);

            void StartAction()
            {
                Injector.Instance.Populate(this);
                Life = Life.Eternal.Derive(GetType().Name);
                Life.Eternal.Add(StartAction, null);
                OnStart();
            }
            
            Life.Eternal.Add(StartAction, null);
        }

        protected abstract void OnStart();
    }
}