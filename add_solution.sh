#!/bin/bash

# Script to add a new Advent of Code solution
# Usage: ./add_solution.sh <year> <day> <solution_name>

# Input parameters
year=$1
day_num=$2
solution_name=$3

if [ -z "$year" ] || [ -z "$day_num" ] || [ -z "$solution_name" ]; then
    echo "Usage: ./add_solution.sh <year> <day> <solution_name>"
    echo "Example: ./add_solution.sh 2025 1 SecretEntrance"
    exit 1
fi

# Enforce zero-padding for the day
day=$(printf "%02d" $day_num)

# Derive solution title from class name
solution_title=$(echo $solution_name | sed -E 's/([a-z0-9])([A-Z])/\1 \2/g')

# Create directories
mkdir -p "$year/Day$day"

# Create Solution class file
solution_file="$year/Day$day/${solution_name}.cs"
cat <<EOL >"$solution_file"
using System;
using adventofcode.Core;

namespace adventofcode.Y${year}.D${day}
{
    [SolutionAttribute("${solution_title}")]
    public class ${solution_name} : Solution
    {
        public override int Year => ${year};
        public override int Day => ${day_num};

        public override SolutionResult Execute(string input)
        {
            // Parse input
            var lines = input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
            
            // TODO: Implement solution logic here
            
            return new SolutionResult
            {
                Part1 = "",
                Part2 = ""
            };
        }
    }
}
EOL

echo "✓ Created $solution_file"
echo ""

# Interactive prompts
read -p "Would you like to add inputs now? (y/n): " add_inputs

if [[ "$add_inputs" == "y" || "$add_inputs" == "Y" ]]; then
    # Sample Input
    echo ""
    echo "=== Sample Input ==="
    echo "Paste sample input (Ctrl+D when done):"
    cat > "$year/Day$day/SampleInput.txt"
    echo "✓ Saved to SampleInput.txt"
    
    # Expected Answers
    echo ""
    echo "=== Expected Answers ==="
    read -p "Part 1 answer (leave blank if unknown): " part1_answer
    read -p "Part 2 answer (leave blank if unknown): " part2_answer
    
    cat <<EOL >"$year/Day$day/Expected.json"
{
  "part1": "$part1_answer",
  "part2": "$part2_answer"
}
EOL
    echo "✓ Saved to Expected.json"
    
    # Real Input
    echo ""
    echo "=== Real Input ==="
    echo "Paste real input (Ctrl+D when done):"
    cat > "$year/Day$day/Input.txt"
    echo "✓ Saved to Input.txt"
    
    echo ""
    echo "=== All Set! ==="
    echo "Start coding with:"
    echo "  ./watch-solution.sh $year $day_num"
else
    # Create empty/template files
    touch "$year/Day$day/SampleInput.txt"
    touch "$year/Day$day/Input.txt"
    cat <<EOL >"$year/Day$day/Expected.json"
{
  "part1": "",
  "part2": ""
}
EOL
    echo "✓ Created empty input files"
    echo ""
    echo "Start coding with:"
    echo "  ./watch-solution.sh $year $day_num"
fi

echo ""
echo "Happy coding! 🎄"
