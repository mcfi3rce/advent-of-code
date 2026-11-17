(def hello-again "Hello once more")

(ns advent-of-code.year-2023.day-01
  (:require [advent-of-code.core :as core]
            [clojure.string :as string]))

(def input (core/get-input))

(defn parse-input [input] (string/split-lines input))

(def text-to-num
  {"one"  1
   "two"  2
   "three" 3
   "four" 4
   "five" 5
   "six" 6
   "seven" 7
   "eight" 8
   "nine" 9})

(def parsed-input (parse-input input))

;;; Part 1
;;; ============================================================================

(defn answer-part-1 [parsed-input]
  (->> parsed-input
       (map #(re-seq #"\d" %))
       (map #(str (first %) (last %)))
       (map parse-long)
       (apply +)
       ))


(def part-1-answer (answer-part-1 parsed-input))

(assert (= part-1-answer 54927))

;;;(apply + part-1-answer)


(map last parsed-input)

;;; Part 2
;;; ============================================================================
(def spelled-out->number
  {"one"   "1"
   "two"   "2"
   "three" "3"
   "four"  "4"
   "five"  "5"
   "six"   "6"
   "seven" "7"
   "eight" "8"
   "nine"  "9"})

(def spelled-out-number-regexes
  (let [inner-regex-string (string/join "|" (keys spelled-out->number))]
    (mapv (fn [f] (re-pattern (str "\\d|" (f inner-regex-string))))
          [identity string/reverse])))

(defn calibration-value [line]
  (let [[forward-regex backward-regex] spelled-out-number-regexes]
    (->> [(re-find forward-regex line)
          (->> line
               string/reverse
               (re-find backward-regex)
               string/reverse)]
         (replace spelled-out->number)
         string/join
         parse-long)))

(defn answer
  [parsed-input]
   (->> parsed-input
        (map calibration-value)
        (apply +)))

(defn answer-part-2 [parsed-input] (answer parsed-input))

(def part-2-answer (answer-part-2 parsed-input))

(assert (= part-2-answer 54581))
