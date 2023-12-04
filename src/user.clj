(ns user
  (:require [clojure.string :as string]))

(defn init-day [year day]
  (-> "src/advent_of_code/day_00.clj"
      slurp
      (string/replace "advent-of-code.day-00"
                      (format "advent-of-code.year-%d.day-%02d" year day))
      (->> (spit (format "src/advent_of_code/year_%d/day_%02d.clj" year day))))
  (spit (format "src/advent_of_code/year_%d/day_%02d_input.txt" year day) ""))
