# Advent of Code Solver

This is a C# console application designed to solve Advent of Code challenges. It is intended to help provide an easily extensible way of adding new solutions each year and day as they challenges are posted.

## Getting Started
### Prerequisites

    .NET SDK installed on your machine.

### Installation

    Clone this repository to your local machine.
    Open a terminal and navigate to the project directory.

## Usage

The application supports two main commands:

#### Solve All Challenges:
- To solve all available challenges, use the following command:

```
dotnet run all
```
- This will execute the `SolveAll` method, which finds all solutions and outputs their results.

#### Solve Specific Challenge:
- To solve a specific challenge for a given year and day, use the following command:

```
dotnet run solve -year <year> -day <day>
```
- Replace `<year>` and `<day>` with the desired values.
- This will execute the `SolveDay` method, which finds and solves the specified challenge.

## Adding New Solutions

1. Create a new class for your solution, inheriting from the Solution class.
2. Apply the SolutionAttribute to the new class, specifying the problem name.
3. Implement the abstract methods, including the Solve method where you write the solution logic.


## Acknowledgments

Inspiration and challenges provided by Advent of Code.

https://adventofcode.com/