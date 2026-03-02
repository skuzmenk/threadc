using System.Threading;
using System;

namespace threaddemo
{
    class Program
    {
        private int threadsCount = 3; 
        static void Main(string[] args)
        {
            (new Program()).Start();
        }

        void Start()
        {
             Thread stopperThread = new Thread(Stoper);
            stopperThread.Start();
            for (int i = 1; i <= threadsCount; i++)
            {
                int threadId = i;
                Thread t = new Thread(() => Calculator(threadId));
                t.Start();
            }
        }

        void Calculator(int threadId)
        {
            long sum = 0;
            long count=0;
            long step=2;
            do
            {
                sum+=step;
                count++;
            } while (!canStop);
            Console.WriteLine(threadId+" - " + sum + " - " + count);
        }

        private bool canStop = false;

        public bool CanStop { get => canStop; }

        public void Stoper()
        {
            Thread.Sleep(30 * 1000);
            canStop = true;
        }
    }
}
