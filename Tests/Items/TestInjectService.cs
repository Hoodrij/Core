using Core.Services;

namespace Core.Tests
{
    public class TestInjectService : Service
    {
        [inject] public TestModel testModel;
    }
}