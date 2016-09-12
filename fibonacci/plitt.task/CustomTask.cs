using System;
using System.Threading;
using System.Threading.Tasks;

namespace plitt.task
{
    public class CustomTask
    {
        public bool Called;
        public bool TimedOut;
        private Task _task;
        private CancellationToken _token;
        private CancellationTokenSource _tokenSource;
        private int _timeLimit;

        public CustomTask()
        {
            Called = false;
            TimedOut = false;
        }

        private void DoSomeWork(int iterations, CancellationToken ct)
        {
            // Was cancellation already requested? 
            if (ct.IsCancellationRequested)
            {
                Console.WriteLine("Task was cancelled before it got started.");
                ct.ThrowIfCancellationRequested();
                return;
            }

            var webMethod = new WebMethod();
            webMethod.AsyncStart(iterations, Callback);

            var i = 1;
            while (true)
            {
                if (ct.IsCancellationRequested)
                {
                    Console.WriteLine("Task cancelled after {0} iterations", i + 1);
                    ct.ThrowIfCancellationRequested();
                    break;
                }

                if (Called)
                    break;

                Thread.Sleep(1);

                if (i > _timeLimit)
                {
                    Console.WriteLine("Timed out.");
                    TimedOut = true;
                    break;
                }

                i++;
            }

        }

        private void Callback()
        {
            Console.WriteLine("Callback called.");
            Called = true;
        }

        public void Start(int iterations, int timeLimit)
        {
            _timeLimit = timeLimit;
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            _task = new Task(() => DoSomeWork(iterations, _token), _token);
            _task.Start();
        }

        public void Cancel()
        {
            _tokenSource.Cancel();
            Console.WriteLine("Task cancellation requested.");
        }
    }
}