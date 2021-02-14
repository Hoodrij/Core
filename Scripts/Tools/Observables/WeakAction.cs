﻿﻿using System;
using System.Reflection;
using UnityEngine;

namespace Core.Tools.Observables
{
    internal class WeakAction<T>
    {
        public bool IsAlive => owner.IsAlive && owner?.Target != null 
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
            this.owner = new WeakReference(owner);
        }

        public void Invoke(T t)
        {
            if (IsActiveAsMonoBeh())
                method.Invoke(target, new object[] { t });
        }

        public bool Equals(Action<T> action) => owner.Target == action.Target;
        
        
        private bool IsAliveAsMonoBeh() => !(owner.Target is MonoBehaviour mono) || mono != null && mono.gameObject != null;
        private bool IsActiveAsMonoBeh() => !(owner.Target is MonoBehaviour mono) || mono.gameObject.activeInHierarchy;
    }
    
    internal class WeakAction
    {
        public bool IsAlive => owner.IsAlive && owner?.Target != null 
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
            this.owner = new WeakReference(owner);
        }

        public void Invoke()
        {
            if (IsActiveAsMonoBeh())
                method.Invoke(target, null);
        }

        public bool Equals(Action action) => owner.Target == action.Target;
        
        private bool IsAliveAsMonoBeh() => !(owner.Target is MonoBehaviour mono) || mono != null && mono.gameObject != null;
        private bool IsActiveAsMonoBeh() => !(owner.Target is MonoBehaviour mono) || mono.gameObject.activeInHierarchy;
    }
}