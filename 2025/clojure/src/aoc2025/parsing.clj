(ns aoc2025.parsing
  "Common parsing utilities for Advent of Code problems.
   These functions help transform raw input strings into usable data structures."
  (:require [clojure.string :as str]))

;; ============================================================================
;; Basic Line and Block Parsing
;; ============================================================================

(defn lines
  "Split input string into lines, removing empty lines by default.

   Examples:
   (lines \"a\\nb\\nc\") => [\"a\" \"b\" \"c\"]
   (lines \"a\\n\\nb\" {:keep-empty true}) => [\"a\" \"\" \"b\"]"
  ([input]
   (lines input {}))
  ([input {:keys [keep-empty] :or {keep-empty false}}]
   (cond-> (str/split-lines input)
     (not keep-empty) (->> (remove str/blank?)))))

(defn blocks
  "Split input into blocks separated by blank lines.
   Each block is a vector of lines.

   Example:
   (blocks \"a\\nb\\n\\nc\\nd\") => [[\"a\" \"b\"] [\"c\" \"d\"]]"
  [input]
  (->> (str/split input #"\n\n")
       (mapv lines)))

(defn paragraphs
  "Alias for blocks - split on double newlines.
   Useful when input has paragraph-style groupings."
  [input]
  (blocks input))

;; ============================================================================
;; Number Extraction
;; ============================================================================

(defn numbers
  "Extract all numbers from a string, returning them as a sequence of longs.
   Handles negative numbers.

   Examples:
   (numbers \"foo 123 bar 456\") => (123 456)
   (numbers \"x=-10, y=20\") => (-10 20)"
  [s]
  (map parse-long (re-seq #"-?\d+" s)))

(defn first-number
  "Extract the first number from a string.

   Example:
   (first-number \"Game 42: data\") => 42"
  [s]
  (when-let [match (re-find #"-?\d+" s)]
    (parse-long match)))

(defn digits
  "Extract individual digits from a string as a sequence of integers.

   Example:
   (digits \"a1b2c3\") => (1 2 3)"
  [s]
  (map #(- (int %) (int \0)) (filter #(Character/isDigit %) s)))

;; ============================================================================
;; Grid Parsing
;; ============================================================================

(defn grid
  "Parse input into a 2D grid (vector of vectors).
   Each character becomes a cell.

   Example:
   (grid \"abc\\ndef\") => [[\\a \\b \\c] [\\d \\e \\f]]"
  [input]
  (mapv vec (lines input)))

(defn char-grid
  "Alias for grid - emphasizes that cells are characters."
  [input]
  (grid input))

(defn grid-with-coords
  "Parse input into a map of {[row col] char}.
   Useful for sparse grids or when you need coordinate lookups.

   Example:
   (grid-with-coords \"ab\\ncd\")
   => {[0 0] \\a, [0 1] \\b, [1 0] \\c, [1 1] \\d}"
  [input]
  (let [line-vec (vec (lines input))]
    (into {}
          (for [row (range (count line-vec))
                col (range (count (nth line-vec row)))]
            [[row col] (nth (nth line-vec row) col)]))))

(defn find-in-grid
  "Find all coordinates where predicate is true in the grid.

   Example:
   (find-in-grid (grid \"..#\\n#..\") #(= % \\#))
   => [[0 2] [1 0]]"
  [grid pred]
  (for [row (range (count grid))
        col (range (count (nth grid row)))
        :when (pred (get-in grid [row col]))]
    [row col]))

;; ============================================================================
;; String Transformations
;; ============================================================================

(defn words
  "Split string into words (whitespace-separated).

   Example:
   (words \"  foo   bar  \") => [\"foo\" \"bar\"]"
  [s]
  (str/split (str/trim s) #"\s+"))

(defn comma-separated
  "Split string on commas, trimming whitespace from each part.

   Example:
   (comma-separated \"a, b , c\") => [\"a\" \"b\" \"c\"]"
  [s]
  (mapv str/trim (str/split s #",")))

;; ============================================================================
;; Pattern Matching Helpers
;; ============================================================================

(defn parse-line
  "Parse a line using a regex pattern, returning captured groups.
   Returns nil if pattern doesn't match.

   Example:
   (parse-line #\"(\\w+): (\\d+)\" \"foo: 42\") => [\"foo\" \"42\"]"
  [pattern line]
  (when-let [match (re-find pattern line)]
    (if (vector? match)
      (subvec match 1)  ; Remove full match, keep groups
      [match])))

(defn parse-lines
  "Parse multiple lines with a pattern, keeping only successful matches.

   Example:
   (parse-lines #\"x=(\\d+)\" [\"x=1\" \"y=2\" \"x=3\"]) => [[\"1\"] [\"3\"]]"
  [pattern lines]
  (keep #(parse-line pattern %) lines))

;; ============================================================================
;; Key-Value Parsing
;; ============================================================================

(defn parse-key-value
  "Parse a line in 'key: value' or 'key=value' format.
   Returns [key value] tuple.

   Example:
   (parse-key-value \"foo: 42\") => [\"foo\" \"42\"]
   (parse-key-value \"bar=baz\") => [\"bar\" \"baz\"]"
  [line]
  (when-let [[_ k v] (re-find #"(\w+)[:=]\s*(.+)" line)]
    [k v]))

(defn parse-key-value-block
  "Parse a block of key-value pairs into a map.

   Example:
   (parse-key-value-block \"foo: 1\\nbar: 2\")
   => {\"foo\" \"1\", \"bar\" \"2\"}"
  [block]
  (into {} (keep parse-key-value (lines block))))

;; ============================================================================
;; TODO: Add more utilities as you discover patterns across days
;; ============================================================================

;; Some ideas for future additions:
;; - parse-range: "1-5" => [1 5] or (range 1 6)
;; - parse-coords: "x=10, y=20" => [10 20]
;; - hex-to-int: Convert hex strings to integers
;; - binary-to-int: Convert binary strings to integers
