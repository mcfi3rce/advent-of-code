# Learning Journey - Advent of Code 2025

A record of functional programming concepts learned while solving AOC problems.

## Overview

**Goal**: Learn functional programming through problem-solving, focusing on Clojure idioms and patterns.

**Approach**: Solve problems first, then understand the FP concepts that emerge naturally.

---

## Progress Summary

| Day | Status | Key Concepts Learned | Difficulty |
|-----|--------|---------------------|------------|
| 1   | ✅ Completed | Threading macros, reduce with state, modular arithmetic, lazy sequences | Medium |
| 2   | ✅ Completed | mapcat, filter composition, string manipulation, data pipelines | Easy |

---

## Day 1: Safe Dial Problem

### Problem Summary
Navigate a circular dial (0-99) and count how many times you land on or pass through zero.

### Key Learnings

#### 1. Threading Macros (`->>`)
**Breakthrough moment**: Understanding how to compose data transformations into readable pipelines.

```clojure
(->> input
     p/lines
     (map parse-line)
     (filter valid?)
     count)
```

**What clicked**: Each step transforms data and passes it to the next step. Read top to bottom like a recipe.

#### 2. Reduce with State
**Pattern**: Track multiple values as you iterate through a collection.

```clojure
(reduce (fn [state num]
          (let [new-position (mod (+ (:position state) num) 100)]
            {:position new-position
             :count (if (= new-position 0)
                     (inc (:count state))
                     (:count state))}))
        {:position 0 :count 0}
        parsed-input)
```

**Key insight**: State is a map, and you return a new map on each iteration. Immutability means you build up new state rather than mutating.

#### 3. Modular Arithmetic
**Pattern**: Wrapping around a circular range.

```clojure
(mod (+ position movement) 100)  ; Wraps 0-99
```

**Examples**:
- `(mod -1 100)` => `99` (wraps backward)
- `(mod 150 100)` => `50` (wraps forward)

#### 4. Lazy Sequences
**Pattern**: Generate sequences on-demand rather than all at once.

```clojure
(defn positions-between [from-pos movement]
  (if (pos? movement)
    (map #(mod % 100) (range (+ from-pos 1) (+ from-pos movement 1)))
    (map #(mod % 100) (range (+ from-pos -1) (+ from-pos movement -1) -1))))
```

**Key insight**: `range` and `map` don't compute everything upfront. They generate values as needed.

#### 5. Counting with Filter
**Pattern**: Find how many items match a condition.

```clojure
(->> (positions-between from-pos movement)
     (filter zero?)
     count)
```

**What I learned**: `zero?` is a predicate function, and predicates can be passed directly to `filter`.

### Struggles
- Initially confused about `reduce` argument order: `(reduce fn initial-value collection)`
- Had the initial value and collection in the wrong positions in the threading macro
- Took time to understand that `let` expressions return their last value

### Wins
- Successfully used lazy sequences to solve Part 2 efficiently
- Composed multiple functional concepts together
- Understood that threading macros make code more readable

---

## Day 2: Number Range Analysis

### Problem Summary
Given ranges of numbers, find numbers with even digit counts and equal halves.

### Key Learnings

#### 1. `mapcat` - Map and Flatten
**Pattern**: Transform each item into a collection, then flatten into one sequence.

```clojure
(->> parsed-input
     (mapcat (fn [[a b]] (range a (inc b)))))
```

**What I learned**:
- `mapcat` = `map` + `concat`
- Perfect for expanding ranges into individual numbers
- Watch out for wrapping results in vectors `[(range ...)]` vs just `(range ...)`

#### 2. Filter Composition
**Pattern**: Chain multiple filters for clear, readable code.

```clojure
(->> numbers
     (filter even-digit-count?)
     (filter equal-halves?)
     count)
```

**Key insight**: Two separate filters is MORE idiomatic than combining them! Benefits:
- Clear separation of concerns
- Each filter is independently testable
- Easy to add/remove filters
- More readable

#### 3. String Manipulation for Number Analysis
**Pattern**: Convert numbers to strings to analyze their structure.

```clojure
(defn even-digit-count?
  [n]
  (-> n str count even?))

(defn equal-halves?
  [n]
  (let [s (str n)
        mid (/ (count s) 2)]
    (= (subs s 0 mid)
       (subs s mid))))
```

**What I learned**:
- `str` converts anything to a string
- `subs` extracts substrings: `(subs "hello" 0 3)` => `"hel"`
- Don't convert back to numbers if you don't need to do math

#### 4. Parsing with `p/numbers`
**Pattern**: Extract all numbers from a string automatically.

```clojure
(->> input
     p/lines
     (map p/numbers))  ; Extracts all numbers from each line
```

**What I learned**: Built-in utilities can replace manual splitting. `p/numbers` is more robust than `str/split`.

### Struggles
- Initially wrapped `range` in a vector: `[(range a b)]` instead of just `(range a b)`
- Forgot that `mapcat` flattens, so extra wrapping broke things
- Had to reload namespace in REPL when getting "No such namespace" errors

### Wins
- Threading macros now make sense!
- Understood filter composition is idiomatic
- Successfully parsed and transformed data in a clean pipeline

---

## Functional Programming Concepts Mastered

### ✅ Threading Macros
- `->>` threads result as last argument
- Makes data transformations read top-to-bottom
- Essential for readable Clojure code

