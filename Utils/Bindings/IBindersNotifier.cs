using System;

namespace Bindings
{
  public interface IBindersNotifier
  {
    void AttachBinder(ABinder binder);
    void DetachBinder(ABinder binder);

    Boolean ReadyForBind { get; }
  }
}