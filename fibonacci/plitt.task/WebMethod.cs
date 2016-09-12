using System;
using System.Threading;
using System.Threading.Tasks;

namespace plitt.task
{
    public class WebMethod : IWebMethod
    {
        private Action _callback;
        private int _iterations;

        public void AsyncStart(int iterations, Action callback)
        {
            _callback = callback;
            _iterations = iterations;
            var t = new Task(DoSomeWork);
            t.Start();
        }

        private void DoSomeWork()
        {
            for (int i = 0; i < _iterations; i++)
            {
                Thread.Sleep(1);
            }
            _callback();
        }
    }
}
