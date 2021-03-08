using Core.Tools;

namespace Core.Services
{
    public abstract class Service
    {
        [Inject] Units.Life life;

        protected Service()
        {
            Injector.Instance.Add(this);
            Injector.Instance.Populate(this);

            if (this is IUpdateHandler iUpdate)
            {
                life.UpdateEvent.Listen(iUpdate.Update);
            }
        }
    }
}