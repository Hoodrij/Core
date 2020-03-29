namespace Core.Tools.Bindings
{
    public interface IBindersNotifier
    {
        bool ReadyForBind { get; }
        void AttachBinder(ABinder binder);
        void DetachBinder(ABinder binder);
    }
}