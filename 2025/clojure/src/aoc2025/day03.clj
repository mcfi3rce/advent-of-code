(ns aoc2025.day03
  (:require [clojure.string :as str]
            [aoc2025.parsing :as p]
            [aoc2025.utils :as u]))

;; ============================================================================
;; Day 03: [PUZZLE TITLE]
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
  "Parse the input string into a data structure.

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
   - See parsing.clj for full list"
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
;;   "Process a single item according to the rules."
;;   [item]
;;   ;; Your logic here
;;   )

;; ============================================================================
;; Part 1
;; ============================================================================

(defn part1
  "Solve part 1 of the puzzle.

   Think about:
   - Can I solve this with map/filter/reduce?
   - Do I need to iterate through the data?
   - Is there a state that changes over time?
   - Can I break this into smaller transformations?

   Common patterns:
   - Line by line: (map process-line parsed-input)
   - Filtering: (->> data (filter pred) count)
   - Accumulating: (reduce combine initial data)
   - Grid navigation: Use u/neighbors, u/find-in-grid"
  [parsed-input]
  ;; TODO: Implement part 1 solution
  nil)

;; ============================================================================
;; Part 2
;; ============================================================================

(defn part2
  "Solve part 2 of the puzzle.

   Often part 2 will:
   - Use the same parsing as part 1
   - Require a modification to your part 1 approach
   - Scale up the problem (may need optimization)

   If part 2 is slow:
   - Consider memoization for recursive functions
   - Look for repeated calculations to cache
   - Check if there's a mathematical pattern
   - Use cycle detection for repeated states"
  [parsed-input]
  ;; TODO: Implement part 2 solution
  nil)

;; ============================================================================
;; Main
;; ============================================================================

(defn -main [& args]
  (let [input (slurp "resources/day03/input.txt")
        parsed (parse-input input)]
    (println "Day 03")
    (println "Part 1:" (part1 parsed))
    (println "Part 2:" (part2 parsed))))
