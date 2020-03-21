using System;

namespace Core.Tools.Bindings
{
  public interface IBindersNotifier
  {
    void AttachBinder(ABinder binder);
    void DetachBinder(ABinder binder);

    Boolean ReadyForBind { get; }
  }
}