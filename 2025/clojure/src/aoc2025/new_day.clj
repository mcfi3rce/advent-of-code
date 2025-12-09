(ns aoc2025.new-day
  (:require [clojure.java.io :as io]
            [clojure.string :as str]))

(defn source-template [day-str]
  (str "(ns aoc2025.day" day-str "
  (:require [clojure.string :as str]
            [aoc2025.parsing :as p]
            [aoc2025.utils :as u]))

;; ============================================================================
;; Day " day-str ": [PUZZLE TITLE]
;; ============================================================================

;; Problem Summary:
;; TODO: Write a brief summary of what the problem asks you to do
;;
;; Approach:
;; TODO: Describe your approach at a high level
;; - What data structure represents the input?
;; - What transformations are needed?
;; - What algorithm or pattern fits this problem?

;; ============================================================================
;; Parsing
;; ============================================================================

(defn parse-input
  \"Parse the input string into a data structure.

   Think about:
   - What structure best represents this data?
   - Do you need lines, blocks, or the whole string?
   - Are there numbers to extract? Use p/numbers
   - Is it a grid? Use p/grid or p/grid-with-coords
   - Key-value pairs? Use p/parse-key-value

   Available parsing helpers in aoc2025.parsing:
   - p/lines, p/blocks, p/numbers, p/digits
   - p/grid, p/grid-with-coords
   - p/words, p/comma-separated
   - See parsing.clj for full list\"
  [input]
  ;; TODO: Implement parsing logic
  input)

;; ============================================================================
;; Helper Functions
;; ============================================================================

;; TODO: Write helper functions for the problem logic
;;
;; Tips:
;; - Keep each function small and focused on one task
;; - Name functions to describe what they do, not how they do it
;; - Test helpers individually in the REPL
;; - Think about the function signature: what goes in, what comes out?

;; Example helper function:
;; (defn process-item
;;   \"Process a single item according to the rules.\"
;;   [item]
;;   ;; Your logic here
;;   )

;; ============================================================================
;; Part 1
;; ============================================================================

(defn part1
  \"Solve part 1 of the puzzle.

   Think about:
   - Can I solve this with map/filter/reduce?
   - Do I need to iterate through the data?
   - Is there a state that changes over time?
   - Can I break this into smaller transformations?

   Common patterns:
   - Line by line: (map process-line parsed-input)
   - Filtering: (->> data (filter pred) count)
   - Accumulating: (reduce combine initial data)
   - Grid navigation: Use u/neighbors, u/find-in-grid\"
  [parsed-input]
  ;; TODO: Implement part 1 solution
  nil)

;; ============================================================================
;; Part 2
;; ============================================================================

(defn part2
  \"Solve part 2 of the puzzle.

   Often part 2 will:
   - Use the same parsing as part 1
   - Require a modification to your part 1 approach
   - Scale up the problem (may need optimization)

   If part 2 is slow:
   - Consider memoization for recursive functions
   - Look for repeated calculations to cache
   - Check if there's a mathematical pattern
   - Use cycle detection for repeated states\"
  [parsed-input]
  ;; TODO: Implement part 2 solution
  nil)

;; ============================================================================
;; Main
;; ============================================================================

(defn -main [& args]
  (let [input (slurp \"resources/day" day-str "/input.txt\")
        parsed (parse-input input)]
    (println \"Day " day-str "\")
    (println \"Part 1:\" (part1 parsed))
    (println \"Part 2:\" (part2 parsed))))
"))

(defn test-template [day-str]
  (str "(ns aoc2025.day" day-str "-test
  (:require [clojure.test :refer :all]
            [aoc2025.day" day-str " :as day" day-str "]))

(def sample-input
  \"Load sample input for testing\"
  (slurp \"resources/day" day-str "/sample.txt\"))

(def puzzle-input
  \"Load actual puzzle input\"
  (slurp \"resources/day" day-str "/input.txt\"))

(deftest test-part1-sample
  (testing \"Part 1 with sample input\"
    (let [parsed (day" day-str "/parse-input sample-input)]
      ;; TODO: Update expected value once you know the answer
      (is (= nil (day" day-str "/part1 parsed))))))

(deftest test-part1-input
  (testing \"Part 1 with puzzle input\"
    (let [parsed (day" day-str "/parse-input puzzle-input)]
      ;; TODO: Update expected value once you solve it
      (is (= nil (day" day-str "/part1 parsed))))))

(deftest test-part2-sample
  (testing \"Part 2 with sample input\"
    (let [parsed (day" day-str "/parse-input sample-input)]
      ;; TODO: Update expected value once you know the answer
      (is (= nil (day" day-str "/part2 parsed))))))

(deftest test-part2-input
  (testing \"Part 2 with puzzle input\"
    (let [parsed (day" day-str "/parse-input puzzle-input)]
      ;; TODO: Update expected value once you solve it
      (is (= nil (day" day-str "/part2 parsed))))))
"))

(defn create-day [day-num]
  (let [day-str (format "%02d" day-num)
        src-file (str "src/aoc2025/day" day-str ".clj")
        test-file (str "test/aoc2025/day" day-str "_test.clj")
        resource-dir (str "resources/day" day-str)
        sample-file (str resource-dir "/sample.txt")
        input-file (str resource-dir "/input.txt")]

    ;; Create resource directory
    (.mkdirs (io/file resource-dir))

    ;; Create source file
    (spit src-file (source-template day-str))
    (println "Created:" src-file)

    ;; Create test file
    (spit test-file (test-template day-str))
    (println "Created:" test-file)

    ;; Create sample input file
    (spit sample-file "TODO: Add sample input from the puzzle description\n")
    (println "Created:" sample-file)

    ;; Create input file
    (spit input-file "TODO: Add your actual puzzle input from adventofcode.com\n")
    (println "Created:" input-file)

    (println "\nDay" day-str "structure created successfully!")
    (println "Next steps:")
    (println "1. Add sample input to" sample-file)
    (println "2. Add puzzle input to" input-file)
    (println "3. Run tests with: lein test aoc2025.day" day-str "-test")
    (println "4. Run solution with: lein run -m aoc2025.day" day-str)))

(defn -main [& args]
  (if-let [day-num (first args)]
    (try
      (create-day (Integer/parseInt day-num))
      (catch NumberFormatException e
        (println "Error: Please provide a valid day number")
        (println "Usage: lein run -m aoc2025.new-day <day-number>")
        (System/exit 1)))
    (do
      (println "Usage: lein run -m aoc2025.new-day <day-number>")
      (println "Example: lein run -m aoc2025.new-day 2")
      (System/exit 1))))
