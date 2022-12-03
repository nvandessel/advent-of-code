using System;
using adventofcode.Core;

namespace adventofcode
{
    class Program
    {
        public const string ARGS_ALL = "-a";
        public const string ARGS_YEAR = "-y";
        public const string ARGS_DAY = "-d";

        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                //TODO: Send greet message
                SolveAll();
            }
            else 
            {
                ExtractArgs(args);
            }
        }

        private static void SolveAll()
        {
            var solutions = SolutionFinder.FindSolutions();
            foreach (var solution in solutions)
            {
                var attributeData = solution.GetCustomAttributesData();
                var customAttributes = solution.GetCustomAttributes(false);
                foreach (var attribute in customAttributes)
                {
                    if (attribute is SolutionAttribute solutionAttribute)
                    {
                        Console.WriteLine($"Answers for {solutionAttribute.ProblemName}");
                    }
                }

                var instance = (Solution)Activator.CreateInstance(solution);
                instance.Solve();
            }
        }

        private static void SolveYear(int year)
        {
            
        }

        private static void SolveDay(int year, int day)
        {

        }

        private static void ExtractArgs(string[] args)
        {
            
        }
    }
}
