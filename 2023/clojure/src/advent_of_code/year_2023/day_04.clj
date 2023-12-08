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
        solution (set (string-to-numbers solution-string))]
    {:hand hand :solution solution :id id}))

(defn calculate-score [answer-set]
  (let [count (count answer-set)]
    (if (= count 0)
      0
      (int (math/pow 2 (dec count))))))

(defn answer-part-1 [parsed-input]
  (map (fn [input]
         (let [card-map (parse-game input)
                hand (:hand card-map)
                solution (:solution card-map)
               winning-set (set/intersection hand solution)]
           (calculate-score winning-set)))
       parsed-input)
  )

(def part-1-answer (apply + (answer-part-1 parsed-input)))
part-1-answer

;(assert (= part-1-answer 21158))
;(assert (= part-1-answer 13))

;;; Part 2
;;; ============================================================================
(def parsed-game (map #(parse-game %) parsed-input))

(defn take-n [s start count]
  (take count (drop start s)))

(defn get-n-games [start count]
  (take-n parsed-game start count))

(defn count-matches [card-map]
  ;; Function to count the number of matching numbers between hand and solution
  ;; Implement the logic based on your game rules
  (let [hand (:hand card-map)
        solution (:solution card-map)
        winning-set (set/intersection hand solution)
        count (count winning-set)]
    count
  ))

(defn process-chunk [cards chunk-size]
  (reduce (fn [[acc-cards acc-count] card]
            (let [matches (count-matches card)
                  new-cards (get-n-games (card :id) matches)]
              [(into acc-cards new-cards) (inc acc-count)]))
          [[] 0]
          (take chunk-size cards)))

(defn answer-part-2 [parsed-input chunk-size]
  (loop [cards (map parse-game parsed-input)
         count 0]
    (if (empty? cards)
      count
      (let [[new-cards chunk-count] (process-chunk cards chunk-size)
            remaining-cards (drop chunk-size cards)]
        (recur (into remaining-cards new-cards) (+ count chunk-count))))))

(def part-2-answer (answer-part-2 parsed-input (count parsed-input)))

;(assert (= part-2-answer part-2-answer))

