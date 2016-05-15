using System.Collections.Generic;

namespace plitt.fibonacci
{
    public class FibonacciGenerator
    {
        private Dictionary<int, long> _dict;

        public FibonacciGenerator()
        {
            _dict = new Dictionary<int, long>();
            _dict.Add(0, 0);
            _dict.Add(1, 1);
        }

        public long GetFibonaci(int n)
        {
            if (_dict.ContainsKey(n))
                return _dict[n];

            long value = GetFibonaci(n - 1) + GetFibonaci(n - 2);
            _dict.Add(n, value);
            return value;
        }

    }
}
