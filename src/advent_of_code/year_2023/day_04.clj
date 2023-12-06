(ns advent-of-code.year-2023.day-04
  (:require [advent-of-code.core :as core]
            [clojure.string :as string]
            [clojure.set :as set]
            [clojure.math :as math]))

(def input (core/get-input))

(defn parse-input [input]
  (string/split-lines input))

(def parsed-input (parse-input input))


;;; Part 1
;;; ============================================================================

(defn string-to-numbers [input]
  (remove nil? (map parse-long (string/split input #"\s"))))

(defn parse-game [game-string]
  (let [[_ input] (string/split  game-string #":")
        [hand-string solution-string] (string/split input #"\|")
        hand (string-to-numbers hand-string)
        solution (string-to-numbers solution-string)]
    (list hand solution)))

(defn calculate-score [answer-set]
  (let [count (count answer-set)]
    (if (= count 0)
      0
      (int (math/pow 2 (dec count))))))

(defn answer-part-1 [parsed-input]
  (map (fn [input]
         (let [[hand solution] (parse-game input)
               hand-set (set hand)
               solution-set (set solution)
               winning-set (set/intersection hand-set solution-set)]
           (calculate-score winning-set)
           ))
       parsed-input))

(def part-1-answer (apply + (answer-part-1 parsed-input)))
part-1-answer

(assert (= part-1-answer 21158))


;;; Part 2
;;; ============================================================================

(defn answer-part-2 [parsed-input]
  parsed-input)

(def part-2-answer (answer-part-2 parsed-input))

(assert (= part-2-answer part-2-answer))
