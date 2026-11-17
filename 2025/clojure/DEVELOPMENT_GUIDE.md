# Development Guide for Advent of Code 2025

## Problem-Solving Approach

### 1. Read and Understand
- Read the problem carefully (twice!)
- Identify inputs, outputs, and constraints
- Work through the sample by hand to understand the logic
- Note any edge cases mentioned

### 2. Plan Your Data Structures
Ask yourself:
- **What represents the state?** (vector, map, set, string?)
- **How will I access the data?** (by index, by key, by searching?)
- **Will I need coordinates?** (use `[row col]` vectors for grids)
- **Is order important?** (list for sequential, set for membership, map for lookup)

### 3. Break Down the Problem
Identify the transformations:
```
Raw Input → Parsed Data → Intermediate Result → Final Answer
```

Think in terms of function composition:
```clojure
(->> input
     parse-input        ; String → Data structure
     transform-data     ; Apply problem logic
     calculate-answer)  ; Compute final result
```

### 4. Start Simple
- Get parsing working first
- Test with the sample input
- Build helper functions for complex logic
- Compose helpers into the full solution

## Common AOC Patterns

### Pattern 1: Line-by-Line Processing
```clojure
(defn solve [input]
  (->> input
       p/lines                    ; Split into lines
       (map parse-line)           ; Parse each line
       (map process-line)         ; Transform each line
       (reduce combine-results))) ; Aggregate results
```

### Pattern 2: Grid Problems
```clojure
(defn solve [input]
  (let [grid (p/grid input)
        rows (count grid)
        cols (count (first grid))]
    ; Use grid navigation from utils.clj
    ; Find specific cells with p/find-in-grid
    ; Get neighbors with u/neighbors
    ))
```

### Pattern 3: State Simulation
```clojure
(defn solve [input]
  (let [initial-state (parse-input input)]
    (loop [state initial-state
           step 0]
      (if (goal-reached? state)
        step
        (recur (simulate-step state) (inc step))))))
```

### Pattern 4: Counting/Frequency
```clojure
(defn solve [input]
  (->> input
       p/lines
       (map parse-line)
       frequencies          ; Count occurrences
       (filter matches?)    ; Filter by criteria
       count))              ; Or (map val) (reduce +)
```

## Choosing the Right Functions

### When to use map
"I need to **transform each element** independently"
```clojure
(map parse-long ["1" "2" "3"]) ; => (1 2 3)
(map inc [1 2 3])              ; => (2 3 4)
```

### When to use filter
"I need to **keep only elements that match** a condition"
```clojure
(filter even? [1 2 3 4])       ; => (2 4)
(filter #(> % 10) [5 15 8 20]) ; => (15 20)
```

### When to use reduce
"I need to **combine all elements** into a single result"
```clojure
(reduce + [1 2 3 4])           ; => 10 (sum)
(reduce max [3 7 2 9])         ; => 9 (maximum)
(reduce conj #{} [1 2 2 3])    ; => #{1 2 3} (build set)
```

### When to use keep
"I need to **transform and filter** in one step (remove nils)"
```clojure
(keep parse-long ["1" "x" "2"]) ; => (1 2)
(keep #(when (even? %) (* 2 %)) [1 2 3 4]) ; => (4 8)
```

### When to use group-by
"I need to **organize elements into categories**"
```clojure
(group-by even? [1 2 3 4])     ; => {false [1 3], true [2 4]}
(group-by :type data)          ; Group maps by :type key
```

## Functional Thinking Tips

### 1. Think in Pipelines
Instead of:
```clojure
(let [lines (p/lines input)
      parsed (map parse-line lines)
      filtered (filter valid? parsed)
      result (reduce + filtered)]
  result)
```

Write:
```clojure
(->> input
     p/lines
     (map parse-line)
     (filter valid?)
     (reduce +))
```

### 2. Extract Small Functions
Instead of:
```clojure
(map (fn [line]
       (let [[a b] (str/split line #" ")]
         (abs (- (parse-long a) (parse-long b)))))
     lines)
```

Write:
```clojure
(defn parse-pair [line]
  (map parse-long (str/split line #" ")))

(defn pair-distance [[a b]]
  (abs (- a b)))

(map (comp pair-distance parse-pair) lines)
```

### 3. Use Destructuring
Instead of:
```clojure
(let [pair (parse-pair line)
      first-num (first pair)
      second-num (second pair)]
  (+ first-num second-num))
```

Write:
```clojure
(let [[first-num second-num] (parse-pair line)]
  (+ first-num second-num))
```

### 4. Leverage Collections
Instead of:
```clojure
(if (or (= x 1) (= x 2) (= x 3))
  :small
  :large)
```

Write:
```clojure
(if (#{1 2 3} x)  ; Sets are functions!
  :small
  :large)
```

## When You Get Stuck

### 1. Simplify
- Test with a smaller input
- Print intermediate results with `println`
- Break the problem into smaller pieces

### 2. REPL It
```clojure
; Load your namespace
(require '[aoc2025.day01 :as d] :reload)

; Test individual functions
(d/parse-input sample-input)
(d/helper-function test-data)

; Try different approaches interactively
(map inc [1 2 3])
(filter even? [1 2 3 4])
```

### 3. Check Your Utilities
Did you already write a helper that solves this?
- `parsing.clj` - Parsing patterns
- `utils.clj` - Common algorithms

### 4. Recognize the Pattern
- Is this a graph problem? (BFS/DFS)
- Is this about finding patterns? (cycle detection)
- Is this about optimization? (memoization, dynamic programming)
- Is this about coordinates? (grid navigation)

## Performance Considerations

### Start Simple
1. Get it working first
2. Only optimize if too slow
3. Most problems are fine with naive solutions

### Common Optimizations
- **Memoization**: Cache expensive function calls with `memoize`
- **Lazy sequences**: Use `map`/`filter` instead of `mapv`/`filterv` when possible
- **Set lookups**: O(1) instead of O(n) with `some`
- **Reduce early**: Don't create intermediate collections if you don't need them

### When Part 1 Works but Part 2 is Slow
Part 2 usually requires:
- Memoization (if recursive)
- Better algorithm (if brute force)
- Mathematical insight (if simulation)
- Cycle detection (if repeated states)

## Daily Workflow

1. **Read problem** on adventofcode.com
2. **Save sample input** to `resources/dayXX/sample.txt`
3. **Update test** with expected sample answer
4. **Implement parse-input** and test it
5. **Implement part1** incrementally
6. **Run test** until it passes
7. **Get puzzle input** and save to `resources/dayXX/input.txt`
8. **Run solution** and submit answer
9. **Update test** with correct answer
10. **Read part 2** and repeat

## Resources

- [Clojure Cheatsheet](https://clojure.org/api/cheatsheet)
- [ClojureDocs](https://clojuredocs.org/) - Examples for every function
- Your `parsing.clj` and `utils.clj` - Use them!
- Previous days' solutions - What patterns worked?

## Remember

- **You're learning two things**: Problem solving AND functional programming
- **It's okay to struggle** - AOC problems are meant to be challenging
- **Refactor after solving** - First make it work, then make it pretty
- **Ask for hints** - "I'm stuck on the transformation from X to Y"
- **Focus on the process** - The goal is learning, not just stars
