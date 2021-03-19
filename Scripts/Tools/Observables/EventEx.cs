using System.Threading.Tasks;

namespace Core.Tools.Observables
{
    public static class EventEx
    {
        public static Task ToTask(this Event @event)
        {
            return Task.Run(async () => await @event);
        }
        
        public static EventAwaiter GetAwaiter(this Event e)
        {
            return new EventAwaiter(e);
        }

        public static EventAwaiter<T> GetAwaiter<T>(this Event<T> e)
        {
            return new EventAwaiter<T>(e);
        }
    }
}