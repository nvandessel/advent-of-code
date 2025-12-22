using System.Collections.Generic;
using System.Linq;

namespace adventofcode.Y2025.D06
{
    public class Problem(char op, params long[] values)
    {
        private readonly List<long> _values = values.ToList();

        public long Calculate()
        {
            var result = op == '*' ? 1L : 0L;
            foreach (var value in _values)
            {
                result = op switch
                {
                    '+' => result + value,
                    '*' => result * value,
                    _ => result
                };
            }
            return result;
        }
    }
    
    public class ProblemBuilder
    {
        private readonly List<long> _values = [];
        private char _operator = '?';

        public void AddValue(long value) => _values.Add(value);
        public void SetOperator(char op) => _operator = op;
        public Problem Build() => new(_operator, _values.ToArray());
    }
}
