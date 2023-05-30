using System.Collections;

namespace Wahrscheinlichkeiten;

internal static class RangeExtensions
{
    public static IEnumerator<int> GetEnumerator(this Range r)
    {
        return new RangeEnumerator(r.Start.Value, r.End.Value);
    }

    private class RangeEnumerator : IEnumerator<int>
    {
        public RangeEnumerator(int min, int max)
        {
            _min = min;
            _max = max;
            _current = _min - 1;
        }
        
        private int _current;
        private readonly int _max;
        private readonly int _min;

        public int Current => _current;

        object IEnumerator.Current => _current;

        public void Dispose() 
        {
            _current = _max + 1;
        }

        public bool MoveNext()
        {
            _current++;
            return _current <= _max;
        }

        public void Reset()
        {
            _current = _min - 1;
        }
    }
}
