using Core.Abstract;
using Core.Tools;

namespace Core.Tests
{
    public class TestInjectService : Service
    {
        [inject] public TestModel testModel;
        
        protected override void OnStart()
        {
            
        }
    }
}