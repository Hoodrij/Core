using System.Threading.Tasks;

namespace Core.Tools.Observables
{
    public static class EventEx
    {
        public static Task ToTask(this Event @event)
        {
            return Task.Run(async () => await @event);
        }
        
        public static IObservableAwaiter GetAwaiter(this IObservable e)
        {
            return new IObservableAwaiter(e);
        }

        public static IObservableAwaiter<T> GetAwaiter<T>(this IObservable<T> e)
        {
            return new IObservableAwaiter<T>(e);
        }
    }
}