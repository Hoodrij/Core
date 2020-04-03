namespace Core.Abstract
{
    public abstract class Service
    {
        protected Service()
        {
            Game.Models.Populate(this);
            Start();
        }

        protected virtual void Start()
        {
            
        }
    }

    public abstract class RuntimeService : Service
    {
        protected internal abstract void Update();
    }
}