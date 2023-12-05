(ns advent-of-code.year-2023.day-02
  (:require [advent-of-code.core :as core]
            [clojure.string :as string]))

(def input (core/get-input))

(defn parse-input [input](string/split-lines input))

(def parsed-input (parse-input input))

(def test-turn "Game 1: 3 blue, 4 red; 1 red,")
(def game
  [{:game 1
    :hand {:green 0 :blue 3 :red 4}}])

(defn parse-game [game-string]
  (let [[game-part hand-part] (string/split game-string #": ")
        game-number (parse-long (re-find #"\d+" game-part))
        hand-matches (re-seq #"(?i)(\d+) (green|blue|red)" hand-part)
        hand (reduce (fn [acc [_, count-str color-str]]
                       (let [color (keyword color-str)
                             count (Integer/parseInt count-str)
                             current-count (get acc color 0)]
                         (if (> count current-count)
                           (assoc acc color count)
                           acc)))
                     {:green 0 :blue 0 :red 0}
                     hand-matches)]
    {:game game-number :hand hand}))

(def max-hand
  {:red 12 :green 13 :blue 14})


;;; Part 1
;;; ============================================================================

(defn hand-within-max? [hand]
  (every? (fn [[k v]]
            (<= v (get max-hand k)))
          hand))

(defn get-passing-indexes [parsed-input]
  (keep-indexed #(when %2 (inc %1))
                (map #(hand-within-max? (get % :hand))
                     (map #(parse-game %) parsed-input))))


(defn answer-part-1 [parsed-input]
  (apply + (get-passing-indexes parsed-input)))

(def part-1-answer (answer-part-1 parsed-input))

(assert (= part-1-answer 2416))


;;; Part 2
;;; ============================================================================
(defn get-game-power [hand]
  (apply * (map (fn [[k v]]
                  v)
                hand)))

(defn answer-part-2 [parsed-input]
  (apply + (map #(get-game-power (get % :hand)) (map #(parse-game %) parsed-input))))

(def part-2-answer (answer-part-2 parsed-input))

(assert (= part-2-answer 63307))
