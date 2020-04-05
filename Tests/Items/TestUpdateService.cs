using Core.Abstract;

namespace Core.Tests
{
    public class TestUpdateService : Service, IUpdate
    {
        public int UpdateCounter;
        
        public void Update()
        {
            UpdateCounter++;
        }
    }
}