using System;
using System.Threading;

namespace ComputerTest.Util
{
    public static class ThreadHelper
    {
        public static void ExecuteThenSleepShortly(Action action, Int16 milliSeconds = 10)
        {
            action();
            Thread.Sleep(milliSeconds);
        }
    }
}
