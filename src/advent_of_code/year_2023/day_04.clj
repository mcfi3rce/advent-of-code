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
  (remove nil? (map parse-long (string/split input #"\s+"))))

(defn parse-numbers [s] (map parse-long (re-seq #"\d+" s)))

(defn parse-game [game-string]
  (let [[id-string input] (string/split  game-string #":")
        id (parse-long (re-find #"\d" id-string))
        [hand-string solution-string] (string/split input #"\|")
        hand (set (string-to-numbers hand-string))
        solution (set (string-to-numbers solution-string))
        winning-set (set/intersection hand solution)
        count (count winning-set)]
    {:wins count :id id :count 1}))

(defn calculate-score [count]
    (if (= count 0)
      0
      (int (math/pow 2 (dec count)))))

(defn answer-part-1 [parsed-input]
  (map (fn [input]
         (let [card-map (parse-game input)]
           (calculate-score (card-map :wins))))
       parsed-input))

(def part-1-answer (apply + (answer-part-1 parsed-input)))

(assert (= part-1-answer 21158))
;(assert (= part-1-answer 13))

;;; Part 2
;;; ============================================================================
(def parsed-game (mapv #(parse-game %) parsed-input))

(defn take-n [s start count]
  (take count (drop start s)))

(defn get-n-games [start count]
  (take-n parsed-game start count))

(defn update-counts [games i]
  (reduce #(update-in %1 [%2 :count] + ((games i) :count))
          games
          (range (inc i) (+ (inc i) ((games i) :wins)))))

(defn my-reducer [parsed-game]
  (reduce (fn [games i]
            (update-counts games i))
          parsed-game
          (range (count parsed-game))))

(defn answer-part-2 [parsed-game]
  (apply + (map #(% :count) (my-reducer parsed-game))))

(def part-2-answer (answer-part-2 parsed-game))

;(assert (= part-2-answer 30))
(assert (= part-2-answer 6050769))
