namespace Core.Tests
{
    public class Service_Test : TestFixture
    {
        // [Test]
        // public void Injection()
        // {
        //     var service = GetService<TestInjectService>();
        //
        //     Assert.NotNull(service.testModel);
        // }

        // [UnityTest]
        // [TestCase(1,1, ExpectedResult = null)]
        // [TestCase(15,15, ExpectedResult = null)]
        // [TestCase(20,30, ExpectedResult = null)]
        // public IEnumerator Update(int waitFrames, int expectedUpdates)
        // {
        //     TestUpdateService service = GetService<TestUpdateService>();
        //     int serviceUpdateCounter = service.UpdateCounter;
        //
        //     for (int i = 0; i < waitFrames; i++)
        //     {
        //         yield return Task.Yield();
        //     }
        //     
        //     serviceUpdateCounter = service.UpdateCounter - serviceUpdateCounter;
        //
        //     if (waitFrames == expectedUpdates)
        //         Assert.AreEqual(expectedUpdates, serviceUpdateCounter);
        //     else
        //         Assert.AreNotEqual(expectedUpdates, serviceUpdateCounter);
        // }
    }
}