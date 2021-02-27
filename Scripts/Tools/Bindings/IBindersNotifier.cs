namespace Core.Tools.Bindings
{
    public interface IBindersNotifier
    {
        void AttachBinder(ABinder binder);
        void DetachBinder(ABinder binder);
    }
}