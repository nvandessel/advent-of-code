# Test Solutions (Year 9999)

This directory contains test/example solutions used to validate the watch mode system.

Year **9999** is used to clearly indicate these are test cases, not actual Advent of Code solutions.

## Day01 - Test Solution

A simple test case that counts lines and characters from the input.

### Running the tests:

**Test sample input with validation:**
```bash
dotnet run solve-sample -y 9999 -d 1
```

Expected output:
```
✓ Part 1: 3 ✓ (0ms)
✓ Part 2: 20 ✓ (0ms)
```

**Test real input:**
```bash
dotnet run solve -y 9999 -d 1
```

**Test watch mode:**
```bash
./watch-solution.sh 9999 1
```

This will open a tmux session where you can edit the solution and see live feedback.

**Note**: If you're already in tmux, the script will offer smart options:
- Option 1: Split current window (quick!)
- Option 2: Create new window
- Option 3: Just run watch mode

## Purpose

These test cases ensure:
- ✅ File watching and auto-rebuild works
- ✅ Sample input validation works
- ✅ Expected.json loading works
- ✅ Color-coded output displays correctly
- ✅ Exception handling works
- ✅ Real input solving works
- ✅ Tmux integration works

Feel free to modify TestSolution.cs to experiment with the system!
