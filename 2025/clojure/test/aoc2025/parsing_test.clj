(ns aoc2025.parsing-test
  (:require [clojure.test :refer :all]
            [aoc2025.parsing :as p]))

;; These tests demonstrate how to test your utility functions
;; and provide examples of expected behavior

(deftest test-lines
  (testing "Basic line splitting"
    (is (= ["a" "b" "c"] (p/lines "a\nb\nc"))))

  (testing "Removes empty lines by default"
    (is (= ["a" "b"] (p/lines "a\n\nb"))))

  (testing "Keep empty lines when requested"
    (is (= ["a" "" "b"] (p/lines "a\n\nb" {:keep-empty true})))))
;; => #'aoc2025.parsing-test/test-lines

(deftest test-blocks
  (testing "Split on double newlines"
    (is (= [["a" "b"] ["c" "d"]]
           (p/blocks "a\nb\n\nc\nd")))))

(deftest test-numbers
  (testing "Extract all numbers"
    (is (= [123 456] (p/numbers "foo 123 bar 456"))))

  (testing "Handle negative numbers"
    (is (= [-10 20] (p/numbers "x=-10, y=20")))))

(deftest test-first-number
  (testing "Extract first number"
    (is (= 42 (p/first-number "Game 42: data"))))

  (testing "Return nil when no number"
    (is (nil? (p/first-number "no numbers here")))))

(deftest test-digits
  (testing "Extract individual digits"
    (is (= [1 2 3] (p/digits "a1b2c3")))))

(deftest test-grid
  (testing "Parse into 2D grid"
    (is (= [[\a \b \c] [\d \e \f]]
           (p/grid "abc\ndef")))))

(deftest test-grid-with-coords
  (testing "Parse into coordinate map"
    (is (= {[0 0] \a, [0 1] \b, [1 0] \c, [1 1] \d}
           (p/grid-with-coords "ab\ncd")))))

(deftest test-find-in-grid
  (testing "Find matching coordinates"
    (let [grid (p/grid "..#\n#..")]
      (is (= [[0 2] [1 0]]
             (p/find-in-grid grid #(= % \#)))))))

(deftest test-words
  (testing "Split on whitespace"
    (is (= ["foo" "bar"] (p/words "  foo   bar  ")))))

(deftest test-comma-separated
  (testing "Split on commas and trim"
    (is (= ["a" "b" "c"] (p/comma-separated "a, b , c")))))

(deftest test-parse-line
  (testing "Extract regex groups"
    (is (= ["foo" "42"]
           (p/parse-line #"(\w+): (\d+)" "foo: 42"))))

  (testing "Return nil on no match"
    (is (nil? (p/parse-line #"(\d+)" "no match")))))

(deftest test-parse-key-value
  (testing "Parse colon format"
    (is (= ["foo" "42"] (p/parse-key-value "foo: 42"))))

  (testing "Parse equals format"
    (is (= ["bar" "baz"] (p/parse-key-value "bar=baz")))))

;; Run these tests with: lein test aoc2025.parsing-test
