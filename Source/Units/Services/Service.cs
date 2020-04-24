using Core.Tools;

namespace Core.Services
{
    public abstract class Service
    {
        [inject] Life life;

        protected Service()
        {
            Injector.Instance.Populate(this);
                
            if (this is IUpdateHandler iUpdate)
            {
                life.OnUpdate.Listen(iUpdate.Update);
            }
        }
    }
}