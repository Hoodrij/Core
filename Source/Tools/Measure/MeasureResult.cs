using System;

namespace Core.Tools
{
    public class MeasureResult
    {
        public Action Action;
        public int Count;
        public string Name;
        public double Ticks;
        public float MemoryAlloc;
    }
}