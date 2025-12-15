using System;
using System.CommandLine;
using System.Diagnostics;
using System.IO;
using System.Threading;
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

            var solveSampleCommand = new Command("solve-sample"){
                dayOption,
                yearOption
            };
            solveSampleCommand.SetHandler((day, year) => 
            {
                SolveSampleDay(year, day);
            }, dayOption, yearOption);
            rootCommand.AddCommand(solveSampleCommand);

            var watchCommand = new Command("watch"){
                dayOption,
                yearOption
            };
            watchCommand.SetHandler((day, year) => 
            {
                WatchSolution(year, day);
            }, dayOption, yearOption);
            rootCommand.AddCommand(watchCommand);

            var allCommand = new Command("all");
            allCommand.SetHandler(() => SolveAll());
            rootCommand.AddCommand(allCommand);

            Directory.SetCurrentDirectory(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));

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

        private static void SolveSampleDay(int year, int day)
        {
            var solution = FindSolution(year, day);
            if (solution == null)
            {
                Console.WriteLine($"No solution found for {year} Day {day}");
                return;
            }

            OutputFormatter.PrintWatchHeader(year, day);
            solution.SolveSample();
        }

        private static void WatchSolution(int year, int day)
        {
            var dayPath = Path.Combine(Directory.GetCurrentDirectory(), year.ToString(), $"Day{day:00}");
            
            if (!Directory.Exists(dayPath))
            {
                Console.WriteLine($"Error: Directory not found: {dayPath}");
                Console.WriteLine($"Run: ./add_solution.sh {year} {day} <SolutionName>");
                return;
            }

            // Initial run
            RunSampleTests(year, day);

            // Setup file watcher
            using var watcher = new FileSystemWatcher(dayPath);
            watcher.Filter = "*.cs";
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            
            Timer debounceTimer = null;
            
            FileSystemEventHandler changeHandler = (sender, e) =>
            {
                debounceTimer?.Dispose();
                debounceTimer = new Timer(_ =>
                {
                    RunSampleTests(year, day);
                }, null, 100, Timeout.Infinite);
            };

            watcher.Changed += changeHandler;
            watcher.Created += changeHandler;
            watcher.Renamed += (sender, e) => changeHandler(sender, e);

            watcher.EnableRaisingEvents = true;

            Console.WriteLine("\nPress 'r' to run with real input, Ctrl+C to stop watching...");
            
            // Keep running until Ctrl+C
            var resetEvent = new ManualResetEvent(false);
            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                resetEvent.Set();
            };

            // Listen for keypresses in a separate thread
            var keyThread = new Thread(() =>
            {
                while (!resetEvent.WaitOne(0))
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true);
                        if (key.KeyChar == 'r' || key.KeyChar == 'R')
                        {
                            Console.WriteLine("\n=== Running with REAL input ===\n");
                            RunRealInput(year, day);
                            Console.WriteLine("\nPress 'r' to run with real input, Ctrl+C to stop watching...");
                        }
                    }
                    Thread.Sleep(100);
                }
            });
            keyThread.Start();
            
            resetEvent.WaitOne();
            debounceTimer?.Dispose();
            keyThread.Join(1000);
        }

        private static void RunSampleTests(int year, int day)
        {
            OutputFormatter.PrintWatchHeader(year, day);

            // Run dotnet run solve-sample as a subprocess to rebuild and get fresh assembly
            var runProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"run solve-sample -y {year} -d {day}",
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    UseShellExecute = false,
                    CreateNoWindow = false
                }
            };

            try
            {
                runProcess.Start();
                runProcess.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error running tests: {ex.Message}");
            }
        }

        private static void RunRealInput(int year, int day)
        {
            // Run dotnet run solve as a subprocess to rebuild and get fresh assembly
            var runProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"run solve -y {year} -d {day}",
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    UseShellExecute = false,
                    CreateNoWindow = false
                }
            };

            try
            {
                runProcess.Start();
                runProcess.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error running real input: {ex.Message}");
            }
        }

        private static Solution FindSolution(int year, int day)
        {
            var solutions = SolutionFinder.FindSolutions();
            foreach (var solutionType in solutions)
            {
                var instance = (Solution)Activator.CreateInstance(solutionType);
                if (instance.Year == year && instance.Day == day)
                {
                    return instance;
                }
            }
            return null;
        }
    }
}
