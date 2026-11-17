(ns advent-of-code.core
  (:require [clojure.string :as string]))

(defmacro get-input []
  `(slurp ~(string/replace *file* #"[a-z]?\.clj$" "_input.txt")))
