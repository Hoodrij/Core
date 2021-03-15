using Core.Tools;

namespace Core.Services
{
    public abstract class Service
    {
        [Inject] Units.Life Life { get; }

        protected Service()
        {
            Injector.Instance.Add(this);
            Injector.Instance.Populate(this);

            if (this is IUpdateHandler iUpdate)
            {
                Life.UpdateEvent.Listen(iUpdate.Update);
            }
        }
    }
}