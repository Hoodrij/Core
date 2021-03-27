namespace Core.Tools.Observables
{
    public static class EventEx
    {
        public static EventAwaiter GetAwaiter(this Event e) => new EventAwaiter(e);
        public static EventAwaiter<T> GetAwaiter<T>(this Event<T> e) => new EventAwaiter<T>(e);
    }
}