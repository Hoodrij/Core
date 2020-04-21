using Core.Services;

namespace Core.Tests
{
    public class TestUpdateService : Service, IUpdateHandler
    {
        public int UpdateCounter;

        public void Update()
        {
            UpdateCounter++;
        }
    }
}