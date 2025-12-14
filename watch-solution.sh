#!/bin/bash
# Usage: ./watch-solution.sh <year> <day>

if [ -z "$1" ] || [ -z "$2" ]; then
    echo "Usage: ./watch-solution.sh <year> <day>"
    echo "Example: ./watch-solution.sh 2025 1"
    exit 1
fi

YEAR=$1
DAY_NUM=$2
DAY=$(printf "%02d" $DAY_NUM)

# Validate directory exists
if [ ! -d "${YEAR}/Day${DAY}" ]; then
    echo "Error: ${YEAR}/Day${DAY} does not exist"
    echo "Run: ./add_solution.sh $YEAR $DAY_NUM <SolutionName>"
    exit 1
fi

# Find solution .cs file (get first one)
SOLUTION_FILE=$(find "${YEAR}/Day${DAY}" -maxdepth 1 -name "*.cs" | head -1)

if [ -z "$SOLUTION_FILE" ]; then
    echo "Error: No .cs files found in ${YEAR}/Day${DAY}"
    exit 1
fi

# Check if already inside tmux
if [ -n "$TMUX" ]; then
    echo "📍 You're already in a tmux session!"
    echo ""
    echo "Option 1: Create split in current window"
    echo "  tmux split-window -v -l 30% 'dotnet run watch -y $YEAR -d $DAY_NUM'"
    echo "  nvim $SOLUTION_FILE"
    echo ""
    echo "Option 2: Create new window"
    echo "  tmux new-window -n \"aoc-${YEAR}-${DAY}\""
    echo "  tmux split-window -v -l 30% 'dotnet run watch -y $YEAR -d $DAY_NUM'"
    echo "  nvim $SOLUTION_FILE"
    echo ""
    echo "Option 3: Just start watch mode in current pane"
    echo "  dotnet run watch -y $YEAR -d $DAY_NUM"
    echo ""
    read -p "Choose option (1/2/3) or 'q' to quit: " choice
    
    case "$choice" in
        1)
            # Split current window
            tmux split-window -v -l 30% "dotnet run watch -y $YEAR -d $DAY_NUM"
            nvim "$SOLUTION_FILE"
            ;;
        2)
            # Create new window
            tmux new-window -n "aoc-${YEAR}-${DAY}"
            tmux split-window -v -l 30% "dotnet run watch -y $YEAR -d $DAY_NUM"
            tmux select-pane -t 0
            tmux send-keys "nvim $SOLUTION_FILE" C-m
            ;;
        3)
            # Just run watch mode
            dotnet run watch -y $YEAR -d $DAY_NUM
            ;;
        *)
            echo "Cancelled."
            exit 0
            ;;
    esac
    exit 0
fi

# Not in tmux - create new session
SESSION_NAME="aoc-${YEAR}-${DAY}"

# Check if session already exists
if tmux has-session -t "$SESSION_NAME" 2>/dev/null; then
    echo "Session $SESSION_NAME already exists. Attaching..."
    tmux attach-session -t "$SESSION_NAME"
    exit 0
fi

# Create new session
tmux new-session -d -s "$SESSION_NAME"

# Split horizontally (top = editor, bottom = watcher)
tmux split-window -v -t "$SESSION_NAME"

# Resize bottom pane to ~30% of height
tmux resize-pane -t "$SESSION_NAME:0.1" -y 15

# Top pane: neovim
tmux send-keys -t "$SESSION_NAME:0.0" "nvim $SOLUTION_FILE" C-m

# Bottom pane: watch mode
tmux send-keys -t "$SESSION_NAME:0.1" "dotnet run watch -y $YEAR -d $DAY_NUM" C-m

# Focus top pane (editor)
tmux select-pane -t "$SESSION_NAME:0.0"

# Attach
tmux attach-session -t "$SESSION_NAME"
