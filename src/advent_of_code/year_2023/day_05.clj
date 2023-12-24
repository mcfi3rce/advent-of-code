(ns advent-of-code.year-2023.day-05
  (:require [advent-of-code.core :as core]
            [clojure.string :as string]))

(def input (core/get-input))
(def input (core/get-input))

(defn parse-input [input]
  (string/split input #"\n\n"))

(defn parse-conversion-map [s]
  (map #(mapv parse-long (string/split % #" ")) (rest (string/split-lines s))))

(def parsed-input (parse-input input))
(def seeds (map parse-long
                (re-seq #"\d+" (first parsed-input))))

(def maps (first (map parse-conversion-map (rest parsed-input))))

;;; Part 1
;;; ============================================================================

(defn return-mapping [m x]
  (if (nil? (m x))
    x
    (m x)))

(comment "
  1. Left of mapping range [1]
  2. Inside the range  [2 3 4]
  3. Outside to the right of the range [5 6 7 8 9 10]

Round 1:
  1. [1] -> Passed to next map
     This will be seed range start to -> left of range
  2. [5, 7] -> Passed to next section -> 
  3. [5, 10] -> Passed to next map

Round 2:
[1] [5 6 7 8 9 10]
[8 9 10 11]
[28 29 30 31]

 1. [1] [5, 7] -> re-map
 2. [28, 30] -> passed on 

Final Round:
   [1, 1] [5, 7] [28, 30]

if ( 𝑏𝑠>𝑎𝑒
 or 𝑎𝑠>𝑏𝑒
 ) { return ∅
 }

else {

𝑜𝑠=max(𝑎𝑠,𝑏𝑠)
𝑜𝑒=min(𝑎𝑒,𝑏𝑒)
return [𝑜𝑠,𝑜𝑒]
}

")

(defn find-destination [src-start dest-start length seed]
  (when (and (<= src-start seed)
             (< seed (+ src-start length)))
    (+ dest-start (- seed src-start))))

(defn get-next-mapping [source target n x]
  (let [results
        (map
         (fn [start end n x] (find-destination start end n x))
         target
         source
         n
         (repeat (count source) x))
        clean (remove nil? results)]
    (if (> (count clean) 0)
      (first clean)
      x)))

(defn destructure-collection [c]
  (list (map #(nth % 0) c)
        (map #(nth % 1) c)
        (map #(nth % 2) c)))

(defn answer-part-1 [seeds maps]
  (reduce (fn [current-seeds m]
            (println (take 5 current-seeds) "...")
            (reduce (fn [updated-seeds seed]
                      (let [[source target count] (destructure-collection m)
                            new-seed (get-next-mapping source target count seed)]
                        (conj updated-seeds new-seed)
                        ))
                         []
                    current-seeds))
          seeds
       maps))

;(def part-1-answer (apply min (answer-part-1 seeds maps)))

;(assert (= part-1-answer 35))

;;; Part 2
;;; ============================================================================
(defn convert-to-range [start n]
  (vector start (dec (+ start n))))

(def part-2-seeds (first (map (fn [[start count]]
                        (convert-to-range start count))
                         (partition 2 seeds)
                         )))

(def part-2-maps (map (fn [[target source n]]
                        {:source (convert-to-range source n) :target (convert-to-range target n)})  maps))

(defn mid-range [x y left-bound right-bound]
  ; In-range: 
  ; If x < left-bound -> left-bound
  ; Else: x
  ; If y > right-bound -> right-bound
  ; Else: y 
  (vector (if (> x left-bound) x left-bound) (if (< y right-bound) y right-bound)))

(defn map-range [[seed-l seed-r] source-l target-l]
  (vector (+ target-l (- seed-l source-l)) (+ target-l (- seed-r source-l))))

(defn scan-range [map seed]
  (let [[source-l source-r] (:source map)
        [target-l _] (:target map)
        [seed-l seed-r] seed]
    (println "seed-l: " seed-l " seed-r: " seed-r "source-l: " source-l " source-r: " source-r)
  (cond 
    (or (and (< seed-l source-l) (< seed-r source-l)) (and (> seed-r source-r) (> seed-l source-r))) (vector (vector seed-l seed-r))
    (< seed-l source-l) (vector (vector seed-l (dec source-l)) (vector source-l seed-r))
    (> seed-r source-r) (vector (vector (inc source-r) seed-r) (vector seed-l source-r)) 
    :else 
    (vector (map-range (mid-range seed-l seed-r source-l source-r) source-l target-l)))
    ))


(scan-range {:source [5 9], :target [10 14]} [1 20])
(scan-range {:source [5 9], :target [10 14]} [5 20])
(scan-range {:source [5 9], :target [10 14]} [5 9])
(:mapped (meta (scan-range {:source [5 9], :target [10 14]} [5 9])))
(map-range [6 8] 5 10)
(map-range [5 20] 5 10)


(defn wrapper-scan-range [map seed]
  (loop [current-seed seed
         intermediate-results []] ; Initialize an empty vector to store intermediate results.
    (println "map: " map " c-seed: " current-seed)
    (let [result (scan-range map current-seed)]
      (println "Result: " result)
      (if (and (vector? result) (== 1 (count result)))
        {:final-result (first result) ; The final result is stored separately.
         :intermediate-results intermediate-results} ; Return all intermediate results. 
        (recur (second result) (conj intermediate-results (first result)))))))

;; (loop [remaining-maps part-2-maps
;;        intermediate-results []
;;        final-results [] 
;;        current-seed part-2-seeds] 
;;        (println "final results: "final-results)
;;   (if (empty? intermediate-results)
;;     final-results)
;;   (let [{:keys [final-result intermediate-results]} (wrapper-scan-range (first remaining-maps) current-seed)]
;;     (println final-result intermediate-results)
;;     (recur (rest remaining-maps)
;;            ()
;;     ))

  (loop [remaining-maps part-2-maps
       final-results []
       current-seed part-2-seeds]
    (println "final results: " final-results) 
    (println "remaining maps: " remaining-maps)
  (if (empty? remaining-maps)  ; Check if there are no more maps to process
    final-results              ; If no more maps, return the final results
    (let [current-map (first remaining-maps)                  ; Get the current map to process
          {final-result :final-result
           new-intermediate-results :intermediate-results}    ; Destructure the results from wrapper-scan-range
          (wrapper-scan-range current-map current-seed)]
      (println "final: " final-result " final-results: " final-results " new-result: " new-intermediate-results "current: seed" current-seed)
      (recur (rest remaining-maps)                            ; Pass the rest of the maps for the next iteration
             (conj final-results final-result)                ; Add the current final result to the collection
             (if (empty? new-intermediate-results)            ; Check if there are new intermediate results
               current-seed                                 ; If not, keep the current seed
               (first new-intermediate-results)))))) 

(wrapper-scan-range {:source [1 3], :target [7 9]} [1 4])
(wrapper-scan-range {:source [5 9], :target [10 14]} [4 4])
(wrapper-scan-range {:source [5 9], :target [10 14]} [5 6])
(wrapper-scan-range {:source [5 9], :target [10 14]} [5 10])
(wrapper-scan-range {:source [5 9], :target [10 14]} [11 100])
(wrapper-scan-range {:source [5 9], :target [10 14]} [5 20])

;(def part-2-answer (answer-part-2 parsed-input))

;(assert (= part-2-answer 46))
