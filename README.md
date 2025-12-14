# Advent of Code Solver

This repository contains my solutions for the Advent of Code challenges. Each year and day have their respective directories with C# solution classes. The solutions are organized using the following structure:
```
/Year
    /Day01
        - Input.txt          # Real puzzle input
        - SampleInput.txt    # Sample from problem description
        - Expected.json      # Expected answers for sample input
        - SolutionClass01.cs
    /Day02
        - Input.txt
        - SampleInput.txt
        - Expected.json
        - SolutionClass02.cs
  ...
```

## Quick Start

### Create a New Solution

Run the interactive setup script:
```bash
./add_solution.sh 2025 1 SecretEntrance
```

You'll be prompted to paste:
- Sample input from the problem
- Expected answers (Part 1 & Part 2)
- Real puzzle input

### Start Coding with Live Feedback

Launch watch mode:
```bash
./watch-solution.sh 2025 1
```

This opens a tmux session with:
- **Top pane**: Your editor (neovim) with the solution file
- **Bottom pane**: Auto-running tests that show results every time you save

**Note**: If you're already in tmux, the script will offer you options:
- Option 1: Split current window
- Option 2: Create new window
- Option 3: Just run watch mode

Every time you save your code, you'll instantly see:
```
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
🎄 Advent of Code 2025 Day 1
   Watching for changes...
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

✓ Part 1: 142 ✓ (12ms)
✓ Part 2: 281 ✓ (8ms)

Last run: 14:23:45
```

### Submit Your Answer

Once sample tests pass, get your real answers:

1. In the bottom pane: Press `Ctrl+C` to stop watching
2. Run: `dotnet run solve -y 2025 -d 1`
3. Copy the output to Advent of Code website

## Commands Reference

### Interactive Solution Setup
```bash
./add_solution.sh <year> <day> <solution_name>
```
Creates solution files and prompts for inputs.

### Watch Mode (Auto-testing)
```bash
./watch-solution.sh <year> <day>
```
Opens tmux with live feedback on sample input.

### Test Sample Input
```bash
dotnet run solve-sample -y <year> -d <day>
```
One-shot test against sample input with validation.

### Solve Real Input
```bash
dotnet run solve -y <year> -d <day>
```
Run solution against real puzzle input.

### Solve All Challenges
```bash
dotnet run all
```
Run all solutions in the repository.

## Solution Structure

Solutions implement the `Execute()` method:
```csharp
public override SolutionResult Execute(string input)
{
    // Parse input
    var lines = input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
    
    // Your solution logic here
    
    return new SolutionResult
    {
        Part1 = "answer1",
        Part2 = "answer2"
    };
}
```

## Expected.json Format

```json
{
  "part1": "142",
  "part2": "281"
}
```

Leave values empty (`""`) if you don't know the answer yet:
```json
{
  "part1": "142",
  "part2": ""
}
```

## Testing the System

The `9999/` directory contains test solutions for validating the watch mode system:

```bash
# Test sample validation
dotnet run solve-sample -y 9999 -d 1

# Test watch mode
./watch-solution.sh 9999 1
```

See `9999/README.md` for more details.

## Tips & Tricks

### Already in tmux?
If you're already in a tmux session (common workflow), `./watch-solution.sh` will offer options:
- **Option 1** (recommended): Splits current window - quick and keeps context
- **Option 2**: Creates new window - preserves your layout
- **Option 3**: Just runs watch mode - you open editor manually

### Quick Manual Setup
If you prefer to set up manually:
```bash
# In one pane/terminal:
dotnet run watch -y 2025 -d 1

# In another pane/terminal:
nvim 2025/Day01/YourSolution.cs
```

## Acknowledgments

Inspiration and challenges provided by Advent of Code.

https://adventofcode.com/
