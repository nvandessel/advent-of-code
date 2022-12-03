using System;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using adventofcode.Core;

namespace adventofcode
{
    class Program
    {
        static int Main(string[] args)
        {
            var rootCommand = new RootCommand("Outputs answers for Advent of Code challenges.");

            var yearOption = new Option<int>(
                new string[] { "-year", "-y"},
                "Will output results for all Solutions for the supplied Year.") { IsRequired = true } ;
            var dayOption = new Option<int>(
                new string[] { "-day", "-d"},
                "Will output results for all Solutions for the supplied Day.") { IsRequired = true } ;

            var dayYearCommand = new Command("solve"){
                dayOption,
                yearOption
            };
            dayYearCommand.SetHandler((day, year) => 
            {
                SolveDay(year, day);
            }, dayOption, yearOption);
            rootCommand.AddCommand(dayYearCommand);


            var allCommand = new Command("all");
            allCommand.SetHandler(() => SolveAll());
            rootCommand.AddCommand(allCommand);


            return rootCommand.InvokeAsync(args).Result;
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

        private static void SolveDay(int year, int day)
        {
            var solutions = SolutionFinder.FindSolutions();
            foreach (var solution in solutions)
            {
                var instance = (Solution)Activator.CreateInstance(solution);
                if (instance.Year != year || instance.Day != day)
                {
                    continue;
                }
                instance.Solve();
            }
        }
    }
}
