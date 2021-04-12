using System;
using System.Reflection;
using UnityEngine;

namespace Core.Tools.Observables
{
    internal class WeakAction<T>
    {
        public bool IsAlive => owner.IsAlive && (owner != null ? owner.Target != null : false) 
                                && IsAliveAsMonoBeh();
        
        private readonly WeakReference owner;
        private readonly MethodInfo method;
        private readonly object target;
        
        public WeakAction(Action<T> action)
        {
            owner ??= new WeakReference(action.Target);
            method = action.Method;
            target = action.Target;
        }
        public WeakAction(Action<T> action, object owner) : this(action)
        {
            this.owner = new WeakReference(owner != null ? owner : action.Target);
        }

        public void Invoke(T t)
        {
            if (IsActiveAsMonoBeh())
                method.Invoke(target, new object[] { t });
        }

        public bool IsOwnedBy(object owner) => this.owner.Target == owner;
        
        private bool IsAliveAsMonoBeh() => !(owner.Target is MonoBehaviour mono) || mono != null && mono.gameObject != null;
        private bool IsActiveAsMonoBeh() => !(owner.Target is MonoBehaviour mono) || mono.gameObject.activeInHierarchy;
    }
    
    internal class WeakAction
    {
        public bool IsAlive => owner.IsAlive && (owner != null ? owner.Target != null : false) 
                                && IsAliveAsMonoBeh();
        
        private readonly WeakReference owner;
        private readonly MethodInfo method;
        private readonly object target;
        
        public WeakAction(Action action)
        {
            owner ??= new WeakReference(action.Target);
            method = action.Method;
            target = action.Target;
        }
        public WeakAction(Action action, object owner) : this(action)
        {
            this.owner = new WeakReference(owner != null ? owner : action.Target);
        }

        public void Invoke()
        {
            if (IsActiveAsMonoBeh())
                method.Invoke(target, null);
        }

        public bool IsOwnedBy(object owner) => this.owner.Target == owner;
        
        private bool IsAliveAsMonoBeh() => !(owner.Target is MonoBehaviour mono) || mono != null && mono.gameObject != null;
        private bool IsActiveAsMonoBeh() => !(owner.Target is MonoBehaviour mono) || mono.gameObject.activeInHierarchy;
    }
}