using Core.Tools;
using Core.Units;

namespace Core.Services
{
    public abstract class Service
    {
        [Inject] Units.Life life;

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