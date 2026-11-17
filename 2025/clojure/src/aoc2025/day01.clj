(ns aoc2025.day01
  (:require [clojure.string :as str]
            [aoc2025.parsing :as p]
            [aoc2025.utils :as u]))

;; ============================================================================
;; Day 1: [PUZZLE TITLE]
;; ============================================================================

;; Problem Summary:
;; Go through a list of turns on an imaginary safe and count the number of times that the dial lands on "0" 
;;
;; Approach:
;; - Describe your approach at a high level
;; - What data structure represents the input?
;; - What transformations are needed?
;; - What algorithm or pattern fits this problem?

;; ============================================================================
;; Parsing
;; ============================================================================
(defn parse-line
  "Parse a line like 'L68' into a signed number: L68 -> -68, R42 -> 42"
  [line]
  (let [direction (first line)
        amount (parse-long (subs line 1))]
    (if (= direction \L)
      (- amount)
      amount)))

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
  (->> input
       p/lines
       (map parse-line)))

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
;; (defn process-item [item]
;;   "Process a single item according to the rules."
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
  (->> (reduce (fn [state num]
                 (let [new-position (mod (+ (:position state) num) 100)]
                   (if (= new-position 0)
                     {:position new-position :count (inc (:count state))}
                     {:position new-position :count (:count state)})))
               {:position 50 :count 0}  ; initial state
               parsed-input)           ; collection
       :count))  ; extract just the count at the end
;; ============================================================================
;; Part 2
;; ============================================================================
(defn positions-between
  "Generate all postions visted when moving from-pos by movement.
   Returns a lazy sequence of postions on the dial (0-99)."
  [from-pos movement]
  (if (pos? movement)
    (map #(mod % 100) (range (+ from-pos 1) (+ from-pos movement 1)))
    (map #(mod % 100) (range (+ from-pos -1) (+ from-pos movement -1) -1))))

(defn count-zeros-passed
  "Count the number of zeroes and return the amount"
  [from-pos movement]
  (->> (positions-between from-pos movement)
       (filter zero?)
       count))

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
  (->> (reduce (fn [state num]
                 (let [new-position (mod (+ (:position state) num) 100)
                       zeros-passed (count-zeros-passed (:position state) num)]
                     {:position new-position
                      :count (+ (:count state) zeros-passed)}))
               {:position 50 :count 0}  ; initial state
               parsed-input)           ; collection
       :count))

;; ============================================================================
;; Main
;; ============================================================================

(defn -main [& args]
  (let [input (slurp "resources/day01/sample.txt")
        parsed (parse-input input)]
    (println "Day 01")
    (println "Part 1:" (part1 parsed))
    (println "Part 2:" (part2 parsed))))
