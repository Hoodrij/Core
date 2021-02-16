namespace Core.Tests
{
    public class Signal_Test
    {
        // [Test]
        // public void CheckSubscribe()
        // {
        //     
        // }
        //
        // [Test]
        // public void CheckUnsubscribe()
        // {
        //     
        // }
        //
        // [Test]
        // public void CheckListenersDuplicate()
        // {
        //     
        // }
        //
        // [Test]
        // public void CheckFireSignal()
        // {
        //     
        // }
        //
        // [Test]
        // public void CheckFewListeners()
        // {
        //     
        // }
        
//        [UnityTest]
//        public IEnumerator CheckCGCollect()
//        {
//            Signal s = new Signal();
//            TestSubscriber t = new TestSubscriber(s);
//            s.Fire();
//            Assert.AreEqual(s.ListenersCount, 1);
//            t = null;
//            GC.Collect();
//            yield return new WaitForSeconds(1);
//            s.Fire();
//            Assert.AreEqual(s.ListenersCount, 0);
//        }
//        
//        class TestSubscriber
//        {
//            public int firesCount = 0;
//            public TestSubscriber(Signal s) => s.Listen(Action);
//            private void Action() => firesCount++;
//        }
    }
}
