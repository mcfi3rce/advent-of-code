(ns aoc2025.day02-test
  (:require [clojure.test :refer :all]
            [aoc2025.day02 :as day02]))

(def sample-input
  "Load sample input for testing"
  (slurp "resources/day02/sample.txt"))

(def puzzle-input
  "Load actual puzzle input"
  (slurp "resources/day02/input.txt"))

(deftest test-part1-sample
  (testing "Part 1 with sample input"
    (let [parsed (day02/parse-input sample-input)]
      ;; TODO: Update expected value once you know the answer
      (is (= nil (day02/part1 parsed))))))

(deftest test-part1-input
  (testing "Part 1 with puzzle input"
    (let [parsed (day02/parse-input puzzle-input)]
      ;; TODO: Update expected value once you solve it
      (is (= nil (day02/part1 parsed))))))

(deftest test-part2-sample
  (testing "Part 2 with sample input"
    (let [parsed (day02/parse-input sample-input)]
      ;; TODO: Update expected value once you know the answer
      (is (= nil (day02/part2 parsed))))))

(deftest test-part2-input
  (testing "Part 2 with puzzle input"
    (let [parsed (day02/parse-input puzzle-input)]
      ;; TODO: Update expected value once you solve it
      (is (= nil (day02/part2 parsed))))))
