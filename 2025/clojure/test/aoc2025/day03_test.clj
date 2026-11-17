(ns aoc2025.day03-test
  (:require [clojure.test :refer :all]
            [aoc2025.day03 :as day03]))

(def sample-input
  "Load sample input for testing"
  (slurp "resources/day03/sample.txt"))

(def puzzle-input
  "Load actual puzzle input"
  (slurp "resources/day03/input.txt"))

(deftest test-part1-sample
  (testing "Part 1 with sample input"
    (let [parsed (day03/parse-input sample-input)]
      ;; TODO: Update expected value once you know the answer
      (is (= nil (day03/part1 parsed))))))

(deftest test-part1-input
  (testing "Part 1 with puzzle input"
    (let [parsed (day03/parse-input puzzle-input)]
      ;; TODO: Update expected value once you solve it
      (is (= nil (day03/part1 parsed))))))

(deftest test-part2-sample
  (testing "Part 2 with sample input"
    (let [parsed (day03/parse-input sample-input)]
      ;; TODO: Update expected value once you know the answer
      (is (= nil (day03/part2 parsed))))))

(deftest test-part2-input
  (testing "Part 2 with puzzle input"
    (let [parsed (day03/parse-input puzzle-input)]
      ;; TODO: Update expected value once you solve it
      (is (= nil (day03/part2 parsed))))))
