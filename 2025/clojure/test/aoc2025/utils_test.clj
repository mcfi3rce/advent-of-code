(ns aoc2025.utils-test
  (:require [clojure.test :refer :all]
            [aoc2025.utils :as u]))

(deftest test-neighbors
  (testing "Cardinal neighbors only"
    (is (= [[0 1] [2 1] [1 2] [1 0]]
           (u/neighbors [1 1]))))

  (testing "Include diagonals"
    (is (= 8 (count (u/neighbors [1 1] {:include-diagonals true})))))

  (testing "Respect bounds"
    (is (= [[1 0] [0 1]]
           (u/neighbors [0 0] {:bounds [2 2]})))))

(deftest test-manhattan-distance
  (testing "Calculate Manhattan distance"
    (is (= 7 (u/manhattan-distance [0 0] [3 4])))
    (is (= 0 (u/manhattan-distance [5 5] [5 5])))))

(deftest test-in-bounds?
  (testing "Check bounds"
    (is (true? (u/in-bounds? [1 1] 3 3)))
    (is (false? (u/in-bounds? [5 5] 3 3)))
    (is (false? (u/in-bounds? [-1 0] 3 3)))))

(deftest test-frequencies-by
  (testing "Count by function result"
    (is (= {1 2, 2 2}
           (u/frequencies-by count ["a" "bb" "c" "dd"])))))

(deftest test-find-first
  (testing "Find first matching element"
    (is (= 4 (u/find-first even? [1 3 4 5 6])))
    (is (nil? (u/find-first even? [1 3 5])))))

(deftest test-take-until
  (testing "Take until predicate is true"
    (is (= [1 2 3 4 5]
           (u/take-until #(= % 5) [1 2 3 4 5 6 7])))))

(deftest test-gcd
  (testing "Greatest common divisor"
    (is (= 6 (u/gcd 48 18)))
    (is (= 1 (u/gcd 17 19)))))

(deftest test-lcm
  (testing "Least common multiple"
    (is (= 12 (u/lcm 4 6)))
    (is (= 24 (u/lcm-coll [4 6 8])))))

(deftest test-abs
  (testing "Absolute value"
    (is (= 5 (u/abs -5)))
    (is (= 5 (u/abs 5)))
    (is (= 0 (u/abs 0)))))

(deftest test-sign
  (testing "Sign function"
    (is (= -1 (u/sign -5)))
    (is (= 0 (u/sign 0)))
    (is (= 1 (u/sign 3)))))

(deftest test-range-overlap?
  (testing "Check range overlap"
    (is (true? (u/range-overlap? [1 5] [3 7])))
    (is (false? (u/range-overlap? [1 3] [5 7])))))

(deftest test-range-contains?
  (testing "Check range containment"
    (is (true? (u/range-contains? [1 10] [3 5])))
    (is (false? (u/range-contains? [3 5] [1 10])))))

(deftest test-range-intersection
  (testing "Find range intersection"
    (is (= [3 5] (u/range-intersection [1 5] [3 7])))
    (is (nil? (u/range-intersection [1 3] [5 7])))))

;; Run these tests with: lein test aoc2025.utils-test
