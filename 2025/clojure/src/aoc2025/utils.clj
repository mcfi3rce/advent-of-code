(ns aoc2025.utils
  "Common utility functions for Advent of Code problems.
   Includes algorithms, data structure helpers, and mathematical utilities.")

;; ============================================================================
;; Grid/Coordinate Navigation
;; ============================================================================

(def cardinal-directions
  "The four cardinal directions as [row col] deltas."
  {:north [-1 0]
   :south [1 0]
   :east [0 1]
   :west [0 -1]})

(def cardinal-dir-vectors
  "Cardinal directions as a vector for iteration."
  [[-1 0] [1 0] [0 1] [0 -1]])

(def diagonal-directions
  "The four diagonal directions as [row col] deltas."
  {:ne [-1 1]
   :nw [-1 -1]
   :se [1 1]
   :sw [1 -1]})

(def all-directions
  "All 8 directions (cardinal + diagonal) as [row col] deltas."
  [[-1 0] [1 0] [0 1] [0 -1]    ; cardinal
   [-1 1] [-1 -1] [1 1] [1 -1]]) ; diagonal

(defn neighbors
  "Get neighboring coordinates for a given position.
   By default returns cardinal neighbors only.

   Options:
   - :include-diagonals true/false (default false)
   - :bounds [max-row max-col] to filter out-of-bounds neighbors

   Examples:
   (neighbors [1 1]) => [[0 1] [2 1] [1 2] [1 0]]
   (neighbors [1 1] {:include-diagonals true}) => all 8 neighbors"
  ([coord]
   (neighbors coord {}))
  ([[row col] {:keys [include-diagonals bounds]
               :or {include-diagonals false}}]
   (let [dirs (if include-diagonals all-directions cardinal-dir-vectors)
         nbrs (map (fn [[dr dc]] [(+ row dr) (+ col dc)]) dirs)]
     (if bounds
       (let [[max-row max-col] bounds]
         (filter (fn [[r c]] (and (>= r 0) (>= c 0) (< r max-row) (< c max-col)))
                 nbrs))
       nbrs))))

(defn manhattan-distance
  "Calculate Manhattan distance between two points.
   Works with [row col] or [x y] coordinates.

   Example:
   (manhattan-distance [0 0] [3 4]) => 7"
  [[x1 y1] [x2 y2]]
  (+ (abs (- x2 x1)) (abs (- y2 y1))))

(defn in-bounds?
  "Check if a coordinate is within grid bounds.

   Example:
   (in-bounds? [1 1] 3 3) => true
   (in-bounds? [5 5] 3 3) => false"
  [[row col] rows cols]
  (and (>= row 0) (>= col 0) (< row rows) (< col cols)))

;; ============================================================================
;; Collection Helpers
;; ============================================================================

(defn frequencies-by
  "Like frequencies, but applies a function to each element first.

   Example:
   (frequencies-by count [\"a\" \"bb\" \"c\" \"dd\"]) => {1 2, 2 2}"
  [f coll]
  (frequencies (map f coll)))

(defn group-by-key
  "Group a collection of maps by a specific key.

   Example:
   (group-by-key :type [{:type :a :val 1} {:type :a :val 2} {:type :b :val 3}])
   => {:a [{:type :a :val 1} {:type :a :val 2}], :b [{:type :b :val 3}]}"
  [k coll]
  (group-by k coll))

(defn distinct-by
  "Return distinct elements based on applying function f.

   Example:
   (distinct-by :id [{:id 1 :x 10} {:id 1 :x 20} {:id 2 :x 30}])
   => ({:id 1 :x 10} {:id 2 :x 30})"
  [f coll]
  (vals (reduce (fn [acc item]
                  (let [k (f item)]
                    (if (contains? acc k)
                      acc
                      (assoc acc k item))))
                {}
                coll)))

(defn find-first
  "Return the first item in collection where (pred item) is true.
   Returns nil if no match found.

   Example:
   (find-first even? [1 3 4 5 6]) => 4"
  [pred coll]
  (first (filter pred coll)))

(defn take-until
  "Take elements until predicate is true (includes the matching element).

   Example:
   (take-until #(= % 5) [1 2 3 4 5 6 7]) => (1 2 3 4 5)"
  [pred coll]
  (lazy-seq
   (when-let [s (seq coll)]
     (let [f (first s)]
       (cons f (when-not (pred f)
                 (take-until pred (rest s))))))))

;; ============================================================================
;; Mathematical Utilities
;; ============================================================================

(defn gcd
  "Greatest common divisor using Euclidean algorithm.

   Example:
   (gcd 48 18) => 6"
  [a b]
  (if (zero? b)
    a
    (recur b (mod a b))))

(defn lcm
  "Least common multiple of two numbers.

   Example:
   (lcm 4 6) => 12"
  [a b]
  (/ (* a b) (gcd a b)))

(defn lcm-coll
  "Least common multiple of a collection of numbers.

   Example:
   (lcm-coll [4 6 8]) => 24"
  [coll]
  (reduce lcm coll))

;; Note: Clojure 1.11+ has built-in abs in clojure.core
;; This is here for reference, but you can just use abs directly
(def abs
  "Absolute value (alias to clojure.core/abs for convenience).

   Example:
   (abs -5) => 5"
  clojure.core/abs)

(defn sign
  "Return -1, 0, or 1 based on the sign of n.

   Example:
   (sign -5) => -1
   (sign 0) => 0
   (sign 3) => 1"
  [n]
  (cond
    (pos? n) 1
    (neg? n) -1
    :else 0))

;; ============================================================================
;; Range and Interval Utilities
;; ============================================================================

(defn range-overlap?
  "Check if two ranges [start end] overlap.

   Example:
   (range-overlap? [1 5] [3 7]) => true
   (range-overlap? [1 3] [5 7]) => false"
  [[a1 a2] [b1 b2]]
  (and (<= a1 b2) (<= b1 a2)))

(defn range-contains?
  "Check if range1 completely contains range2.

   Example:
   (range-contains? [1 10] [3 5]) => true
   (range-contains? [3 5] [1 10]) => false"
  [[a1 a2] [b1 b2]]
  (and (<= a1 b1) (>= a2 b2)))

(defn range-intersection
  "Find the intersection of two ranges, or nil if they don't overlap.

   Example:
   (range-intersection [1 5] [3 7]) => [3 5]
   (range-intersection [1 3] [5 7]) => nil"
  [[a1 a2] [b1 b2]]
  (when (range-overlap? [a1 a2] [b1 b2])
    [(max a1 b1) (min a2 b2)]))

;; ============================================================================
;; Memoization Helpers
;; ============================================================================

(defn memoize-by
  "Memoize a function using a custom key function.
   Useful when you want to cache based on specific argument properties.

   Example:
   (def f (memoize-by (fn [x y] x) expensive-fn))
   ; Only caches based on first argument"
  [key-fn f]
  (let [cache (atom {})]
    (fn [& args]
      (let [k (apply key-fn args)]
        (if-let [cached (get @cache k)]
          cached
          (let [result (apply f args)]
            (swap! cache assoc k result)
            result))))))

;; ============================================================================
;; Search Algorithms (Scaffolding - You'll implement when needed)
;; ============================================================================

(defn bfs
  "Breadth-first search template.

   Parameters:
   - start: Initial state
   - goal?: Predicate function - is this state the goal?
   - neighbors-fn: Function that returns neighboring states from current state

   Returns: Path to goal, or nil if no path found.

   TODO: Implement this when you encounter a BFS problem.
   Think about:
   - How to track visited states
   - How to build the path
   - What data structure for the queue"
  [start goal? neighbors-fn]
  ;; TODO: Implement BFS
  nil)

(defn dfs
  "Depth-first search template.

   Parameters:
   - start: Initial state
   - goal?: Predicate function - is this state the goal?
   - neighbors-fn: Function that returns neighboring states from current state

   Returns: Path to goal, or nil if no path found.

   TODO: Implement this when you encounter a DFS problem."
  [start goal? neighbors-fn]
  ;; TODO: Implement DFS
  nil)

(defn dijkstra
  "Dijkstra's shortest path algorithm template.

   Parameters:
   - start: Initial state
   - goal?: Predicate function - is this state the goal?
   - neighbors-fn: Function that returns [neighbor cost] pairs from current state

   Returns: [path total-cost], or nil if no path found.

   TODO: Implement this when you encounter a shortest path problem.
   Think about:
   - Priority queue for unvisited nodes
   - Distance tracking
   - Path reconstruction"
  [start goal? neighbors-fn]
  ;; TODO: Implement Dijkstra
  nil)

;; ============================================================================
;; Cycle Detection
;; ============================================================================

(defn find-cycle
  "Detect a cycle in a sequence using Floyd's algorithm (tortoise and hare).

   Returns: {:cycle-start idx, :cycle-length len} or nil if no cycle.

   Example use case: Detecting repeated states in simulations.

   TODO: Implement when you need cycle detection for a problem."
  [sequence-fn start]
  ;; TODO: Implement Floyd's cycle detection
  nil)

;; ============================================================================
;; TODO: Add more utilities as patterns emerge
;; ============================================================================

;; Ideas for future additions based on common AOC patterns:
;; - rotate-grid: Rotate a 2D grid 90 degrees
;; - transpose-grid: Transpose a 2D grid
;; - flood-fill: Fill connected regions
;; - parse-state-machine: Framework for state machine problems
;; - chinese-remainder: Chinese Remainder Theorem for number theory problems
