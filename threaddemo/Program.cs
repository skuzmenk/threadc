using System.Threading;
using System;

namespace threaddemo
{
    class Program
    {
        private int threadsCount = 10;

        private static SemaphoreSlim semaphore = new SemaphoreSlim(8);

        static void Main(string[] args)
        {
            (new Program()).Start();
        }

        void Start()
        {
            for (int i = 1; i <= threadsCount; i++)
            {
                int threadId = i;
                Thread t = new Thread(() => Calculator(threadId));
                t.Start();
            }
        }

        void Calculator(int threadId)
        {
            semaphore.Wait();

            bool canStop = false;

            new Thread(() =>
            {
                Thread.Sleep(30 * 1000);
                canStop = true;
            }).Start();

            long sum = 0;
            long count = 0;
            long step = 2;

            do
            {
                sum += step;
                count++;
            }
            while (!canStop);

            Console.WriteLine(threadId + " - " + sum + " - " + count);

            semaphore.Release();
        }
    }
}