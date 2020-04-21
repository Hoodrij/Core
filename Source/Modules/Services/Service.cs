using Core.Tools;

namespace Core.Services
{
    public abstract class Service
    {
        [inject] Lifecycle.Lifecycle lifecycle;

        protected Service()
        {
            Injector.Instance.Populate(this);
                
            if (this is IUpdateHandler iUpdate)
            {
                lifecycle.OnUpdate.Listen(iUpdate.Update);
            }
        }
    }
}