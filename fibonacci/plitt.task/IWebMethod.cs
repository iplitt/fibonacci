using System;

namespace plitt.task
{
    public interface IWebMethod
    {
        void AsyncStart(int iterations, Action callback);
    }
}
