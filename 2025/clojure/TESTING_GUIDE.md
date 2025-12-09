# Testing Guide for Advent of Code 2025

## Running Tests

```bash
# Run all tests
lein test

# Run tests for a specific namespace
lein test aoc2025.day01-test

# Run a specific test
lein test :only aoc2025.day01-test/test-part1-sample

# Run tests and watch for changes (requires lein-test-refresh plugin)
lein test-refresh
```

## Test Structure

Each day should have tests for:
1. Sample input (provided in problem description)
2. Actual puzzle input
3. Individual helper functions (optional but recommended)

## Example Test Workflow

1. **Read the problem** and save sample input to `resources/day01/sample.txt`
2. **Write a test for the sample** with the expected answer
3. **Implement your solution** until the sample test passes
4. **Run against puzzle input** and submit your answer
5. **Update the test** with the correct answer for future verification

## Testing Patterns

### Testing Parse Functions

```clojure
(deftest test-parse-input
  (testing "Parse simple input"
    (let [input "1 2 3\n4 5 6"]
      (is (= [[1 2 3] [4 5 6]] (parse-input input))))))
```

### Testing with Sample Data

```clojure
(def sample-input "3   4\n4   3\n2   5\n1   3\n3   9\n3   3")

(deftest test-part1-sample
  (testing "Part 1 with sample input"
    (is (= 11 (part1 (parse-input sample-input))))))
```

### Testing Helper Functions in Isolation

```clojure
(deftest test-helper-function
  (testing "Calculate distance"
    (is (= 5 (calculate-distance 3 8)))
    (is (= 0 (calculate-distance 5 5)))))
```

### Using `are` for Multiple Test Cases

```clojure
(deftest test-multiple-cases
  (testing "Multiple inputs"
    (are [input expected] (= expected (my-function input))
      [1 2] 3
      [5 5] 10
      [0 0] 0)))
```

## Debugging Tips

1. **Print intermediate values** using `println` or `prn`
2. **Use `tap>`** for non-intrusive debugging (requires tap listener)
3. **REPL-driven development**: Load your namespace and test functions interactively
4. **Isolate failures**: Write tests for individual helper functions

## REPL Workflow

```clojure
; In your REPL
(require '[aoc2025.day01 :as day01] :reload)

; Test with sample data
(def sample "3   4\n4   3\n2   5")
(def parsed (day01/parse-input sample))

; Test individual functions
(day01/part1 parsed)

; When you make changes, reload
(require '[aoc2025.day01 :as day01] :reload-all)
```

## Common Assertions

```clojure
(is (= expected actual))              ; Equality
(is (not= wrong actual))              ; Inequality
(is (< x y))                          ; Comparison
(is (nil? result))                    ; Nil check
(is (contains? #{:a :b} :a))         ; Set membership
(is (every? even? [2 4 6]))          ; All elements match
(is (some even? [1 2 3]))            ; At least one matches
(is (thrown? Exception (risky-fn)))  ; Exception thrown
```

## Performance Testing

For problems with large inputs or slow algorithms:

```clojure
(deftest test-performance
  (testing "Solution completes in reasonable time"
    (let [start (System/nanoTime)
          result (part1 puzzle-input)
          end (System/nanoTime)
          elapsed-ms (/ (- end start) 1000000.0)]
      (is (< elapsed-ms 1000)) ; Should complete in under 1 second
      (is (some? result)))))
```

## Tips for AOC Testing

1. **Always test with sample input first** - it's smaller and easier to debug
2. **Don't hardcode answers initially** - use `nil` and update once you know it's correct
3. **Keep tests after solving** - helps prevent regressions if you refactor
4. **Test edge cases** - empty input, single element, maximum values
5. **Comment your test inputs** - explain what makes each test case interesting
