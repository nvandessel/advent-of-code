using System;
using System.Collections.Generic;

namespace adventofcode.Y2024.D05
{
    public class PrintRules
    {
        private Dictionary<int, HashSet<int>> _lookup = new ();

        public void AddRule(int x, int y)
        {
            if (_lookup.ContainsKey(x))
            {
                _lookup[x].Add(y);
            }
            else
            {
                _lookup.Add(x, new HashSet<int>(){y});
            }
        }

        public bool IsOrderValid(List<int> order)
        {
            var illegalPageNumbers = new List<int>();
            for (int i = order.Count - 1; i >= 0; i--)
            {
                if (illegalPageNumbers.Contains(order[i]))
                {
                    return false;
                }
                if (_lookup.ContainsKey(order[i]))
                {
                    illegalPageNumbers.AddRange(_lookup[order[i]]);
                }
            }
            return true;
        }

        public List<int> EnforceValidOrder(List<int> order)
        {
            // Using Topological sorting
            var inDegree = new Dictionary<int, int>();
            foreach (var page in order)
            {
                if (!inDegree.ContainsKey(page))
                {
                    inDegree.Add(page, 0);
                }
                
                if (!_lookup.ContainsKey(page))
                {
                    continue;
                }

                foreach (var dependentPage in _lookup[page])
                {
                    if (!inDegree.ContainsKey(dependentPage))
                    {
                        inDegree.Add(dependentPage, 0);
                    }
                    inDegree[dependentPage]++;
                }
            }

            // Queue pages with no dependencies
            var queue = new Queue<int>();
            foreach (var page in order)
            {
                if (inDegree[page] == 0)
                {
                    queue.Enqueue(page);
                }
            }

            // Start sorting
            var sorted = new List<int>();
            while (queue.Count > 0)
            {
                var page = queue.Dequeue();
                sorted.Add(page);

               if (_lookup.ContainsKey(page))
               {
                   foreach (var dependentPage in _lookup[page])
                   {
                       inDegree[dependentPage]--;
                       if (inDegree[dependentPage] == 0 && order.Contains(dependentPage))
                       {
                           queue.Enqueue(dependentPage);
                       }
                       
                   }
               } 
            }

            if (sorted.Count != order.Count)
            {
                throw new System.Exception($"Sort has failed! Sorted = {sorted.Count} : Order = {order.Count}");
            }
            return sorted;
        }
    }
}
