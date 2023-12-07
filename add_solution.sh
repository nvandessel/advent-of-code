#!/bin/bash

# Script to add a new Advent of Code solution
# Usage: ./add_solution.sh <year> <day> <solution_name>

# Input parameters
year=$1
day=$2
solution_name=$3

# Enforce zero-padding for the day
day=$(printf "%02d" $day)

# Derive solution title from class name
solution_title=$(echo $solution_name | sed -E 's/([a-z0-9])([A-Z])/\1 \2/g')

# Create directories
mkdir -p "$year/Day$day"

# Create Input.txt file
touch "$year/Day$day/Input.txt"

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
        public override int Day => ${day};

        public override void Solve()
        {
            // Implement solution logic here
            throw new NotImplementedException();
        }
    }
}
EOL

echo "Solution added: $solution_file"
