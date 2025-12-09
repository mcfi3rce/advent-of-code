(ns aoc2025.day01-test
  (:require [clojure.test :refer :all]
            [aoc2025.day01 :as day01]))

(def sample-input
  "Load sample input for testing"
  (slurp "resources/day01/sample.txt"))

(def puzzle-input
  "Load actual puzzle input"
  (slurp "resources/day01/input.txt"))

(deftest test-parse-line
  (testing "Parse L direction to negative"
    (is (= -68 (day01/parse-line "L68"))))

  (testing "Parse R direction to positive"
    (is (= 42 (day01/parse-line "R42"))))

  (testing "Parse small numbers"
    (is (= -5 (day01/parse-line "L5")))
    (is (= 10 (day01/parse-line "R10")))))

(deftest test-positions-between
  (testing "Moving right without wrapping"
    (is (= [6 7 8 9 10] (day01/positions-between 5 5))))

  (testing "Moving right with wrapping around 0"
    (is (= [96 97 98 99 0 1 2 3 4] (day01/positions-between 95 9))))

  (testing "Moving left without wrapping"
    (is (= [9 8 7 6 5] (day01/positions-between 10 -5))))

  (testing "Moving left with wrapping around 0"
    (is (= [4 3 2 1 0 99 98 97 96] (day01/positions-between 5 -9))))

  (testing "Large movement right crosses 0 multiple times"
    (let [positions (day01/positions-between 0 250)]
      (is (= 2 (count (filter zero? positions))))))
 
  (testing "No movement"
    (is (= [] (day01/positions-between 5 0)))))

(deftest test-count-zeros-passed
  (testing "Cross zero once going right"
    (is (= 1 (day01/count-zeros-passed 95 10))))

  (testing "Cross zero once going left"
    (is (= 1 (day01/count-zeros-passed 5 -10))))

  (testing "Cross zero twice with large movement"
    (is (= 2 (day01/count-zeros-passed 0 200))))

  (testing "Don't cross zero"
    (is (= 0 (day01/count-zeros-passed 50 10))))

  (testing "Start at zero, don't count it"
    (is (= 0 (day01/count-zeros-passed 0 10)))))

(deftest test-part1-sample
  (testing "Part 1 with sample input"
    (let [parsed (day01/parse-input sample-input)]
      (is (= 3(day01/part1 parsed))))))

(deftest test-part1-input
  (testing "Part 1 with puzzle input"
    (let [parsed (day01/parse-input puzzle-input)]
      (is (= 1165 (day01/part1 parsed))))))

(deftest test-part2-sample
  (testing "Part 2 with sample input"
    (let [parsed (day01/parse-input sample-input)]
      (is (= 6(day01/part2 parsed))))))

(deftest test-part2-input
  (testing "Part 2 with puzzle input"
    (let [parsed (day01/parse-input puzzle-input)]
      (is (= 6496 (day01/part2 parsed))))))
