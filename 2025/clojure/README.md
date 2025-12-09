# Advent of Code 2025

Clojure solutions for [Advent of Code 2025](https://adventofcode.com/2025)

## Setup

This project uses [Leiningen](https://leiningen.org/) for dependency management and building.

### Prerequisites

- Leiningen (already installed via Homebrew)
- Java/OpenJDK (installed as a dependency of Leiningen)
- Go (optional, for the day generator script)

### Java Setup

After installing Leiningen, you need to set up Java in your shell profile. Add this to your `~/.zshrc` (or `~/.bashrc`):

```bash
export JAVA_HOME=$(/opt/homebrew/bin/brew --prefix openjdk)
export PATH="$JAVA_HOME/bin:$PATH"
```

Then restart your terminal or run:
```bash
source ~/.zshrc
```

## Project Structure

```
AOC25/
├── src/aoc2025/
│   ├── parsing.clj        # Shared parsing utilities ⭐ NEW
│   ├── utils.clj          # Common algorithms & helpers ⭐ NEW
│   ├── day01.clj          # Day 1 solution
│   ├── day02.clj          # Day 2 solution
│   └── ...
├── test/aoc2025/
│   ├── parsing_test.clj   # Tests for parsing utilities ⭐ NEW
│   ├── utils_test.clj     # Tests for utility functions ⭐ NEW
│   ├── day01_test.clj     # Tests for day 1
│   └── ...
├── resources/             # Input files
│   ├── day01/
│   │   ├── sample.txt    # Sample input from puzzle description
│   │   └── input.txt     # Your actual puzzle input
│   └── ...
├── DEVELOPMENT_GUIDE.md   # Functional problem-solving approach ⭐ NEW
├── TESTING_GUIDE.md       # Testing patterns and workflows ⭐ NEW
└── project.clj           # Leiningen project configuration
```

## Creating a New Day

To generate the structure for a new day's challenge, you can use either:

**Option 1: Go script (recommended)**
```bash
go run new-day.go <day-number>
```

**Option 2: Direct lein command**
```bash
lein run -m aoc2025.new-day <day-number>
```

Example:
```bash
go run new-day.go 2
```

This will create:
- `src/aoc2025/day02.clj` - Solution template
- `test/aoc2025/day02_test.clj` - Test template with sample and input tests
- `resources/day02/sample.txt` - Placeholder for sample input
- `resources/day02/input.txt` - Placeholder for puzzle input

## Workflow

1. **Create a new day:**
   ```bash
   go run new-day.go 1
   ```

2. **Add inputs:**
   - Copy the sample input from the puzzle to `resources/day01/sample.txt`
   - Copy your puzzle input to `resources/day01/input.txt`

3. **Implement the solution:**
   - Edit `src/aoc2025/day01.clj`
   - Implement `parse-input`, `part1`, and `part2` functions

4. **Run tests:**

   Run all tests for a specific day:
   ```bash
   lein test aoc2025.day01-test
   ```

   Run a specific test (e.g., only sample for part 1):
   ```bash
   lein test :only aoc2025.day01-test/test-part1-sample
   ```

   Run all tests:
   ```bash
   lein test
   ```

5. **Run the solution:**
   ```bash
   lein run -m aoc2025.day01
   ```

## Testing Strategy

Each day has four separate tests:
- `test-part1-sample` - Part 1 with sample input (use this to verify logic)
- `test-part1-input` - Part 1 with actual puzzle input
- `test-part2-sample` - Part 2 with sample input (use this to verify logic)
- `test-part2-input` - Part 2 with actual puzzle input

You can run tests individually to focus on specific parts while developing.

## Shared Utilities

### parsing.clj - Input Parsing Helpers
Common patterns for transforming raw input strings:

```clojure
(require '[aoc2025.parsing :as p])

(p/lines input)                    ; Split into lines
(p/blocks input)                   ; Split on blank lines
(p/numbers "foo 123 bar 456")      ; Extract all numbers => (123 456)
(p/grid input)                     ; Parse into 2D grid
(p/words "foo  bar")               ; Split on whitespace
```

See `src/aoc2025/parsing.clj` for the complete list and examples.

### utils.clj - Algorithms & Data Structure Helpers
Common algorithms you'll need throughout AOC:

```clojure
(require '[aoc2025.utils :as u])

;; Grid navigation
(u/neighbors [1 1])                ; Get adjacent cells
(u/manhattan-distance [0 0] [3 4]) ; => 7

;; Collections
(u/find-first even? [1 3 4 5])     ; => 4

;; Math
(u/gcd 48 18)                      ; => 6
(u/lcm 4 6)                        ; => 12

;; Ranges (useful for interval problems)
(u/range-overlap? [1 5] [3 7])     ; => true
```

See `src/aoc2025/utils.clj` for the complete list. Many functions include TODO stubs for common AOC patterns (BFS, DFS, cycle detection) that you'll implement when you need them.

## Learning Resources

- **[DEVELOPMENT_GUIDE.md](DEVELOPMENT_GUIDE.md)** - How to approach problems functionally
  - Common AOC patterns
  - When to use map/filter/reduce
  - Functional thinking tips
  - What to do when you're stuck

- **[TESTING_GUIDE.md](TESTING_GUIDE.md)** - Testing patterns and workflows
  - How to run tests
  - Testing strategies
  - REPL-driven development

## Tips

- **Use your utilities!** Check `parsing.clj` and `utils.clj` before writing common patterns
- Start by solving with the sample input and update the expected values in tests
- Once the sample tests pass, run against the actual input
- Update test assertions with the correct answers once you solve each part
- Tests serve as regression prevention for refactoring
- **Think in pipelines**: Use `->>` to chain transformations
- **Extract functions**: Keep functions small and focused

## REPL Development

Start a REPL for interactive development:
```bash
lein repl
```

Then load your namespace:
```clojure
(require '[aoc2025.day01 :as day01] :reload)
(def sample (slurp "resources/day01/sample.txt"))
(day01/part1 (day01/parse-input sample))
```
