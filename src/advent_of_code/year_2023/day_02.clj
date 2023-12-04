(ns advent-of-code.year-2023.day-02
  (:require [advent-of-code.core :as core]
            [clojure.string :as string]))

(def input (core/get-input))

(defn parse-input [input](string/split-lines input))

(def parsed-input (parse-input input))

(defn split-input [parsed-input]
  (->> parsed-input
       (map #(re-seq #"\d" %))
       (map (fn [collection]
              (map parse-long collection)))))
;;; Part 1
;;; ============================================================================

(defn answer-part-1 [parsed-input]
  parsed-input)

(def part-1-answer (answer-part-1 parsed-input))

(assert (= part-1-answer part-1-answer))


;;; Part 2
;;; ============================================================================

(defn answer-part-2 [parsed-input]
  parsed-input)

(def part-2-answer (answer-part-2 parsed-input))

(assert (= part-2-answer part-2-answer))
