using System;
using System.Threading;

namespace threaddemo
{
    class Program
    {
        private int threadsCount = 8;
        private static Random random = new Random();

        private bool[] canStop;
        private int[] startTimes;
        private int[] workTimes;

        static void Main(string[] args)
        {
            new Program().Start();
        }

        void Start()
        {
            canStop = new bool[threadsCount];
            startTimes = new int[threadsCount];
            workTimes = new int[threadsCount];
            for (int i = 0; i < threadsCount; i++)
            {
                workTimes[i] = random.Next(5000, 10001);
            }

            for (int i = 0; i < threadsCount; i++)
            {
                int id = i;

                startTimes[i] = Environment.TickCount;

                Thread t = new Thread(() => Calculator(id));
                t.Start();
            }
            Thread controller = new Thread(Controller);
            controller.Start();
        }
        void Controller()
        {
            bool allStopped = false;

            while (!allStopped)
            {
                allStopped = true;

                for (int i = 0; i < threadsCount; i++)
                {
                    if (!canStop[i])
                    {
                        allStopped = false;

                        int worked = Environment.TickCount - startTimes[i];

                        if (worked >= workTimes[i])
                        {
                            canStop[i] = true;
                        }
                    }
                }
            }
        }

        void Calculator(int id)
        {
            long sum = 0;
            long count = 0;
            long step = 2;

            while (!canStop[id])
            {
                sum += step;
                count++;
            }

            Console.WriteLine(
                $"Thread {id + 1}  sum={sum}  count={count}"
            );
        }
    }
}