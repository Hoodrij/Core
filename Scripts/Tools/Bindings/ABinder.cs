using System;
using System.Reflection;
using Core.Tools.ExtensionMethods;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Tools.Bindings
{
    [DefaultExecutionOrder(-1)]
    public abstract class ABinder : MonoBehaviour
    {
        [SerializeField] private string _memberName;

        [SerializeField] protected string _params;
        [SerializeField] private Component _target;

        public Component Target => _target;

        public string MemberName => _memberName;

        public static void Bind(GameObject go)
        {
            go.SendMessage("Bind", SendMessageOptions.DontRequireReceiver);
        }

        public static void BindBroadcast(GameObject go)
        {
            go.BroadcastMessage("Bind", SendMessageOptions.DontRequireReceiver);
        }

        [ContextMenu("Rebind")] public void Rebind()
        {
            Bind();
        }

        public override string ToString()
        {
            if (Target == null)
                return name + "->" + GetType().Name;

            return name + "->" + GetType().Name + " On " + Target.name + "->" + Target.GetType().Name + "." +
                   MemberName;
        }

        protected abstract void Bind();

        protected void Init<TArg>(ref Action<TArg> action, bool requireSetter = true)
        {
            BindSource bindSource = new BindSource {Target = _target, MemberName = _memberName, Params = _params};

            Init(ref action, ref bindSource, requireSetter);
        }

        protected void Init<TResult>(ref Func<TResult> func, bool requireGetter = true)
        {
            BindSource bindSource = new BindSource {Target = _target, MemberName = _memberName, Params = _params};

            Init(ref func, ref bindSource, requireGetter);
        }

        protected void Init<TArg>(ref Action<TArg> action, ref BindSource bindSource, bool requireSetter = true)
        {
            try
            {
                Type type = bindSource.Target.GetType();

                do
                {
                    PropertyInfo prop = type.GetProperty(bindSource.MemberName,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                    if (prop != null)
                    {
                        MethodInfo propSetter = prop.GetSetMethod(true);

                        if (propSetter != null)
                        {
                            action = (Action<TArg>) Delegate.CreateDelegate(typeof(Action<TArg>), bindSource.Target,
                                propSetter);
                            return;
                        }
                    }

                    type = type.BaseType;
                } while (type != typeof(Object));
            }
            catch (Exception ex)
            {
                Debug.LogException(ex, this);
            }

            if (requireSetter)
                Debug.LogError(
                    "[ABinder] - Init Fail: " + bindSource.Target.name + "->" + bindSource.Target.GetType().Name + "." +
                    bindSource.MemberName + " has no setter", this);
        }

        protected void Init<TResult>(ref Func<TResult> action, ref BindSource bindSource, bool requireGetter = true)
        {
            try
            {
                Type type = bindSource.Target.GetType();

                do
                {
                    PropertyInfo prop = type.GetProperty(bindSource.MemberName,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                    if (prop != null)
                        if (prop.GetCustomAttributes(typeof(BindAttribute), true).Length > 0)
                        {
                            MethodInfo propGetter = prop.GetGetMethod(true);

                            if (propGetter.ReturnType == typeof(Enum) && typeof(TResult) != typeof(Enum))
                            {
                                Func<Enum> delegat = (Func<Enum>) Delegate.CreateDelegate(typeof(Func<Enum>),
                                    bindSource.Target, propGetter);

                                action = () => (TResult) (object) delegat();
                            }
                            else
                            {
                                action = (Func<TResult>) Delegate.CreateDelegate(typeof(Func<TResult>),
                                    bindSource.Target, propGetter);
                            }

                            return;
                        }

                    foreach (MethodInfo method in type.GetMethods(
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        if (method.Name != bindSource.MemberName ||
                            !typeof(TResult).IsAssignableFrom(method.ReturnType) ||
                            method.GetCustomAttributes(typeof(BindAttribute), true).Length == 0)
                            continue;

                        action = BindMethod<TResult>(bindSource.Target, method, bindSource.Params);

                        if (action != null)
                            return;
                    }

                    type = type.BaseType;
                } while (type != typeof(Object));
            }
            catch (Exception ex)
            {
                Debug.LogException(ex, this);
            }

            if (requireGetter)
                Debug.LogError(
                    "[ABinder] - Init Fail: " + bindSource.Target.name + "->" + bindSource.Target.GetType().Name + "." +
                    bindSource.MemberName + " has no getter", this);

            else
                Debug.Log(
                    "[ABinder] - Property " + bindSource.Target.name + "->" + bindSource.Target.GetType().Name + "." +
                    bindSource.MemberName + " has no getter. Binder get logic will not work.", this);
        }

        protected virtual void OnEnable()
        {
            if (_target is IBindersNotifier target)
            {
                target.AttachBinder(this);
                return;
            }
        
            Bind();
        }

        protected virtual void OnDisable()
        {
            if (_target is IBindersNotifier target2)
                target2.DetachBinder(this);
        }

        protected virtual void OnDestroy()
        {
            if (_target is IBindersNotifier target2)
                target2.DetachBinder(this);
        }

        private void OnValidate()
        {
            if (_target == null)
                _target = this.GetComponentInParent<ABindableBehaviour>(true);
        }

        internal void RebindOnPropertyChanged(string prop)
        {
            if (prop != "*" && prop != _memberName)
                return;

            Bind();
        }

        private Func<TResult> BindMethod<TResult>(Object target, MethodInfo method, string parameters)
        {
            ParameterInfo[] @params = method.GetParameters();
            if (@params.Length == 0)
                return (Func<TResult>) Delegate.CreateDelegate(typeof(Func<TResult>), target, method);

            if (@params.Length != 1) return null;

            if (@params[0].ParameterType == typeof(string))
                return new StringParamBinder<TResult>
                {
                    Param = parameters,
                    Function = (Func<string, TResult>) Delegate.CreateDelegate(typeof(Func<string, TResult>), target,
                        method)
                }.GetValue;

            if (@params[0].ParameterType == typeof(int))
                if (int.TryParse(parameters, out int result))
                    return new IntParamBinder<TResult>
                    {
                        Param = result,
                        Function = (Func<int, TResult>) Delegate.CreateDelegate(typeof(Func<int, TResult>), target,
                            method)
                    }.GetValue;

            if (@params[0].ParameterType == typeof(float))
                if (float.TryParse(parameters, out float result))
                    return new SingleParamBinder<TResult>
                    {
                        Param = result,
                        Function = (Func<float, TResult>) Delegate.CreateDelegate(typeof(Func<float, TResult>), target,
                            method)
                    }.GetValue;

            if (@params[0].ParameterType == typeof(bool))
                if (bool.TryParse(parameters, out bool result))
                    return new BooleanParamBinder<TResult>
                    {
                        Param = result,
                        Function = (Func<bool, TResult>) Delegate.CreateDelegate(typeof(Func<bool, TResult>), target,
                            method)
                    }.GetValue;

            if (@params[0].ParameterType == typeof(Enum))
            {
                BindAttribute[] attributes = (BindAttribute[]) method.GetCustomAttributes(typeof(BindAttribute), true);
                Enum result = (Enum) Enum.Parse(attributes[0].ArgumentType, parameters);

                return new EnumParamBinder<TResult>
                {
                    Param = result,
                    Function = (Func<Enum, TResult>) Delegate.CreateDelegate(typeof(Func<Enum, TResult>), target,
                        method)
                }.GetValue;
            }

            if (@params[0].ParameterType == typeof(Object))
                return new ObjectParamBinder<TResult>
                {
                    Param = gameObject,
                    Function = (Func<Object, TResult>) Delegate.CreateDelegate(typeof(Func<Object, TResult>), target,
                        method)
                }.GetValue;

            return null;
        }

        [Serializable] public struct BindSource
        {
            public Component Target;
            public string MemberName;
            public string Params;
        }

        #region | SubTypes |

        private class StringParamBinder<TType>
        {
            public Func<string, TType> Function;
            public string Param;

            public TType GetValue()
            {
                return Function(Param);
            }
        }

        private class IntParamBinder<TType>
        {
            public Func<int, TType> Function;
            public int Param;

            public TType GetValue()
            {
                return Function(Param);
            }
        }

        private class SingleParamBinder<TType>
        {
            public Func<float, TType> Function;
            public float Param;

            public TType GetValue()
            {
                return Function(Param);
            }
        }

        private class BooleanParamBinder<TType>
        {
            public Func<bool, TType> Function;
            public bool Param;

            public TType GetValue()
            {
                return Function(Param);
            }
        }

        private class EnumParamBinder<TType>
        {
            public Func<Enum, TType> Function;
            public Enum Param;

            public TType GetValue()
            {
                return Function(Param);
            }
        }

        private class ObjectParamBinder<TType>
        {
            public Func<Object, TType> Function;
            public Object Param;

            public TType GetValue()
            {
                return Function(Param);
            }
        }

        #endregion
    }
}