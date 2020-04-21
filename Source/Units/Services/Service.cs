using Core.Tools;

namespace Core
{
    public abstract class Service
    {
        [inject] Lifecycle lifecycle;

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