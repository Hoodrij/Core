using Core.Services;

namespace Core.Tests
{
    public class TestInjectService : Service
    {
        [Inject] public TestModel testModel;
    }
}