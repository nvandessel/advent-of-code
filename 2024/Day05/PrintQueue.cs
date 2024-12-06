using System.Collections.Generic;
using adventofcode.Core;

namespace adventofcode.Y2024.D05
{
    [SolutionAttribute("Print Queue")]
    public class PrintQueue : Solution
    {
        public override int Year => 2024;
        public override int Day => 05;

        public override void Solve()
        {
            var input = GetInput();
            var lines = input.Split("\n");

            var rules = ExtractRules(lines);
            var orders = ExtractOrders(lines);

            var partOne = CalculateValidOrders(rules, orders);
            OutputAnswer("Part One = " + partOne);

            var partTwo = CalculateInvalidOrders(rules, orders);
            OutputAnswer("Part Two = " + partTwo);
        }

        private int CalculateValidOrders(PrintRules rules, List<List<int>> orders)
        {
            var result = 0;

            foreach (var order in orders)
            {
                if (rules.IsOrderValid(order))
                {
                    result += order[order.Count / 2];
                }
                
            }

            return result;
        }

        private int CalculateInvalidOrders(PrintRules rules, List<List<int>> orders)
        {
            var result = 0;

            foreach (var order in orders)
            {
                if (!rules.IsOrderValid(order))
                {
                    var valid = rules.EnforceValidOrder(order);
                    result += valid[valid.Count / 2];
                }
                
            }

            return result;
        }

        private PrintRules ExtractRules(string[] lines)
        {
            var rules = new PrintRules();
            foreach (var line in lines)
            {
                if (line.Contains('|'))
                {
                    var rule = line.Split('|');
                    rules.AddRule(int.Parse(rule[0]), int.Parse(rule[1]));
                }
            }
            return rules;
        }

        private List<List<int>> ExtractOrders(string[] lines)
        {
            var orders = new List<List<int>>();
            foreach (var line in lines)
            {
                if (!line.Contains(','))
                {
                    continue;
                }

                var order = new List<int>();
                var pages = line.Split(',');
                foreach (var page in pages)
                {
                    order.Add(int.Parse(page));
                }
                orders.Add(order);
            }
            return orders;
        }
    }
}