### ✅ Higher-Order Functions
- `map` - transform each element
- `filter` - keep elements matching predicate
- `reduce` - accumulate results with state
- `mapcat` - map and flatten

### ✅ Predicates
- Functions that return true/false
- Can be passed directly to `filter`, `some`, etc.
- Examples: `even?`, `zero?`, `pos?`

### ✅ Lazy Sequences
- `range`, `map`, `filter` are lazy
- Generate values on demand
- Efficient for large datasets

### ✅ Immutability
- Never mutate data structures
- Always return new values
- State updates mean creating new maps/vectors

---

## Common Patterns Reference

### Loop with Accumulator → `reduce`
```clojure
;; Build up a result while iterating
(reduce (fn [acc item]
          (update-or-compute acc item))
        initial-value
        collection)
```

### Transform Each Item → `map`
```clojure
;; Convert each element independently
(map transform-fn collection)
```

### Select Items → `filter`
```clojure
;; Keep only items matching condition
(filter predicate-fn collection)
```

### Flatten Nested Collections → `mapcat`
```clojure
;; Transform and flatten in one step
(mapcat (fn [item] (collection-of-results)) collection)
```

### Count Matches → `filter` + `count`
```clojure
(->> collection
     (filter predicate?)
     count)
```

### Find First Match → `some` or `first` + `filter`
```clojure
(some predicate? collection)  ; Returns first truthy result
(first (filter predicate? collection))  ; Returns first match
```

---

## Traditional Loop → Functional Equivalents

| Traditional Pattern | Functional Approach |
|-------------------|-------------------|
| Loop with accumulator | `reduce` |
| Loop with index | `map-indexed`, `keep-indexed` |
| Loop until condition | `take-while`, recursion |
| Early exit | `some`, `first` with `filter` |
| Nested loops | `for` comprehension |
| Loop building collection | `map`, `filter`, `reduce` |

---

## Tools & Utilities Learned

### Parsing (`aoc2025.parsing`)
- `p/lines` - Split into lines
- `p/numbers` - Extract all numbers from string
- `p/digits` - Get individual digits
- `p/grid` - Parse into 2D grid
- `p/blocks` - Split on blank lines

### Utils (`aoc2025.utils`)
- `u/neighbors` - Get adjacent coordinates
- `u/manhattan-distance` - Calculate distance
- Mathematical utilities: `gcd`, `lcm`, `abs`
- Range utilities: `range-overlap?`, `range-intersection`

### Core Functions
- `mod` - Modular arithmetic (wrapping)
- `subs` - Substring extraction
- `str` - Convert to string
- `parse-long` - String to number
- `inc` / `dec` - Increment/decrement
- `even?` / `odd?` - Check parity
- `zero?` / `pos?` / `neg?` - Number predicates

---

## REPL Skills Developed

### Loading Code
- `(require '[namespace :as alias] :reload)` - Load/reload namespace
- `C-c C-k` - Load entire buffer in Emacs
- `C-c C-c` - Evaluate form at point

### Testing
- `(function-name args)` - Call function directly
- `(doc function-name)` - View documentation
- `(source function-name)` - View source code
- `(dir namespace)` - List all functions in namespace

### Navigation
- `C-c C-z` - Jump between source and REPL
- `M-p` / `M-n` - Navigate REPL history

---

## Problem-Solving Approach Developed

### 1. Understand the Problem
- Read carefully (twice!)
- Work through sample by hand
- Identify inputs, outputs, edge cases

### 2. Plan Data Structures
- What represents the state?
- How will I access the data?
- What transformations are needed?

### 3. Break Down Transformations
```
Raw Input → Parsed Data → Intermediate Result → Final Answer
```

### 4. Build Incrementally
- Get parsing working first
- Test with sample input
- Build helper functions
- Compose into final solution

### 5. Test in REPL
- Test each function individually
- Verify intermediate results
- Build confidence step by step

---

## Challenges Still Ahead

### Not Yet Encountered
- Graph problems (BFS/DFS)
- Memoization for optimization
- Recursion with `recur`
- Cycle detection
- Complex state machines

### Areas to Improve
- Writing tests first (TDD)
- Extracting reusable patterns earlier
- Thinking functionally from the start (not after trying loops)

---

## Key Insights

### 💡 "Think in Transformations"
Instead of "how do I loop through this?", ask "what transformation does each element need?"

### 💡 "Compose Small Functions"
Better to have many small, named functions than one large anonymous function.

### 💡 "Data Pipelines are Recipes"
Threading macros read like cooking instructions: take input, do step 1, do step 2, etc.

### 💡 "State is Data"
Don't think "variables that change" - think "new data created from old data"

### 💡 "Two Filters > One Complex Filter"
Clarity and composability beat micro-optimization.

---

## Resources Used

- [DEVELOPMENT_GUIDE.md](DEVELOPMENT_GUIDE.md) - Functional problem-solving patterns
- [TESTING_GUIDE.md](TESTING_GUIDE.md) - Testing workflows
- `src/aoc2025/parsing.clj` - Parsing utilities reference
- `src/aoc2025/utils.clj` - Algorithm utilities reference
- [ClojureDocs](https://clojuredocs.org/) - Function examples

---

## Next Steps

- Continue with Day 3
- Watch for graph problems (will need BFS/DFS)
- Practice thinking functionally from the start
- Build up pattern recognition for common problem types
- Add more utilities to `parsing.clj` and `utils.clj` as patterns emerge

---

*Last updated after completing Day 2*
