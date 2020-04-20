namespace Core
{
    public abstract class Service
    {
        protected internal Life Life { get; internal set; }

        protected internal abstract void OnStart();
    }
}