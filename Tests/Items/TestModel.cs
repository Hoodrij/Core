using Core.Abstract;

namespace Core.Tests
{
    public class TestModel : Model
    {
        public int i = 8;

        protected override void Reset()
        {
            i = 8;
        }
    }
}